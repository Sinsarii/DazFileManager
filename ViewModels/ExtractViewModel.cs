using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DazFileManager.ViewModels
{

    public class ExtractViewModel : ViewModelBase
    {
        public ObservableCollection<FileDetailModel> FileDetails { get; } = new ObservableCollection<FileDetailModel>();

        //populate listview with file details
        private void LoadFileDetails()
        {
            //dummy details
            FileDetails.Add(new FileDetailModel { Filename = "example.zip", Filesize = 1024, DownloadDate = DateTime.Now });
            FileDetails.Add(new FileDetailModel { Filename = "sample.rar", Filesize = 2048, DownloadDate = DateTime.Now.AddDays(-1) });

        }

        public ExtractViewModel()
        {
            LoadFileDetails();
        }
    }
}
