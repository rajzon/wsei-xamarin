using AirMonitor.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace AirMonitor.API
{
	public static class InstallationNearestProcessor
	{
		

		/// <summary>
		/// Return List of nearest installation starting from the nearest one
		/// </summary>
		/// <param name="latitude"></param>
		/// <param name="longitude"></param>
		/// <param name="maxResults">max amount of installation that returns</param>
		/// <returns></returns>
		public static async Task<List<InstallationNearestModel>> GetInstallationsAsync(double latitude, double longitude, int maxResults)

		{

			string queryToAppend = $"?lat={latitude}&lng={longitude}&maxDistanceKM=3000&maxResults={maxResults}";
			UriBuilder baseUri = new UriBuilder(APIHelper.ApiClient.BaseAddress);
			//baseUri.Path = baseUri.Path.Substring(1) + App.APIInstallationNearestEndpoint;
			baseUri.Path = baseUri.Path.Substring(1) + APIHelper.APIInstallationNearestEndpoint;
			baseUri.Query = queryToAppend;


			using (HttpResponseMessage response = await APIHelper.ApiClient.GetAsync(baseUri.Uri))
			{
				if (response.IsSuccessStatusCode)
				{
					//string responseString = await response.Content.ReadAsStringAsync();
					//var deserializeJSON = (JsonConvert.DeserializeObject<List<Location>>(responseString));

					//int TEST=1;



					List<InstallationNearestModel> installationsNearest = await response.Content.ReadAsAsync<List<InstallationNearestModel>>();
					return installationsNearest;
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}

		}





	}
}
