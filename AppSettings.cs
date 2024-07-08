using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DazFileManager
{
    public class AppSettings
    {
        public ObservableCollection<string> FolderCollection_Downloads { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> FolderCollection_Extraction { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> FolderCollection_Archives { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> FolderCollection_Manifests { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> FolderCollection_Working { get; set; } = new ObservableCollection<string>();

        public void Save(string filePath)
        {
            var jsonString = JsonSerializer.Serialize(this);
            File.WriteAllText(filePath, jsonString);
        }

        public static AppSettings Load(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new AppSettings();
            }

            var jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<AppSettings>(jsonString);
        }
    }
}
