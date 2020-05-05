using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AirMonitor.Helpers
{
   public static class LocationInformationsRetrieveHelper
    {

        public async static Task<double[]> GetLocationAsync()
        {
            try
            {
                var location = await Geolocation.GetLocationAsync();
                double[] locationLatLng = { location.Latitude, location.Longitude };

                return locationLatLng;
            }
            catch(PermissionException)
            {
                await Application.Current.MainPage.DisplayAlert($"{nameof(PermissionException)}", "Permission to get location information was denied , causing inability to get measurements", "Ok");
                return null;
            }
        }

        public  static double GetLatitude(double[] latLngLocation)
        {
            return latLngLocation[0];
        }

        public  static double GetLongitude(double[] latLngLocation)
        {
            return latLngLocation[1];
        }


    }
}
