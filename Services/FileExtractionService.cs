using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace DazFileManager.Services
{
    public class FileExtractionService
    {
        public string currentFolder;

        public string workingFolder = "C:\\Users\\mikol\\Downloads\\TestWorking";

        public async Task Extract(string zipFilePath, string dazContentFolderPath)
        {
            //create tempporary working directory, will be able to specify later
            //string tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            string tempDir = workingFolder;
            Directory.CreateDirectory(tempDir);

            if (!Directory.Exists(dazContentFolderPath))
            {
                Directory.CreateDirectory(dazContentFolderPath);
            }

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

        //Expected Behavior

        //*
        //Extract Held Zip, this task thread can't be made without it
        //Move into Extracted Folder
        //Check if directory contains 'Runtime' folder
        //If Yes, move everything in this directory level and lower that is not a ZIPPED file
        //If Not, Loop again and extract Zip
        //*//
        private async Task ZipExtract(string zipFilePath, string tempDir, string contentFolderPath)
        {
            if(zipFilePath != null)
            {
                await Task.Run(() => ZipFile.ExtractToDirectory(zipFilePath, tempDir));
            }

            //case insensitive check to make sure 'runtime' folder exists
            bool runtimeFolderExists = Directory.EnumerateDirectories(tempDir).Any(dir => string.Equals(Path.GetFileName(dir), "runtime", StringComparison.OrdinalIgnoreCase));

            if (runtimeFolderExists)
            {
                // If the 'runtime' folder exists, we are at the correct directory level and move everything at that level or lower
                MoveDirectoryContents(tempDir, contentFolderPath);
                return;
            }
            else
            {
                foreach (var file in Directory.GetFiles(tempDir))
                {
                    if (Path.GetExtension(file).Equals(".zip", StringComparison.OrdinalIgnoreCase))
                    {
                        //string nestedTempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                        string nestedTempDirectory = Path.Combine(tempDir, Path.ChangeExtension(file, null));
                        Directory.CreateDirectory(nestedTempDirectory);

                        await ZipExtract(file, nestedTempDirectory, contentFolderPath);
                    }
                    //else
                    //{
                    //    MoveFileToDazContentFolder(file, contentFolderPath);
                    //}
                }

                //foreach (var directory in Directory.GetDirectories(tempDir))
                //{
                //    MoveDirectoryContents(directory, contentFolderPath);
                //}

                //There are no more zipped files, now we check all folders
            }
            foreach (var directory in Directory.GetDirectories(tempDir))
            {
               await ZipExtract(null,directory, contentFolderPath);
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
