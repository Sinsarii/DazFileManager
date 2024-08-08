using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DazFileManager.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DazFileManager.ViewModels
{

    public class ExtractViewModel : ViewModelBase
    {
        private readonly IFileScannerService _fileScannerService;
        private readonly FolderCollectionService _folderCollectionService;

        private readonly ParallelFileExtractorService _parallelFileExtractorService;
        private readonly FileExtractionService _fileExtractionService;

        public ObservableCollection<FileDetailModel> FileDetails { get; } = new ObservableCollection<FileDetailModel>();


        //public ObservableCollection<string> FolderCollection_Downloads => _folderCollectionService.FolderCollection_Downloads;

        //populate all drop-down menus with their respective collections. should just point to the collection that is generated from the service
        public ObservableCollection<string> FolderCollection_Downloads => _folderCollectionService.FolderCollection_Downloads;
        public ObservableCollection<string> FolderCollection_Archives => _folderCollectionService.FolderCollection_Archives;

        

        // Command that toggles selection
        public ICommand ToggleSelectCommand { get; }

        public ICommand ExtractFilesCommand { get; }


        // Populate listview with file details
        private void LoadFileDetails()
        {
            // Dummy details
            //FileDetails.Add(new FileDetailModel { Filename = "example.zip", Filesize = 1024, DownloadDate = DateTime.Now });
            //FileDetails.Add(new FileDetailModel { Filename = "sample.rar", Filesize = 2048, DownloadDate = DateTime.Now.AddDays(-1) });

            var files = _fileScannerService.ScanFiles(FolderCollection_Downloads[0]);
            foreach(var file in files)
            {
                FileDetails.Add(file);
            }

        }

        // populate folder storage with saved folder favorites if available
        private void LoadFileFavorites()
        {
            _folderCollectionService.FolderCollection_Downloads.Add("C:\\Users\\mikol\\Downloads");
        }
        private void ToggleSelect(object parameter)
        {
            if (parameter is FileDetailModel fileDetail)
            {
                fileDetail.IsSelected = !fileDetail.IsSelected;
            }
        }


        private async Task ExtractFilesAsync()
        {
            var selectedFiles = FileDetails.Where(fd => fd.IsSelected).Select(fd => fd.FilePath).ToList();
            if (selectedFiles.Any())
            {
                await _fileExtractionService.Extract(selectedFiles[0], "C:\\Users\\mikol\\Downloads\\TestOutput");
                //await _parallelFileExtractorService.ExtractFilesInParallelAsync("C:\\Users\\mikol\\Downloads\\TestOutput"); // Replace with actual path
            }
        }


        public ExtractViewModel(
            IFileScannerService fileScannerService, 
            FolderCollectionService folderCollectionService,
            ParallelFileExtractorService parallelFileExtractorService,
            FileExtractionService fileExtractionService)
        {
            _fileScannerService = fileScannerService;
            _folderCollectionService = folderCollectionService;
            _parallelFileExtractorService = parallelFileExtractorService;
            _fileExtractionService = fileExtractionService;
            LoadFileDetails();
            //LoadFileFavorites();
            //lambda expression here to initialize checkbox toggle relay so it can be used. throws an error if you dont initialize it with anything because relaycommand expects an action when intializing. only a problem on initialization. 
            ToggleSelectCommand = new RelayCommand(() => ToggleSelect(null));
            ExtractFilesCommand = new RelayCommand(async () => await ExtractFilesAsync());

            if (ExtractFilesCommand == null)
            {
                throw new InvalidOperationException("ExtractFilesCommand is not initialized.");
            }

        }

        public ExtractViewModel()
        {


        }
    }


}
