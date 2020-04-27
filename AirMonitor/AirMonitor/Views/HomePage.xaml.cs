﻿using AirMonitor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AirMonitor.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel(this.Navigation);
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            var viewmodel = (HomeViewModel)this.BindingContext;
            viewmodel.GoToDetailsCommand.Execute(e.Item);
            

        }

        /*
       async private void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DetailsPage());
        }
       */

    }
}