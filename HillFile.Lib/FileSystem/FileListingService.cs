using System.Collections.Generic;
using System.IO;
using System.Linq;
using HillFile.Lib.Abstractions.Interfaces;
using HillFile.Lib.Abstractions.Models;

namespace HillFile.Lib.FileSystem
{
    public class FileListingService: IFileBackend
    {
        public IEnumerable<HFListing> ListFiles(string path)
        {
            return Directory.EnumerateFiles(path)
                .Select(f => new HFListing
                {
                    Name = Path.GetFileName(f),
                    Path = path,
                    FullPath = f,
                    Type = ListingType.File
                });
        }

        public IEnumerable<HFListing> ListDirectories(string path)
        {
            return Directory.EnumerateDirectories(path)
                .Select(f => new HFListing
                {
                    Name = Path.GetFileName(f),
                    Path = path,
                    FullPath = f,
                    Type = ListingType.Directory
                });
        }

        public HFFileInfo LoadFileInfo(HFListing listing)
        {
            return LoadFileInfo(listing.FullPath);
        }

        public HFFileInfo LoadFileInfo(string filePath)
        {
            var hfFileInfo = new HFFileInfo();
            var systemFileInfo = new FileInfo(filePath);

            hfFileInfo.ByteSize = systemFileInfo.Length;
            hfFileInfo.FullPath = filePath;
            hfFileInfo.Name = Path.GetFileName(filePath);
            hfFileInfo.Path = Path.GetDirectoryName(filePath);
            hfFileInfo.CreateDate = File.GetCreationTime(filePath);
            hfFileInfo.ModifiedDate = File.GetLastWriteTime(filePath);
            hfFileInfo.AccessDate = File.GetLastAccessTime(filePath);

            return hfFileInfo;
        }
    }
}