using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DazFileManager.Services
{
    public class ParallelFileExtractorService
    {

        private readonly IServiceProvider _serviceProvider;
        private readonly SemaphoreSlim _semaphore;
        private readonly ObservableCollection<string> _fileCollection; //given by folder collection service
        private string _dazContentFolderPath;
        private CancellationToken _cancellationToken;

        public ParallelFileExtractorService(IServiceProvider serviceProvider, ObservableCollection<string> fileCollection, int maxDegreeOfParallelism)
        {
            _serviceProvider = serviceProvider;
            _fileCollection = fileCollection;
            _semaphore = new SemaphoreSlim(maxDegreeOfParallelism);
            _fileCollection.CollectionChanged += FileCollection_CollectionChanged;
        }

        private void FileCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.NewItems != null)
            {
                foreach (string filePath in e.NewItems)
                {
                    //start the extraction process for the newly added files
                    _ = ExtractWithSemaphoreAsync(filePath, _dazContentFolderPath, _cancellationToken);
                }
            }
        }

        public async Task ExtractFilesInParallelAsync(ObservableCollection<string> fileCollection, string dazContentFolderPath, CancellationToken cancellationToken = default)
        {
            _dazContentFolderPath = dazContentFolderPath;
            _cancellationToken = cancellationToken;

            var tasks = fileCollection.Select(filePath => ExtractWithSemaphoreAsync(filePath, dazContentFolderPath, cancellationToken));
            await Task.WhenAll(tasks);
        }
        public async Task ExtractWithSemaphoreAsync(string filePath, string dazContentFolderPath, CancellationToken cancellationToken = default)
        {
            await _semaphore.WaitAsync(cancellationToken);
            try
            {
                var extractor = _serviceProvider.GetRequiredService<FileExtractionService>();
                await extractor.Extract(filePath, dazContentFolderPath);
            }
            finally
            {

            _semaphore.Release(); 
            }
        }

    }
}
