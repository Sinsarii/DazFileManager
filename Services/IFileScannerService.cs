using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DazFileManager.Services
{
    public interface IFileScannerService
    {
        IEnumerable<FileDetailModel> ScanFiles(string folderPath);
    }
}
