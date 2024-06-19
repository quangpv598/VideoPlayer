using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System;
using VideoPlayer.ViewModels;
using VideoPlayer;

namespace SolarVPN.App
{
    public static class StartUp
    {
        public static void Configure(IServiceCollection services)
        {
            services.ConfigureRepositories();
            services.ConfigureServices();
            services.ConfigureViewModels();
            services.ConfigureViews();
        }

        private static void ConfigureViewModels(this IServiceCollection services)
        {
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindowViewModel>();
        }

        private static void ConfigureViews(this IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
        }

        private static void ConfigureServices(this IServiceCollection services)
        {
            
        }

        private static void ConfigureRepositories(this IServiceCollection services)
        {
            
        }
    }
}
