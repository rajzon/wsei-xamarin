using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirMonitor.Models
{
   public class MeasurementEntity
    {
        [PrimaryKey , AutoIncrement]
        public int Id { get; set; }
        public int CurrentId { get; set; }
        public int InstallationId { get; set; }
        public string History { get; set; }
        public string Forecast { get; set; }

        public MeasurementEntity()
        {

        }

        public MeasurementEntity(MeasurementModel measurementModel , MeasurementCurrentEntity measurementCurrentEntity)
        {
            CurrentId = measurementCurrentEntity.Id;
            History = JsonConvert.SerializeObject(measurementModel.History);
            Forecast = JsonConvert.SerializeObject(measurementModel.Forecast);
            InstallationId = measurementModel.Installation.Id;
        }

       
    }
}
