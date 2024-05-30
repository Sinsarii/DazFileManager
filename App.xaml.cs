using DazFileManager.Infrastructure;
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

            //var mainWindow = DIContainer.ServiceProvider.GetRequiredService<MainWindow>();
            //mainWindow.Show();
        }
    }

}
