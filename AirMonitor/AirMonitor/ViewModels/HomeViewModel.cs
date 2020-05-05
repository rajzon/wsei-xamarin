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
        private bool isDataDownloading = true;
        public bool IsDataDownloading
        {
            get { return isDataDownloading; }
            set { SetProperty(ref isDataDownloading, value); }
        }

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { SetProperty(ref isRefreshing, value); }
        }

        public INavigation HomeNav { get; set; }
        public ICommand GoToDetailsCommand => new Command<MeasurementModel>(ChangePage);
        
        public ICommand RefreshMeasurementsCommand
        {
            get
            {
                return new Command(() => 
                {                 
                    IsRefreshing = true;                   
                    GetMeasurmentsAsync(1, 3000 , true);
                    IsRefreshing = false;
                  });
            
            }
        }
        public ObservableCollection<MeasurementModel> ListOfMeasurementObs { get; set; }

        public  HomeViewModel(INavigation homeNav)
        { 
            HomeNav = homeNav;
            ListOfMeasurementObs = new ObservableCollection<MeasurementModel>();
            GetMeasurmentsAsync(1 , 3000);           
        }


        private async void GetMeasurmentsAsync(int maxResults = 1 , int maxDistanceKM = 3 , bool isRefreshCommandDemand = false)
        {
            var latLngCooridnates =  await LocationInformationsRetrieveHelper.GetLocationAsync();
            if (latLngCooridnates != null)
            {
                await Task.Run(async () =>
                 {
                     var latitude = LocationInformationsRetrieveHelper.GetLatitude(latLngCooridnates);
                     var longitude = LocationInformationsRetrieveHelper.GetLongitude(latLngCooridnates);
                     var listOfMeasurements = await MeasurementsInstallationProcessor.GetMeasurementsForNearestOneInstallationAsync(latitude, longitude, maxResults , maxDistanceKM);
                     if (isRefreshCommandDemand)
                     {
                         ListOfMeasurementObs.Clear();
                         foreach (var measurement in listOfMeasurements)
                         {
                             ListOfMeasurementObs.Add(measurement);
                         }                       
                     }
                     else
                     {
                         foreach (var measurement in listOfMeasurements)
                         {
                             ListOfMeasurementObs.Add(measurement);
                         }
                         IsDataDownloading = false;
                     }
                 });
            }
            else
            {
                IsDataDownloading = false;
            }
            
        }
  
        async private void ChangePage(MeasurementModel obj)
        {
            await HomeNav.PushAsync(new DetailsPage(obj));
        }
    }
}
