using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordPressXF.Models;
using Xamarin.Forms;

namespace WordPressXF.Services
{
    internal class NavigationService
    {
        private INavigation _navigation;

        private Dictionary<NavigationTarget, Type> _pages = new Dictionary<NavigationTarget, Type>();

        private bool _alreadyNavigating;

        public void Initialize(object navigationPageInstance)
        {
            var navigationPage = navigationPageInstance as NavigationPage;
            _navigation = navigationPage?.Navigation;
        }

        public void Configure(NavigationTarget key, Type type)
        {
            lock (_pages)
            {
                if (_pages.ContainsKey(key))
                    _pages[key] = type;
                else
                    _pages.Add(key, type);
            }
        }

        public async Task NavigateToAsync(NavigationTarget pageKey)
        {
            try
            {
                if (!_alreadyNavigating)
                {
                    _alreadyNavigating = true;

                    var type = _pages[pageKey];
                    var page = Activator.CreateInstance(type) as Page;

                    if (_navigation == null)
                        throw new NullReferenceException("Please initialize the navigation service on the NavigationPage.");

                    _alreadyNavigating = false;
                    await _navigation.PushAsync(page, true);
                }
            }
            catch (NullReferenceException ex)
            {
                _alreadyNavigating = false;
                throw ex;
            }
            catch (Exception ex)
            {
                _alreadyNavigating = false;
                Debug.WriteLine($"{nameof(NavigationService)} | {nameof(NavigateToAsync)} | {ex}");
            }
        }

        public void ClearBackstack()
        {
            if (_navigation == null)
                throw new NullReferenceException("Please initialize the navigation service on the NavigationPage.");

            if (_navigation.NavigationStack.Count > 1)
            {
                var existingPages = _navigation.NavigationStack.ToList();
                existingPages.Reverse();

                foreach (var page in existingPages.Skip(1))
                    _navigation.RemovePage(page);
            }
        }

        public async Task GoBackAsync()
        {
            if (_navigation == null)
                throw new NullReferenceException("Please initialize the navigation service on the NavigationPage.");

            await _navigation.PopAsync(true);
        }
    }
}
