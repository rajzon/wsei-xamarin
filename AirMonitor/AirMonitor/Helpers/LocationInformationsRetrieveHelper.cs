using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AirMonitor.Helpers
{
   public static class LocationInformationsRetrieveHelper
    {

        public async static Task<double[]> GetLocationAsync()
        {
            var location = await Geolocation.GetLocationAsync();
            double[] locationLatLng = {location.Latitude , location.Longitude};

            return locationLatLng;
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
