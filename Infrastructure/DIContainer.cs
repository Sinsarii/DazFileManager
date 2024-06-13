using DazFileManager.Services;
using DazFileManager.ViewModels;
using DazFileManager.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DazFileManager.Infrastructure
{
    public class DIContainer
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public static void ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();
            Configure(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        private static void Configure (IServiceCollection services)
        {
            services.AddTransient<IFileScannerService, FileScannerService>();
            services.AddSingleton<FolderCollectionService>();
            services.AddTransient<MainWindow>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<ExtractViewModel>();
            services.AddTransient<ExtractView>();
        }
    }
}
