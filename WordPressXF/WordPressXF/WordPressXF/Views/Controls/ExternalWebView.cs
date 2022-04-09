using Xamarin.Essentials;
using Xamarin.Forms;

namespace WordPressXF.Views.Controls
{
    internal class ExternalWebView : WebView
    {
        public ExternalWebView()
        {
            Navigating += ExternalWebViewOnNavigating;
        }

        private async void ExternalWebViewOnNavigating(object sender, WebNavigatingEventArgs e)
        {
            // check that Url is available
            if (e == null || string.IsNullOrEmpty(e.Url))
                return;

            // check if URL doesn't start with http or https
            if (!e.Url.StartsWith("http") || !e.Url.StartsWith("https"))
                return;

            // check for youtube link
            if (e.Url.StartsWith("https://www.youtube.com/"))
                return;

            // check for twitter link
            if (e.Url.StartsWith("https://syndication.twitter.com/") || e.Url.StartsWith("https://platform.twitter.com"))
                return;

            // cancel WebView navigation
            e.Cancel = true;

            // open external browser to show website
            await Browser.OpenAsync(e.Url, BrowserLaunchMode.SystemPreferred);
        }
    }
}
