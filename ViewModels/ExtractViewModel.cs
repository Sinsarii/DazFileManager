using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DazFileManager.ViewModels
{

    public class ExtractViewModel : ViewModelBase
    {
        public ObservableCollection<FileDetailModel> FileDetails { get; } = new ObservableCollection<FileDetailModel>();

        // Command that toggles selection
        public ICommand ToggleSelectCommand { get; }

        // Populate listview with file details
        private void LoadFileDetails()
        {
            // Dummy details
            FileDetails.Add(new FileDetailModel { Filename = "example.zip", Filesize = 1024, DownloadDate = DateTime.Now });
            FileDetails.Add(new FileDetailModel { Filename = "sample.rar", Filesize = 2048, DownloadDate = DateTime.Now.AddDays(-1) });
        }

        private void ToggleSelect(object parameter)
        {
            if (parameter is FileDetailModel fileDetail)
            {
                fileDetail.IsSelected = !fileDetail.IsSelected;
            }
        }

        public ExtractViewModel()
        {
            LoadFileDetails();
            //lambda expression here to initialize the relay so it can be used. throws an error if you dont initialize it with anything because relay expects an action when intializing. only a problem on initialization. 
            ToggleSelectCommand = new RelayCommand(() => ToggleSelect(null));
        }
    }


}
