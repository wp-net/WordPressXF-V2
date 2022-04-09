using CommunityToolkit.Mvvm.ComponentModel;

namespace WordPressXF.ViewModels
{
    [INotifyPropertyChanged]
    internal partial class BaseViewModel
    {
        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private bool _isRefreshing;
    }
}
