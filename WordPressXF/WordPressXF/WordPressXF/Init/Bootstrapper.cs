﻿using Microsoft.Extensions.DependencyInjection;
using System;
using WordPressXF.Services;
using WordPressXF.ViewModels;

namespace WordPressXF.Init
{
    internal static class Bootstrapper
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceProvider Init()
        {
            var serviceCollection = new ServiceCollection();

            var serviceProvider = serviceCollection
                .ConfigureServices()
                .ConfigureViewModels()
                .BuildServiceProvider();

            ServiceProvider = serviceProvider;

            return serviceProvider;
        }

        private static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            // add services
            services.AddSingleton(CreateNavigationService());

            services.AddSingleton<DialogService>();
            services.AddSingleton<SettingsService>();
            services.AddSingleton<WordPressService>();

            return services;
        }

        private static IServiceCollection ConfigureViewModels(this IServiceCollection services)
        {
            // add viewmodels
            services.AddSingleton<AccountViewModel>();
            services.AddSingleton<LoadingViewModel>();
            services.AddSingleton<PostsViewModel>();

            return services;
        }

        private static NavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();

            return navigationService;
        }
    }
}