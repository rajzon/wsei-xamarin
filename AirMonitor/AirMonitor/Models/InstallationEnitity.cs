using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirMonitor.Models
{
    public class InstallationEnitity
    {

        [PrimaryKey]
        public string Id { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public double Elevation { get; set; }
        public bool Airly { get; set; }

        public string Sponsor { get; set; }


        public InstallationEnitity()
        {

        }

        public InstallationEnitity(InstallationModel installation)
        {
           
                Id = installation.Id.ToString();
                Elevation = installation.Elevation;
                Airly = installation.Airly;
                Location = JsonConvert.SerializeObject(installation.Location);
                Address = JsonConvert.SerializeObject(installation.Address);
                Sponsor = JsonConvert.SerializeObject(installation.Sponsor);
            

        }
    }
}
