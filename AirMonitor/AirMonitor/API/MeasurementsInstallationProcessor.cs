using AirMonitor.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AirMonitor.API
{
	public static class MeasurementsInstallationProcessor
	{
	
		public static async Task<MeasurementsInstallationModel> GetMeasurementsForSpecificInstallationAsync(double latitude, double longitude , int installationId)
		{
				string queryToAppend = $"?installationId={installationId}";
				UriBuilder baseUri = new UriBuilder(APIHelper.ApiClient.BaseAddress);
				//baseUri.Path = baseUri.Path.Substring(1) + App.APIMeasurementsInstallationEndpoint;
				baseUri.Path = baseUri.Path.Substring(1) + APIHelper.APIMeasurementsInstallationEndpoint;
				baseUri.Query = queryToAppend;


				using (HttpResponseMessage response = await APIHelper.ApiClient.GetAsync(baseUri.Uri))
				{
					if (response.IsSuccessStatusCode)
					{
						MeasurementsInstallationModel measurementsInstallation = await response.Content.ReadAsAsync<MeasurementsInstallationModel>();
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
