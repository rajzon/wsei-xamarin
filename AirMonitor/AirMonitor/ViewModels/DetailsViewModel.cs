using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AirMonitor.ViewModels
{
    class DetailsViewModel : INotifyPropertyChanged
    {
		private int caqi;
		public int CAQI
		{
			get { return caqi; }
			set {
				caqi = value;
				OnPropertyChanged("CAQI"); 
				}
		}

		private string caqiJudge;

		public string CAQIJudge
		{
			get { return caqiJudge; }
			set {
				caqiJudge = value;
				OnPropertyChanged("CAQIJudge");
				}
		}

		private string caqiComment;

		public string CAQIComment
		{
			get { return caqiComment; }
			set { 
				caqiComment = value;
				OnPropertyChanged("CAQIComment");
				}
		}

		private int pm2_5;

		public int PM2_5
		{
			get { return pm2_5; }
			set { 
				pm2_5 = value;
				OnPropertyChanged("PM2_5");					
				}
		}

		private int pm10;

		public int PM10
		{
			get { return pm10; }
			set { 
				pm10 = value;
				OnPropertyChanged("PM10");
				}
		}

		private double humidity;

		public double Humidity
		{
			get { return humidity; }
			set { 
				humidity = value;
				OnPropertyChanged("Humidity");
				}
		}

		private double pressure;

		public double Pressure
		{
			get { return pressure; }
			set { 
				pressure = value;
				OnPropertyChanged("Pressure");
				}
		}

		private int pm2_5Precentage;

		public int PM2_5Precentage
		{
			get { return pm2_5Precentage; }
			set { 
				pm2_5Precentage = value;
				OnPropertyChanged("PM2_%Precentage");
				}
		}

		private int pm10Precentage;

		public int PM10Precentage
		{
			get { return pm10Precentage; }
			set { 
				pm10Precentage = value;
				OnPropertyChanged("PM10Precentage");
			}
		}



		public event PropertyChangedEventHandler PropertyChanged;

		public DetailsViewModel()
		{
			//Domyślne wartości 
			CAQIJudge = "Brak danych!";
			CAQIComment = "Brak Komentarza spowodowany Brakiem Danych!";
			Pressure = 900;			
			
		}

		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
