using WordPressXF.Resources;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;

namespace WordPressXF
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            LocalizationResourceManager.Current.Init(AppResources.ResourceManager);

            MainPage = new MainPage();
        }

        protected override void OnStart()
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
