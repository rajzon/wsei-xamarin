using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirMonitor.Models
{
    public class MeasurementValue 
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
    }

    public class AirQualityIndex 
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public string Level { get; set; }
        public string Description { get; set; }
        public string Advice { get; set; }
        public string Color { get; set; }
    }

    public class AirQualityStandard
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pollutant { get; set; }
        public double Limit { get; set; }
        public double Percent { get; set; }
        public string Averaging { get; set; }
    }

    public class Current
    {
        public DateTime FromDateTime { get; set; }
        public DateTime TillDateTime { get; set; }
        public List<MeasurementValue> Values { get; set; }
        public List<AirQualityIndex> Indexes { get; set; }
        public List<AirQualityStandard> Standards { get; set; }
    }


    public class History
    {
        public DateTime FromDateTime { get; set; }
        public DateTime TillDateTime { get; set; }
        public List<MeasurementValue> Values { get; set; }
        public List<AirQualityIndex> Indexes { get; set; }
        public List<AirQualityStandard> Standards { get; set; }
    }

   
    public class Forecast
    {
        public DateTime FromDateTime { get; set; }
        public DateTime TillDateTime { get; set; }
        public List<MeasurementValue> Values { get; set; }
        public List<AirQualityIndex> Indexes { get; set; }
        public List<AirQualityStandard> Standards { get; set; }
    }

    public class MeasurementModel  : InstallationModel
    {
        public Current Current { get; set; }
        public List<History> History { get; set; }
        public List<Forecast> Forecast { get; set; }

        
    }
}
