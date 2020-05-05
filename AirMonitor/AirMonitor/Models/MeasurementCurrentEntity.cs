using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirMonitor.Models
{
    public class MeasurementCurrentEntity
    {
     
        [PrimaryKey , AutoIncrement]
        public int Id { get; set; }
        public DateTime FromDateTime { get; set; }
        public DateTime TillDateTime { get; set; }

        public string Values { get; set; }

        public string Indexes { get; set; }

        public string Standards { get; set; }

        public MeasurementCurrentEntity()
        {

        }

        public MeasurementCurrentEntity(MeasurementModel measurementModel)
        {
            FromDateTime = measurementModel.Current.FromDateTime;
            TillDateTime = measurementModel.Current.TillDateTime;
            Values = JsonConvert.SerializeObject(measurementModel.Current.Values);
            Indexes = JsonConvert.SerializeObject(measurementModel.Current.Indexes);
            Standards = JsonConvert.SerializeObject(measurementModel.Current.Standards);
        }
    }
}
