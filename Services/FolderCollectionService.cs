using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
            //loadDummyData();
            LoadDefaultFolders();
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

        public void loadDummyData()
        {
            FolderCollection_Downloads.Add(@"c:\user\Downloads");
            FolderCollection_Downloads.Add(@"c:\user\Downloads_backup");

            FolderCollection_Archives.Add(@"c:\user\Archives");
            FolderCollection_Archives.Add(@"c:\user\Archives_backup");

            OnPropertyChanged(nameof(FolderCollection_Downloads));
            OnPropertyChanged(nameof(FolderCollection_Extraction));
        }

        private void LoadDefaultFolders()
        {
            //get base directory of application
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            //default folder relative paths

            ////downloads uses the windows downloads folder
            string downloadsFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads";

            ////default folders are located within app file structure at the root
            string extractionFolder = Path.Combine(baseDirectory, "Extraction");
            string archivesFolder = Path.Combine(baseDirectory, "Archives");
            string manifestsFolder = Path.Combine(baseDirectory, "Manifests");
            string workingFolder = Path.Combine(baseDirectory, "Working");

            //Ensure that the folders exist
            EnsureFolderExists(FolderCollection_Downloads, downloadsFolder);
            EnsureFolderExists(FolderCollection_Extraction, extractionFolder);
            EnsureFolderExists(FolderCollection_Archives, archivesFolder);
            EnsureFolderExists(FolderCollection_Manifests, manifestsFolder);
            EnsureFolderExists(FolderCollection_Working, workingFolder);
        }

        // Helper method to ensure a folder exists and add it to the collection if necessary
        private void EnsureFolderExists(ObservableCollection<string> collection, string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            if (!collection.Contains(folderPath))
            {
                collection.Add(folderPath);
                OnPropertyChanged(nameof(collection));
            }
        }

    }
}
