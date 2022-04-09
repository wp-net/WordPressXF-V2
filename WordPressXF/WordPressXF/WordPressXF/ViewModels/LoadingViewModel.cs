using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WordPressXF.Models;
using WordPressXF.Services;

namespace WordPressXF.ViewModels
{
    [INotifyPropertyChanged]
    internal partial class LoadingViewModel : BaseViewModel
    {
        private readonly NavigationService _navigationService;
        private readonly AccountViewModel _accountViewModel;
        private readonly PostsViewModel _postsViewModel;


        public LoadingViewModel(NavigationService navigationService, AccountViewModel accountViewModel, PostsViewModel postsViewModel)
        {
            _navigationService = navigationService;
            _accountViewModel = accountViewModel;
            _postsViewModel = postsViewModel;
        }


        [ICommand(AllowConcurrentExecutions = false)]
        private async Task InitAsync()
        {
            IsLoading = true;

            try
            {
                await _accountViewModel.TryAutoLoginCommand.ExecuteAsync(null);
                await _postsViewModel.LoadMorePostsCommand.ExecuteAsync(null);

                await _navigationService.NavigateToAsync(NavigationTarget.PostsOverviewPage);
                _navigationService.ClearBackstack();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{nameof(LoadingViewModel)} | {nameof(InitAsync)} | {ex}");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
