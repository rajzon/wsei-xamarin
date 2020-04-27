using System;
using AirMonitor.Views;
using AirMonitor.API;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using System.Reflection;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using AirMonitor.ViewModels;
using AirMonitor.Helpers;
using Xamarin.Essentials;
using System.Threading.Tasks;
using Java.Lang;
using Nito.AsyncEx;
using System.Xml.Serialization;

namespace AirMonitor
{
    public partial class App : Application 
    {

        public App()
        {
            InitializeComponent();          
            APIHelper.GetAPIConfig();        
            APIHelper.InitializeClient();
            
            MainPage = new NavigationPage(new TabbedPage1());
        }
 
        protected  override void OnStart()
        {
           
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
