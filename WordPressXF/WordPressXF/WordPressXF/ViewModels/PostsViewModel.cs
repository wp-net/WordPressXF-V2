using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressXF.Common;
using WordPressXF.Models;
using WordPressXF.Resources;
using WordPressXF.Services;

namespace WordPressXF.ViewModels
{
    [INotifyPropertyChanged]
    internal partial class PostsViewModel : BaseViewModel
    {
        private readonly DialogService _dialogService;
        private readonly NavigationService _navigationService;
        private readonly WordPressService _wordPressService;

        private int _currentPage = -1;

        [ObservableProperty]
        private ObservableCollection<Post> _posts = new();

        [ObservableProperty]
        private List<CommentThreaded> _comments;

        [ObservableProperty]
        private Post _selectedPost;

        [ObservableProperty]
        private bool _isIncrementalLoading;

        [ObservableProperty]
        private string _commentText;

        [ObservableProperty]
        private bool _isCommenting = false;


        public PostsViewModel(DialogService dialogService, NavigationService navigationService, WordPressService wordPressService)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;
            _wordPressService = wordPressService;
        }

        [ICommand(AllowConcurrentExecutions = false)]
        private async Task LoadPostsAsync()
        {
            try
            {
                IsRefreshing = true;

                _currentPage = 0;

                Posts.Clear();

                var posts = await _wordPressService.GetLatestPostsAsync(_currentPage, Statics.PageSize);
                Posts.AddRange(posts);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{nameof(PostsViewModel)} | {nameof(LoadPostsAsync)} | {ex}");
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        [ICommand(AllowConcurrentExecutions = false)]
        private async Task LoadMorePostsAsync()
        {
            if (IsIncrementalLoading)
                return;

            try
            {
                IsIncrementalLoading = true;

                _currentPage++;

                var posts = await _wordPressService.GetLatestPostsAsync(_currentPage, Statics.PageSize);

                if (posts == null)
                    return;

                Posts.AddRange(posts);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{nameof(PostsViewModel)} | {nameof(LoadMorePostsAsync)} | {ex}");
            }
            finally
            {
                IsIncrementalLoading = false;
            }
        }

        [ICommand(AllowConcurrentExecutions = false)]
        private async Task SetSelectedPostASync(Post selectedPost)
        {
            try
            {
                IsLoading = true;

                Comments = null;
                CommentText = null;

                SelectedPost = selectedPost;
                await _navigationService.NavigateToAsync(NavigationTarget.PostDetailOverviewPage);

                await GetCommentsAsync(selectedPost.Id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{nameof(PostsViewModel)} | {nameof(SetSelectedPostASync)} | {ex}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        [ICommand(AllowConcurrentExecutions = false)]
        private async Task PostCommentAsync()
        {
            try
            {
                IsCommenting = true;

                if (await _wordPressService.IsUserAuthenticatedAsync())
                {
                    var comment = await _wordPressService.PostCommentAsync(SelectedPost.Id, CommentText);
                    if (comment != null)
                    {
                        CommentText = null;
                        await GetCommentsAsync(SelectedPost.Id);
                    }
                }
                else
                {
                    await _dialogService.OpenSimplePlatformDialogAsync(AppResources.CommentDialogNotAuthorizedTitle, AppResources.CommentDialogNotAuthorizedMessage, AppResources.DialogOk);
                }
            }
            finally
            {
                IsCommenting = false;
            }
        }

        [ICommand(AllowConcurrentExecutions = false)]
        private async Task ShowAccountAsync()
        {
            await _navigationService.NavigateToAsync(NavigationTarget.AccountPage);
        }

        [ICommand(CanExecute = nameof(CanPostComment), AllowConcurrentExecutions = false)]
        private async Task GetCommentsAsync(int id)
        {
            IsLoading = true;

            Comments = await _wordPressService.GetCommentsForPostAsync(id);

            IsLoading = false;
        }

        private bool CanPostComment()
        {
            return !string.IsNullOrEmpty(CommentText) && !IsCommenting;
        }
    }
}
