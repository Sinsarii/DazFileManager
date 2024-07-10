using DazFileManager.Infrastructure;
using DazFileManager.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace DazFileManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
         protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            DIContainer.ConfigureServices();

            var mainWindow = DIContainer.ServiceProvider.GetService<MainWindow>();
            var mainViewModel = DIContainer.ServiceProvider.GetRequiredService<MainViewModel>();

            //mainWindow.DataContext = mainViewModel;
            //mainWindow.Show();

            this.Exit += OnExit;
        }

        private void OnExit(object sender, EventArgs e)
        {
            var appSettings = DIContainer.ServiceProvider.GetRequiredService<AppSettings>();
            appSettings.Save("appSettings.json");
        }
    }
}
