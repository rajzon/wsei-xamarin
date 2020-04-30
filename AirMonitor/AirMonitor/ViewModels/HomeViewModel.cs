using AirMonitor.API;
using AirMonitor.Helpers;
using AirMonitor.Models;
using AirMonitor.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AirMonitor.ViewModels
{
    class HomeViewModel : BaseViewModel
    {
        public INavigation HomeNav { get; set; }
        public ICommand GoToDetailsCommand => new Command<MeasurementModel>(ChangePage);      

        public ObservableCollection<MeasurementModel> ListOfMeasurementObs { get; set; }   

        private bool isDataDownloading = true;
        public bool IsDataDownloading
        {
            get { return isDataDownloading; }
            set { SetProperty(ref isDataDownloading, value); }
        }



        public  HomeViewModel(INavigation homeNav)
        {         
            HomeNav = homeNav;

            ListOfMeasurementObs = new ObservableCollection<MeasurementModel>();

            GetMeasurmentsAsync();
                   
           
        }
  

        private async void GetMeasurmentsAsync()
        {
            var latLngCooridnates =  await LocationInformationsRetrieveHelper.GetLocationAsync();

           await Task.Run(async () =>
            {
                var latitude = LocationInformationsRetrieveHelper.GetLatitude(latLngCooridnates);
                var longitude = LocationInformationsRetrieveHelper.GetLongitude(latLngCooridnates);
                IsDataDownloading = await GetMeasurementsForNearestOneInstallationAsync(latitude, longitude);
            });
        }

        private async Task<bool> GetMeasurementsForNearestOneInstallationAsync(double latitude , double longitude)
        {        
            var isDataDownloading = true;
            

            var listOfInstallationNearest = await InstallationNearestProcessor.GetInstallationsAsync(latitude, longitude, 1);
            int nearestInstallationId = listOfInstallationNearest.FirstOrDefault().Id;
            int installationId = nearestInstallationId;
    

            var measurements = await MeasurementsInstallationProcessor.GetMeasurementsForSpecificInstallationAsync(latitude, longitude, installationId);
             SetLocationInformationForMeasurements(measurements, listOfInstallationNearest.FirstOrDefault());
            
           
            isDataDownloading = false;
            return isDataDownloading;
        }

        private void SetLocationInformationForMeasurements(MeasurementModel measurements , InstallationModel specificInstallationNearest)
        {
           
                measurements.Address = specificInstallationNearest.Address;
                measurements.Airly = specificInstallationNearest.Airly;
                measurements.Location = specificInstallationNearest.Location;
                measurements.Elevation = specificInstallationNearest.Elevation;
                measurements.Sponsor = specificInstallationNearest.Sponsor;
                measurements.Id = specificInstallationNearest.Id;

                ListOfMeasurementObs.Add(measurements);

        }

        async private void ChangePage(MeasurementModel obj)
        {
            await HomeNav.PushAsync(new DetailsPage(obj));
        }
    }
}
