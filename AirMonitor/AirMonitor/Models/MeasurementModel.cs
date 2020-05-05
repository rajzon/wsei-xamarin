using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirMonitor.Models
{
    public class MeasurementValue 
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }

        public MeasurementValue()
        {

        }
    }

    public class AirQualityIndex 
    {
        [PrimaryKey , AutoIncrement]    
        public int Id { get; set; }
        public string Name { get; set; }
        public double? Value { get; set; }
        public string Level { get; set; }
        public string Description { get; set; }
        public string Advice { get; set; }
        public string Color { get; set; }

        public AirQualityIndex()
        {

        }
    }

    public class AirQualityStandard
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pollutant { get; set; }
        public double Limit { get; set; }
        public double Percent { get; set; }
        public string Averaging { get; set; }

        public AirQualityStandard()
        {

        }
    }

    public class Current
    {
        public DateTime FromDateTime { get; set; }
        public DateTime TillDateTime { get; set; }
        public List<MeasurementValue> Values { get; set; }
        public List<AirQualityIndex> Indexes { get; set; }
        public List<AirQualityStandard> Standards { get; set; }

        public Current()
        {

        }

        public Current(MeasurementCurrentEntity measurementCurrentEntity , List<MeasurementValue> measurementValue , List<AirQualityIndex> airQualityIndex , List<AirQualityStandard> airQualityStandard)
        {
            FromDateTime = measurementCurrentEntity.FromDateTime;
            TillDateTime = measurementCurrentEntity.TillDateTime;
            Values = measurementValue;
            Indexes = airQualityIndex;
            Standards = airQualityStandard;

        }
    }


    public class History
    {
        public DateTime FromDateTime { get; set; }
        public DateTime TillDateTime { get; set; }
        public List<MeasurementValue> Values { get; set; }
        public List<AirQualityIndex> Indexes { get; set; }
        public List<AirQualityStandard> Standards { get; set; }

        public History()
        {

        }
    }

   
    public class Forecast
    {
        public DateTime FromDateTime { get; set; }
        public DateTime TillDateTime { get; set; }
        public List<MeasurementValue> Values { get; set; }
        public List<AirQualityIndex> Indexes { get; set; }
        public List<AirQualityStandard> Standards { get; set; }

        public Forecast()
        {

        }
    }

    public class MeasurementModel
    {
        public Current Current { get; set; }
        public InstallationModel Installation { get; set; }

        public List<History> History { get; set; }
        public List<Forecast> Forecast { get; set; }

        public MeasurementModel()
        {

        }

        public MeasurementModel(MeasurementEntity measurementEntity , InstallationModel installationModel , Current current)
        {
            Current = current;
            History = JsonConvert.DeserializeObject<List<History>>(measurementEntity.History);
            Forecast = JsonConvert.DeserializeObject<List<Forecast>>(measurementEntity.Forecast);
            Installation = installationModel;

        }

        
    }
}
