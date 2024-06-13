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
        public ObservableCollection<string> FolderCollection_Downloads { get; set; }
        public ObservableCollection<string> FolderCollection_Extraction { get; set; }
        public ObservableCollection<string> FolderCollection_Archives {  get; set; }
        public ObservableCollection<string> FolderCollection_Manifests {  get; set; }
        public ObservableCollection<string> FolderCollection_Working {  get; set; }

        public FolderCollectionService()
        {
            FolderCollection_Downloads = new ObservableCollection<string>();
            FolderCollection_Extraction = new ObservableCollection<string>();
            FolderCollection_Archives = new ObservableCollection<string>();
            FolderCollection_Manifests = new ObservableCollection<string>();
            FolderCollection_Working = new ObservableCollection<string>();
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
