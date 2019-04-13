using System.Collections.Generic;
using System.IO;
using HillFile.Lib.Abstractions.Models;

namespace HillFile.Lib.Abstractions.Interfaces
{
    public interface IFileBackend
    {
        IEnumerable<HFListing> ListFiles(string path);
        IEnumerable<HFListing> ListDirectories(string path);
        HFFileInfo LoadFileInfo(string filePath);
        FileStream LoadFileStream(string filePath);
    }
}