using WordPressXF.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WordPressXF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingPage : ContentPage
    {
        private readonly LoadingViewModel _viewModel;

        public LoadingPage()
        {
            InitializeComponent();

            _viewModel = (LoadingViewModel)BindingContext;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await _viewModel.InitCommand.ExecuteAsync(null);
        }
    }
}