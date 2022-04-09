using System.Threading.Tasks;
using Xamarin.Essentials;

namespace WordPressXF.Services
{
    internal class SettingsService
    {
        public async Task<string> GetAsync(string key)
        {
            return await SecureStorage.GetAsync(key);
        }

        public async Task SetAsync(string key, string value)
        {
            await SecureStorage.SetAsync(key, value);
        }

        public void Remove(string key)
        {
            SecureStorage.Remove(key);
        }
    }
}
