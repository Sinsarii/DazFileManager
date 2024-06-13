using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DazFileManager.Services;

namespace DazFileManager.ViewModels
{

    public class ExtractViewModel : ViewModelBase
    {
        private readonly IFileScannerService _fileScannerService;
        private readonly FolderCollectionService _folderCollectionService;
        public ObservableCollection<FileDetailModel> FileDetails { get; } = new ObservableCollection<FileDetailModel>();

        public ObservableCollection<string> FolderCollection_Downloads => _folderCollectionService.FolderCollection_Downloads;


        // Command that toggles selection
        public ICommand ToggleSelectCommand { get; }

        // Populate listview with file details
        private void LoadFileDetails()
        {
            // Dummy details
            //FileDetails.Add(new FileDetailModel { Filename = "example.zip", Filesize = 1024, DownloadDate = DateTime.Now });
            //FileDetails.Add(new FileDetailModel { Filename = "sample.rar", Filesize = 2048, DownloadDate = DateTime.Now.AddDays(-1) });

            var files = _fileScannerService.ScanFiles("C:\\Users\\mikol\\Downloads");
            foreach(var file in files)
            {
                FileDetails.Add(file);
            }
        }

        private void ToggleSelect(object parameter)
        {
            if (parameter is FileDetailModel fileDetail)
            {
                fileDetail.IsSelected = !fileDetail.IsSelected;
            }
        }

        public ExtractViewModel(IFileScannerService fileScannerService, FolderCollectionService folderCollectionService)
        {
            _fileScannerService = fileScannerService;
            _folderCollectionService = folderCollectionService;
            LoadFileDetails();
            //lambda expression here to initialize checkbox toggle relay so it can be used. throws an error if you dont initialize it with anything because relaycommand expects an action when intializing. only a problem on initialization. 
            ToggleSelectCommand = new RelayCommand(() => ToggleSelect(null));
        }

        public ExtractViewModel()
        {


        }
    }


}
