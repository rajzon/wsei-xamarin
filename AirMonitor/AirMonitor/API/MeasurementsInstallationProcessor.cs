using AirMonitor.Helpers;
using AirMonitor.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AirMonitor.API
{
	public static class MeasurementsInstallationProcessor
	{
	
		public static async Task<MeasurementModel> GetMeasurementsForSpecificInstallationAsync(double latitude, double longitude , int installationId)
		{

				string queryToAppend = $"?installationId={installationId}";
				UriBuilder baseUri = new UriBuilder(APIHelper.ApiClient.BaseAddress);
				baseUri.Path = baseUri.Path.Substring(1) + APIHelper.APIMeasurementsInstallationEndpoint;
				baseUri.Query = queryToAppend;


				using (HttpResponseMessage response = await APIHelper.ApiClient.GetAsync(baseUri.Uri))
				{
					if (response.IsSuccessStatusCode)
					{					
						MeasurementModel measurementsInstallation = await response.Content.ReadAsAsync<MeasurementModel>();
						return measurementsInstallation;

					}
					else
					{
						throw new Exception(response.ReasonPhrase);
					}
				}	
		}

        public static async Task<IEnumerable<MeasurementModel>> GetMeasurementsForNearestOneInstallationAsync(double latitude, double longitude, int maxResults, int maxDistanceKM)
        {
            var listOfInstallationNearest = await InstallationNearestProcessor.GetInstallationsAsync(latitude, longitude, maxResults, maxDistanceKM);
            //Save Installations to DataBase
            App.DBInstantion.SaveInstallations(listOfInstallationNearest);

            var measurements = new MeasurementModel();
            var listOfMeasurements = new List<MeasurementModel>();
            if (!DatabaseHelper.IsDataUpToDateInDB(maxResults))
            {
                foreach (var installation in listOfInstallationNearest)
                {
                    int installationId = installation.Id;
                    measurements = await MeasurementsInstallationProcessor.GetMeasurementsForSpecificInstallationAsync(latitude, longitude, installationId);
                    measurements.Installation = installation;
                    listOfMeasurements.Add(measurements);

                }
            }
            else
            {
                listOfMeasurements.AddRange(App.DBInstantion.LoadMeasurements());
            }
            //Save Measurements to DataBase
            App.DBInstantion.SaveMeasurements(listOfMeasurements);
            return listOfMeasurements;
        }
    }
}
