using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;

namespace AirMonitor.API
{
    public static class APIHelper
    {
        public static HttpClient ApiClient { get; private set; }

        public static string APIKey { get; private set; }
        public static string APIBaseAdress { get; private set; }
        public static string APIInstallationNearestEndpoint { get; private set; }
        public static string APIMeasurementsInstallationEndpoint { get; private set; }
        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri(APIBaseAdress);
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ApiClient.DefaultRequestHeaders.Add("apikey", APIKey);

        }

        public static void GetAPIConfig()
        {
            var assembly = Assembly.GetExecutingAssembly();
            string result;
            string resourceName = assembly.GetManifestResourceNames()
             .Single(str => str.EndsWith("config.json"));


            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();

                }

                var apiConfig = JObject.Parse(result);

                APIKey = (string)apiConfig["apikey"];
                APIBaseAdress = (string)apiConfig["apiAddress"];
                APIInstallationNearestEndpoint = (string)apiConfig["apiInstallationNearestEndpoint"];
                APIMeasurementsInstallationEndpoint = (string)apiConfig["apiMeasurementsInstallationEndpoint"];
            }
        }


    }
}
