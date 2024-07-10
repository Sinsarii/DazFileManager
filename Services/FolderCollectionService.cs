using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DazFileManager.Services
{
    public class FolderCollectionService : INotifyPropertyChanged
    {
        private readonly AppSettings _appSettings;
        public ObservableCollection<string> FolderCollection_Downloads => _appSettings.FolderCollection_Downloads;
        public ObservableCollection<string> FolderCollection_Extraction => _appSettings.FolderCollection_Extraction;
        public ObservableCollection<string> FolderCollection_Archives => _appSettings.FolderCollection_Archives;
        public ObservableCollection<string> FolderCollection_Manifests => _appSettings.FolderCollection_Manifests;
        public ObservableCollection<string> FolderCollection_Working => _appSettings.FolderCollection_Working;

        public FolderCollectionService(AppSettings appSettings)
        {
            _appSettings = appSettings;
            //LoadDefaultFolders();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Collection Manipulation
        public void AddFolder(ObservableCollection<string> collection, string folderPath)
        {
            if (!collection.Contains(folderPath))
            {
                collection.Add(folderPath);
                OnPropertyChanged(nameof(collection));
            }

        }

        public void RemoveFolder(ObservableCollection<string> collection, string folderPath)
        {
            if (collection.Contains(folderPath))
            {
                collection.Remove(folderPath);
                OnPropertyChanged(nameof(collection));
            }
        }

    }
}
