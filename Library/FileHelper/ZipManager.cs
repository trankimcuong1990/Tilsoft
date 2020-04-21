using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace Library.FileHelper
{
    public class ZipManager
    {
        private string TmpFolder = string.Empty;

        public ZipManager(string tmpFolder)
        {
            this.TmpFolder = tmpFolder;
        }

        public string CreateZipFile(List<DTO.ZipFileDTO> toBeZippedFiles)
        {
            string outputFile = this.TmpFolder + System.Guid.NewGuid().ToString().Replace("-", "") + ".zip";
            using (FileStream fs = new FileStream(outputFile, FileMode.CreateNew))
            {
                using (var archive = new ZipArchive(fs, ZipArchiveMode.Create, true))
                {
                    FileInfo fInfo = null;
                    byte[] fileContent = null;
                    foreach (DTO.ZipFileDTO zipInfo in toBeZippedFiles)
                    {
                        if (File.Exists(zipInfo.FilePath))
                        {
                            fInfo = new FileInfo(zipInfo.FilePath);
                            var zipArchiveEntry = archive.CreateEntry(zipInfo.FileName + fInfo.Extension, CompressionLevel.Optimal);
                            using (var zipStream = zipArchiveEntry.Open())
                            {
                                fileContent = File.ReadAllBytes(zipInfo.FilePath);
                                zipStream.Write(fileContent, 0, fileContent.Length);
                            }
                        }                        
                    }
                }
            }
            return outputFile;
        }
    }
}
