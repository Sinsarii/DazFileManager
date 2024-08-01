using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DazFileManager.Services
{
    public class FileExtractionService
    {
        public string currentFolder;

        public string outputFolder;

        public string workingFolder;

        public async Task Extract(string zipFilePath, string dazContentFolderPath)
        {
            //create tempporary working directory, will be able to specify later
            string tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDir);

            try
            {
                await ZipExtract(zipFilePath, tempDir, dazContentFolderPath);
            }
            finally
            {
                Directory.Delete(tempDir, true);
            }

            //make sure list has items in it

            //iterate through each item of list
            ///call correct extraction method based on if it is a rar or zip
            ///
        }

        private async Task ZipExtract(string zipFilePath, string tempDir, string contentFolderPath)
        {
            await Task.Run(() => ZipFile.ExtractToDirectory(zipFilePath, tempDir));

            foreach (var file in Directory.GetFiles(tempDir))
            {
                if (Path.GetExtension(file).Equals(".zip", StringComparison.OrdinalIgnoreCase))
                {
                    string nestedTempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                    Directory.CreateDirectory(nestedTempDirectory);

                    await ZipExtract(file, nestedTempDirectory, contentFolderPath);
                }
                else
                {
                    MoveFileToDazContentFolder(file, contentFolderPath);
                }
            }

            foreach (var directory in Directory.GetFiles(tempDir))
            {
                MoveDirectoryContents(directory, contentFolderPath);
            }
        }

        private void MoveFileToDazContentFolder(string file, string contentFolderPath)
        {
            string destinationPath = Path.Combine(contentFolderPath, Path.GetFileName(file));
            File.Move(file, destinationPath);
        }

        private void MoveDirectoryContents(string sourceDirectory, string destinationDirectory)
        {
            foreach (var file in Directory.GetFiles(sourceDirectory))
            {
                MoveFileToDazContentFolder((string)file, destinationDirectory);
            }

            foreach (var directory in Directory.GetDirectories(sourceDirectory))
            {
                string destinationSubDirectory = Path.Combine(destinationDirectory, Path.GetFileName(directory));
                if(!Directory.Exists(destinationSubDirectory))
                {
                    Directory.CreateDirectory(destinationSubDirectory);
                }
                MoveDirectoryContents((string)directory, destinationSubDirectory);
            }
        }
    }
}
