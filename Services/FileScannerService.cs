using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DazFileManager.ViewModels;

namespace DazFileManager.Services
{
    public class FileScannerService : IFileScannerService
    {
        public IEnumerable<FileDetailModel> ScanFiles (string folderPath)
        {
            var files = Directory.GetFiles(folderPath);
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                yield return new FileDetailModel
                {
                    Filename = fileInfo.Name,
                    Filesize = fileInfo.Length,
                    DownloadDate = fileInfo.CreationTime
                };

            }
        }
    }
}
