using AirMonitor.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AirMonitor.API
{
	public static class InstallationNearestProcessor
	{
		

		/// <summary>
		/// Return List of nearest installation starting from the nearest one
		/// </summary>
		/// <param name="latitude"></param>
		/// <param name="longitude"></param>
		/// <param name="maxResults">max amount of installation that method returns</param>
		/// <returns></returns>
		public static async Task<List<InstallationModel>> GetInstallationsAsync(double latitude, double longitude, int maxResults)
		{

			string queryToAppend = $"?lat={latitude}&lng={longitude}&maxDistanceKM=3000&maxResults={maxResults}";
			UriBuilder baseUri = new UriBuilder(APIHelper.ApiClient.BaseAddress);
			baseUri.Path = baseUri.Path.Substring(1) + APIHelper.APIInstallationNearestEndpoint;
			baseUri.Query = queryToAppend;


			using (HttpResponseMessage response = await APIHelper.ApiClient.GetAsync(baseUri.Uri))
			{
				if (response.IsSuccessStatusCode)
				{

					List<InstallationModel> installationsNearest = await response.Content.ReadAsAsync<List<InstallationModel>>();
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
