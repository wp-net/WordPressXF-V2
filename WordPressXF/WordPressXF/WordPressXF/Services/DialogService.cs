using System.Threading.Tasks;
using Xamarin.Forms;

namespace WordPressXF.Services
{
    internal class DialogService
    {
        public async Task OpenSimplePlatformDialogAsync(string title, string message, string accept)
        {
            await Application.Current?.MainPage?.DisplayAlert(title, message, accept);
        }

        public async Task<bool> OpenSimplePlatformDialogAsync(string title, string message, string accept, string cancel)
        {
            return await Application.Current?.MainPage?.DisplayAlert(title, message, accept, cancel);
        }
    }
}
