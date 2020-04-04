using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirMonitor.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;


namespace AirMonitor
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPage1 : Xamarin.Forms.TabbedPage
    {
        public TabbedPage1()
        {
            InitializeComponent();

            /*
            NavigationPage navigationPage = new NavigationPage(new HomePage());
            navigationPage.IconImageSource = "home.png";
            navigationPage.Title = "Home";

            Children.Add(new SettingsPage());
            Children.Add(navigationPage);
            */
           
            On<Android>().SetToolbarPlacement(Xamarin.Forms.PlatformConfiguration.AndroidSpecific.ToolbarPlacement.Bottom);
        }
       
    }
}