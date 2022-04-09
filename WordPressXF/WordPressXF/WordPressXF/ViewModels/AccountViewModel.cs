using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressXF.Common;
using WordPressXF.Services;
using Xamarin.Forms;

namespace WordPressXF.ViewModels
{
    internal partial class AccountViewModel : BaseViewModel
    {
        private readonly SettingsService _settingsService;
        private readonly WordPressService _wordPressService;

        [ObservableProperty]
        private bool _isCurrentlyLoggingIn;

        [ObservableProperty]
        [AlsoNotifyCanExecuteFor(nameof(LoginCommand))]
        private string _username;

        [ObservableProperty]
        [AlsoNotifyCanExecuteFor(nameof(LoginCommand))]
        private string _password;

        [ObservableProperty]
        private User _currentUser;

        [ObservableProperty]
        private Color _avatarBackgroundColor;

        [ObservableProperty]
        private Color _avatarTextColor;

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
            {
                CurrentUser = user;
                SetAvatarColors(CurrentUser.Name);
            }
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
                SetAvatarColors(CurrentUser.Name);
            }

            IsCurrentlyLoggingIn = false;
        }

        [ICommand]
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

        private void SetAvatarColors(string name)
        {
            // get color for the provided text
            var hexColor = "#FF" + Convert.ToString(name.GetHashCode(), 16).Substring(0, 6);

            // fix issue if value is too short
            if (hexColor.Length == 8)
                hexColor += "5";

            // create color from hex value
            var color = Color.FromHex(hexColor);

            // set backgroundcolor of contentboxview
            AvatarBackgroundColor = color;

            // get brightness and set textcolor
            var brightness = color.R * .3 + color.G * .59 + color.B * .11;
            AvatarTextColor = brightness < 0.5 ? Color.White : Color.Black;
        }
    }
}
