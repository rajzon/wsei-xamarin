using AirMonitor.API;
using AirMonitor.Helpers;
using Xamarin.Forms;


namespace AirMonitor
{
    public partial class App : Application 
    {
        public static DatabaseHelper DBInstantion { get; set; }

        public App()
        {
            InitializeComponent();
            ConfigAPI();
            DBInstantion = new DatabaseHelper();
            DBInstantion.InitializeDBEntities();
            
            MainPage = new NavigationPage(new TabbedPage1());
        }

        private static void ConfigAPI()
        {
            APIHelper.GetAPIConfig();
            APIHelper.InitializeClient();
        }
 
        protected  override void OnStart()
        {
            if(DBInstantion == null)
            {
                DBInstantion = new DatabaseHelper();
                DBInstantion.InitializeDBEntities();
            }

        }

        protected override void OnSleep()
        {
            DBInstantion?.Dispose();
            DBInstantion = null;
        }

        protected override void OnResume()
        {
            if (DBInstantion == null)
            {
                DBInstantion = new DatabaseHelper();
                DBInstantion.InitializeDBEntities();
            }
        }
    }
}
