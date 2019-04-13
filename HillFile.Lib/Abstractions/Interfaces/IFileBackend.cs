using System.Collections.Generic;
using HillFile.Lib.Abstractions.Models;

namespace HillFile.Lib.Abstractions.Interfaces
{
    public interface IFileBackend
    {
        IEnumerable<HFListing> ListFiles(string path);
        IEnumerable<HFListing> ListDirectories(string path);
        HFFileInfo LoadFileInfo(HFListing listing);
        HFFileInfo LoadFileInfo(string filePath);
    }
}