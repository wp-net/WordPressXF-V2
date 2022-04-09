using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressXF.Common;
using WordPressXF.Services;

namespace WordPressXF.ViewModels
{
    [INotifyPropertyChanged]
    internal partial class AccountViewModel : BaseViewModel
    {
        private readonly SettingsService _settingsService;
        private readonly WordPressService _wordPressService;

        [ObservableProperty]
        private bool _isCurrentlyLoggingIn;

        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private User _currentUser;


        public AccountViewModel(SettingsService settingsService, WordPressService wordPressService)
        {
            _settingsService = settingsService;
            _wordPressService = wordPressService;
        }

        [ICommand(AllowConcurrentExecutions = false)]
        private async Task TryAutoLoginAsync()
        {
            var username = await _settingsService.GetAsync(Statics.UsernameSettingsKey);
            var password = await _settingsService.GetAsync(Statics.PasswordSettingsKey);

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return;

            var user = await _wordPressService.LoginAsync(username, password);

            if (user != null)
                CurrentUser = user;
        }

        [ICommand(CanExecute = nameof(CanLogin), AllowConcurrentExecutions = false)]
        private async Task LoginAsync()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                return;

            IsCurrentlyLoggingIn = true;

            var user = await _wordPressService.LoginAsync(Username, Password);
            if (user != null)
            {
                await _settingsService.SetAsync(Statics.UsernameSettingsKey, Username);
                await _settingsService.SetAsync(Statics.PasswordSettingsKey, Password);

                CurrentUser = user;
            }

            IsCurrentlyLoggingIn = false;
        }

        [ICommand(AllowConcurrentExecutions = false)]
        private void Logout()
        {
            _wordPressService.Logout();

            CurrentUser = null;
            Username = null;
            Password = null;

            _settingsService.Remove(Statics.UsernameSettingsKey);
            _settingsService.Remove(Statics.PasswordSettingsKey);
        }

        private bool CanLogin()
        {
            return !IsCurrentlyLoggingIn && !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }
    }
}
