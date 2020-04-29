using AirMonitor.Models;
using System;
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
	}
}
