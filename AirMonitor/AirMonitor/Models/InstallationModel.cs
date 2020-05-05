

using Newtonsoft.Json;

namespace AirMonitor.Models
{
    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Location()
        {

        }
    }

    public class Address 
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string DisplayAddress1 { get; set; }
        public string DisplayAddress2 { get; set; }

        public Address()
        {

        }
    }

    public class Sponsor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string Link { get; set; }

        public Sponsor()
        {

        }
    }

    public class InstallationModel
    {
       
        public int Id { get; set; }
        public Location Location { get; set; }
        public Address Address { get; set; }  
        public double Elevation { get; set; }   
        public bool Airly { get; set; }  
        public Sponsor Sponsor { get; set; }
        public InstallationModel()
        {

        }

        public InstallationModel(InstallationEnitity installationEnitity)
        {
            int.TryParse(installationEnitity.Id, out int installationEnitityId);
            Id = installationEnitityId;
            Location = JsonConvert.DeserializeObject<Location>(installationEnitity.Location);
            Address = JsonConvert.DeserializeObject<Address>(installationEnitity.Address);
            Elevation = installationEnitity.Elevation;
            Airly = installationEnitity.Airly;
            Sponsor = JsonConvert.DeserializeObject<Sponsor>(installationEnitity.Sponsor);

        }
    }
}
