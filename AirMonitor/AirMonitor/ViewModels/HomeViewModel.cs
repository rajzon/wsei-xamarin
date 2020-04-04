using AirMonitor.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AirMonitor.ViewModels
{
    class HomeViewModel
    {
        public INavigation HomeNav { get; set; }
        public ICommand ChangePageCommand => new Command(ChangePage);

        
        public HomeViewModel(INavigation homeNav)
        {
            HomeNav = homeNav;
        }

        async private void ChangePage(object obj)
        {
            await HomeNav.PushAsync(new DetailsPage());
        }
    }
}
