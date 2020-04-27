

namespace AirMonitor.Models
{
    public class Location
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Address 
    {
        public string country { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string number { get; set; }
        public string displayAddress1 { get; set; }
        public string displayAddress2 { get; set; }
    }

    public class Sponsor
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string logo { get; set; }
        public string link { get; set; }
    }

    public class InstallationNearestModel
    {
       
        public int id { get; set; }

        
        public Location location { get; set; }

       
        public Address address { get; set; }

        
        public double elevation { get; set; }

        
        public bool airly { get; set; }

        
        public Sponsor sponsor { get; set; }

        
    }
}
