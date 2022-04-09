using Microsoft.Extensions.DependencyInjection;
using WordPressXF.ViewModels;

namespace WordPressXF.Init
{
    internal class ViewModelLocator
    {
        public AccountViewModel AccountViewModel
            => Bootstrapper.ServiceProvider.GetService<AccountViewModel>();

        public LoadingViewModel LoadingViewModel
            => Bootstrapper.ServiceProvider.GetService<LoadingViewModel>();

        public PostsViewModel PostsViewModel
            => Bootstrapper.ServiceProvider.GetService<PostsViewModel>();
    }
}
