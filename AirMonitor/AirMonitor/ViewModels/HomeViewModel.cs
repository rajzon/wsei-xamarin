using AirMonitor.API;
using AirMonitor.Helpers;
using AirMonitor.Models;
using AirMonitor.Views;
using Android.Telecom;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AirMonitor.ViewModels
{
    class HomeViewModel : BaseViewModel
    {
        public INavigation HomeNav { get; set; }
        public ICommand GoToDetailsCommand => new Command<MeasurementsInstallationModel>(ChangePage);      

        public ObservableCollection<MeasurementsInstallationModel> ListOfMeasurementObs { get; set; }   

        private bool isDataDownloading = true;
        public bool IsDataDownloading
        {
            get { return isDataDownloading; }
            set { SetProperty(ref isDataDownloading, value); }
        }


        public  HomeViewModel(INavigation homeNav)
        {         
            HomeNav = homeNav;

            ListOfMeasurementObs = new ObservableCollection<MeasurementsInstallationModel>();
           
            GetMeasurmentsAsync();

        }

        private async void GetMeasurmentsAsync()
        {
            var latLngCooridnates = await LocationInformationsRetrieveHelper.GetLocationAsync();
            var latitude = LocationInformationsRetrieveHelper.GetLatitude(latLngCooridnates);
            var longitude = LocationInformationsRetrieveHelper.GetLongitude(latLngCooridnates);
            IsDataDownloading = await GetMeasurementsForNearestOneInstallationAsync(latitude , longitude);
        }

        private async Task<bool> GetMeasurementsForNearestOneInstallationAsync(double latitude , double longitude)
        {        
            var isDataDownloading = true;
            

            var listOfInstallationNearest = await InstallationNearestProcessor.GetInstallationsAsync(latitude, longitude, 1);
            int nearestInstallationId = listOfInstallationNearest[0].id;
            int installationId = nearestInstallationId;
    

            var measurements = await MeasurementsInstallationProcessor.GetMeasurementsForSpecificInstallationAsync(latitude, longitude, installationId);
            SetLocationInformationForMeasurements(measurements, listOfInstallationNearest);
            
           
            isDataDownloading = false;
            return isDataDownloading;
        }

        private void SetLocationInformationForMeasurements(MeasurementsInstallationModel measurements , List<InstallationNearestModel> listOfInstallationNearest)
        {
            for (int i = 0; i < listOfInstallationNearest.Count; i++)
            {
                measurements.address = listOfInstallationNearest[i].address;
                measurements.airly = listOfInstallationNearest[i].airly;
                measurements.location = listOfInstallationNearest[i].location;
                measurements.elevation = listOfInstallationNearest[i].elevation;
                measurements.sponsor = listOfInstallationNearest[i].sponsor;
                measurements.id = listOfInstallationNearest[i].id;

            }
            ListOfMeasurementObs.Add(measurements);

        }

        async private void ChangePage(MeasurementsInstallationModel obj)
        {
            await HomeNav.PushAsync(new DetailsPage(obj));
        }
    }
}
