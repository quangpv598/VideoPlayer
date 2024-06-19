using FlyleafLib;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SolarVPN.App;
using System.Configuration;
using System.Data;
using System.Windows;
using VideoPlayer.ViewModels;

namespace VideoPlayer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder().ConfigureServices(StartUp.Configure).Build();
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();
            Configure(AppHost.Services);
            base.OnStartup(e);
        }

        private static void Configure(IServiceProvider service)
        {
            var mainWindow = service.GetRequiredService<MainWindow>();
            var windowViewModel = service.GetRequiredService<MainWindowViewModel>();
            mainWindow.DataContext = windowViewModel;
            mainWindow.Show();
        }
    } 
}
