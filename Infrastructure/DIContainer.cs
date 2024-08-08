using DazFileManager.Services;
using DazFileManager.ViewModels;
using DazFileManager.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            // Resolve the ExtractViewModel separately and populate the services
            var extractViewModel = ServiceProvider.GetRequiredService<ExtractViewModel>();

            // Register ParallelFileExtractorService with the required parameters
            serviceCollection.AddSingleton<ParallelFileExtractorService>(provider =>
            {
                var fileCollection = new ObservableCollection<string>(extractViewModel.FileDetails.Select(fd => fd.Filename));
                int maxDegreeOfParallelism = 4; // Set appropriate value
                return new ParallelFileExtractorService(provider, fileCollection, maxDegreeOfParallelism);
            });

            // Rebuild the service provider to include the new registrations
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        private static void Configure (IServiceCollection services)
        {
            var appSettings = AppSettings.Load("appSettings.json");
            services.AddSingleton(appSettings);

            services.AddSingleton<IFileScannerService, FileScannerService>();
            services.AddSingleton<FolderCollectionService>();

            //services.AddTransient<MainViewModel>();
            //services.AddTransient<ExtractViewModel>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainViewModel>();

            services.AddSingleton<ExtractViewModel>();
            services.AddSingleton<ExtractView>();

            services.AddTransient<FileExtractionService>(); // Changed to Transient


        }
    }
}
