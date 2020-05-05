using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using AirMonitor.Models;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace AirMonitor.Helpers
{
    public  class DatabaseHelper : IDisposable
    {
       private static readonly string dbName = "AirlyDB.db";
       private static readonly string dbFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
       private static readonly string dbPath = Path.Combine(dbFolder,dbName);
       private static readonly SQLiteOpenFlags flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex;
       private bool isDisposed = false;
       private SQLiteConnection DBConnection { get; set; }


        public  void InitializeDBEntities()
        {

            DBConnection = new SQLiteConnection(dbPath,flags);
       
                DBConnection.CreateTable<InstallationEnitity>();
                DBConnection.CreateTable<MeasurementEntity>();
                DBConnection.CreateTable<MeasurementCurrentEntity>();
                DBConnection.CreateTable<MeasurementValue>();
                DBConnection.CreateTable<AirQualityIndex>();
                DBConnection.CreateTable<AirQualityStandard>();
            

        }

        public void SaveInstallations(List<InstallationModel> installations) 
        {            
                List<InstallationEnitity> listOfInstallations = new List<InstallationEnitity>();
                foreach (var installation in installations)
                {
                    var installationEnitity = new InstallationEnitity(installation);
                    listOfInstallations.Add(installationEnitity);                   
                }

                
                DBConnection.RunInTransaction(() =>
                    {
                        DBConnection.DeleteAll<InstallationEnitity>();
                        DBConnection.InsertAll(listOfInstallations);
                    }
                );

                var installationEnitityInfo = DBConnection.Table<InstallationEnitity>().ToList();

        }

        public void SaveMeasurements(List<MeasurementModel> listOfMeasurements)
        {

                    DBConnection.RunInTransaction(() => 
                    {
                        DBConnection.DeleteAll<MeasurementEntity>();
                        DBConnection.DeleteAll<MeasurementCurrentEntity>();
                        DBConnection.DeleteAll<MeasurementValue>();
                        DBConnection.DeleteAll<AirQualityIndex>();
                        DBConnection.DeleteAll<AirQualityStandard>();

                        foreach (var measurement in listOfMeasurements)
                        {

                            DBConnection.InsertAll(measurement.Current.Values, false);
                            DBConnection.InsertAll(measurement.Current.Indexes, false);
                            DBConnection.InsertAll(measurement.Current.Standards, false);

                            MeasurementCurrentEntity measurementCurrentEntity = new MeasurementCurrentEntity(measurement);
                            DBConnection.Insert(measurementCurrentEntity);
                            MeasurementEntity measurementEntity = new MeasurementEntity(measurement, measurementCurrentEntity);
                            DBConnection.Insert(measurementEntity);
                        }
                    });

            
        }

        public List<InstallationModel> LoadInstallations()
        {
            var installationsEnitityData = DBConnection.Table<InstallationEnitity>().ToList();         
            List<InstallationModel> installationModels = new List<InstallationModel>();

            foreach (var installationEntity in installationsEnitityData)
            {
                installationModels.Add(new InstallationModel(installationEntity));
            }
           
            return installationModels;
        }

        public List<MeasurementModel> LoadMeasurements()
        {
            var measurementEnitityData = DBConnection.Query<MeasurementEntity>("Select * From MeasurementEntity");
            List<MeasurementModel> listOfMeasurementModel = new List<MeasurementModel>();

            foreach (var specificMeasurement in measurementEnitityData)
            {
               InstallationEnitity installationEnitity = DBConnection.Get<InstallationEnitity>(specificMeasurement.InstallationId);
               InstallationModel installationModel = new InstallationModel(installationEnitity);
               MeasurementCurrentEntity measurementCurrentEntity = DBConnection.Get<MeasurementCurrentEntity>(specificMeasurement.CurrentId);  
                

                var valuesArray = JArray.Parse(measurementCurrentEntity.Values);
                var indexesArray = JArray.Parse(measurementCurrentEntity.Indexes);
                var standardsArray = JArray.Parse(measurementCurrentEntity.Standards);  
                
                var valuesIds =  DeserializeJsonToIntArray(valuesArray , "Id");
                var indexesIds = DeserializeJsonToIntArray(indexesArray , "Id");
                var standardsIds = DeserializeJsonToIntArray(standardsArray , "Id");

                var measurementValues =  DBConnection.Table<MeasurementValue>().Where(x => valuesIds.Contains(x.Id)).ToList();
                var measurementIndexes = DBConnection.Table<AirQualityIndex>().Where(x => indexesIds.Contains(x.Id)).ToList();
                var measurementStandards = DBConnection.Table<AirQualityStandard>().Where(x => standardsIds.Contains(x.Id)).ToList();


                Current measurementCurrent = new Current(measurementCurrentEntity , measurementValues , measurementIndexes , measurementStandards);     

                MeasurementModel measurementModel = new MeasurementModel(specificMeasurement , installationModel , measurementCurrent);
                listOfMeasurementModel.Add(measurementModel);
            }

            return listOfMeasurementModel;
        }

        private int[] DeserializeJsonToIntArray(JArray jArray , string key)
        {
            JObject JObj;
            int[] JObjKeyNums = new int[jArray.Count];
            for (int i = 0; i < jArray.Count; i++)
            {
                JObj = JObject.Parse(jArray[i].ToString());
                JObjKeyNums[i] = (int)JObj[key];
            }

            return JObjKeyNums;
        }

        public static bool IsDataUpToDateInDB(int maxResults)
        {
            var UTCOneHourAgo = DateTime.Now.ToUniversalTime().AddHours(-1);

            var measurementsDB = App.DBInstantion.LoadMeasurements();

            if (measurementsDB.Count == 0)
                return false;
            else if(measurementsDB.Any(x => x.Current.TillDateTime < UTCOneHourAgo) || measurementsDB.Count  < maxResults)
                return false;

            return true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if(isDisposed)
            {
                return;
            }

            if(isDisposing)
            {
                DBConnection.Dispose();
                DBConnection = null;
            }

            isDisposed = true;
        }
    }
}
