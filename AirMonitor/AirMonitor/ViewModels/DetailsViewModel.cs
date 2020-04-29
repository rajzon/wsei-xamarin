using AirMonitor.Models;
using System;

namespace AirMonitor.ViewModels
{
    class DetailsViewModel : BaseViewModel
    {
		private int caqi;
		public int CAQI
		{
			get { return caqi; }
			set {			
				SetProperty(ref caqi, value);
				}
		}

		private string caqiJudge;

		public string CAQIJudge
		{
			get { return caqiJudge; }
			set {
				SetProperty(ref caqiJudge, value);
				}
		}

		private string caqiComment;

		public string CAQIComment
		{
			get { return caqiComment; }
			set { 
				SetProperty(ref caqiComment, value);
				}
		}

		private int pm2_5;
		public int PM2_5
		{
			get { return pm2_5; }
			set { 				
				SetProperty(ref pm2_5, value);
				}
		}

		private int pm10;
		public int PM10
		{
			get { return pm10; }
			set { 
				SetProperty(ref pm10, value);
				}
		}

		private double humidity;

		public double Humidity
		{
			get { return humidity; }
			set { 			
				SetProperty(ref humidity, value);
				}
		}

		private double pressure;

		public double Pressure
		{
			get { return pressure; }
			set { 
				SetProperty(ref pressure, value);
				}
		}

		private int pm2_5Precentage;

		public int PM2_5Precentage
		{
			get { return pm2_5Precentage; }
			set { 				
				SetProperty(ref pm2_5Precentage, value);
				}
		}

		private int pm10Precentage;

		public int PM10Precentage
		{
			get { return pm10Precentage; }
			set {				
				SetProperty(ref pm10Precentage, value);
				}
		}

		public MeasurementModel SelectedItem { get; set; }

		public DetailsViewModel(MeasurementModel selectedItem)
		{
			//Domyślne wartości 
			CAQIJudge = "Brak danych!";
			CAQIComment = "Brak Komentarza spowodowany Brakiem Danych!";
			Pressure = 900;

			SelectedItem = selectedItem;		
			SettingMeasurmentInformations();

		}


		private void SettingMeasurmentInformations()
		{
			CAQI = (int)Math.Round(SelectedItem.Current.Indexes[0].Value);
			CAQIJudge = SelectedItem.Current.Indexes[0].Description;
			CAQIComment = SelectedItem.Current.Indexes[0].Advice;
			PM2_5 = (int)Math.Round(SelectedItem.Current.Values[1].Value);
			PM2_5Precentage = (PM2_5 * 100) / (int)Math.Round(SelectedItem.Current.Standards[0].Limit);
			PM10 = (int)Math.Round(SelectedItem.Current.Values[2].Value);
			PM10Precentage = (PM10 * 100) / (int)Math.Round(SelectedItem.Current.Standards[1].Limit);
			Humidity = (int)Math.Round(SelectedItem.Current.Values[4].Value);
			Pressure = (int)Math.Round(SelectedItem.Current.Values[3].Value);
		}
		
	}
}
