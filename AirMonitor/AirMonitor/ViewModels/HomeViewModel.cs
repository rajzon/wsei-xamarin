using AirMonitor.API;
using AirMonitor.Helpers;
using AirMonitor.Models;
using AirMonitor.Views;
using System.Collections.Generic;
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
            GetMeasurmentsAsync(2,6000);
                   
           
        }
  

        private async void GetMeasurmentsAsync(int maxResults = 1 , int maxDistanceKM = 3)
        {
            var latLngCooridnates =  await LocationInformationsRetrieveHelper.GetLocationAsync();

            if (latLngCooridnates != null)
            {
                await Task.Run(async () =>
                 {
                     var latitude = LocationInformationsRetrieveHelper.GetLatitude(latLngCooridnates);
                     var longitude = LocationInformationsRetrieveHelper.GetLongitude(latLngCooridnates);
                     var listOfMeasurements = await GetMeasurementsForNearestOneInstallationAsync(latitude, longitude, maxResults , maxDistanceKM);
                     foreach (var measurement in listOfMeasurements)
                     {
                         ListOfMeasurementObs.Add(measurement);
                     }
                     IsDataDownloading = false;
                 });
            }
            else
            {             
                IsDataDownloading = false;
            }
        }

        private async Task<IEnumerable<MeasurementModel>> GetMeasurementsForNearestOneInstallationAsync(double latitude , double longitude , int maxResults , int maxDistanceKM)
        {                   
            var listOfInstallationNearest = await InstallationNearestProcessor.GetInstallationsAsync(latitude, longitude, maxResults , maxDistanceKM);
            var measurements = new MeasurementModel();
            var listOfMeasurements = new List<MeasurementModel>();
            foreach (var installation in listOfInstallationNearest)
            {
                int installationId = installation.Id;
                measurements = await MeasurementsInstallationProcessor.GetMeasurementsForSpecificInstallationAsync(latitude, longitude, installationId);
                measurements.Installation = installation;
                listOfMeasurements.Add(measurements);
            }
 
            return listOfMeasurements;
        }

       

        async private void ChangePage(MeasurementModel obj)
        {
            await HomeNav.PushAsync(new DetailsPage(obj));
        }
    }
}
