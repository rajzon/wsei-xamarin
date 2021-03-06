﻿using AirMonitor.Models;
using AirMonitor.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace AirMonitor.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class DetailsPage : ContentPage
    {
        public DetailsPage(MeasurementModel selectedItem)
        {
            InitializeComponent();
            BindingContext = new DetailsViewModel(selectedItem);
        }
     

        private void Help_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Co to jest CAQI?", "Lorem ipsum.", "Zamknij");
        }
    }
}
