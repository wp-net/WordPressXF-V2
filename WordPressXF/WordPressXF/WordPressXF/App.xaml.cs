using Microsoft.Extensions.DependencyInjection;
using WordPressXF.Init;
using WordPressXF.Resources;
using WordPressXF.Services;
using WordPressXF.Views;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;

namespace WordPressXF
{
    public partial class App : Application
    {
        public App()
        {
            Bootstrapper.Init();

            InitializeComponent();

            LocalizationResourceManager.Current.Init(AppResources.ResourceManager);

            MainPage = new NavigationPage(new LoadingPage());
            Bootstrapper.ServiceProvider.GetService<NavigationService>().Initialize(MainPage);
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
