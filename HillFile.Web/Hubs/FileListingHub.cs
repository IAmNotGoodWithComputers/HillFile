using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HillFile.Lib.Abstractions.Interfaces;
using HillFile.Web.Service;
using Microsoft.AspNetCore.SignalR;

namespace HillFile.Web.Hubs
{
    public class FileListingHub: Hub
    {
        private readonly IFileBackend fileBackend;
        private readonly ICurrentUserDirectoryService currentUserDirectoryService;

        public FileListingHub(IFileBackend fileBackend, 
            ICurrentUserDirectoryService currentUserDirectoryService)
        {
            this.fileBackend = fileBackend;
            this.currentUserDirectoryService = currentUserDirectoryService;
        }

        public async Task ListDirectory(string directory)
        {
            var tasks = new List<Task>();
            directory = NormalizePath(directory);
            var rootPath = currentUserDirectoryService.GetCurrentUserDirectory();
            
            var directories = fileBackend.ListDirectories(directory);
            var files = fileBackend.ListFiles(directory);
            
            tasks.AddRange(directories.Select(hfListing => Clients.Caller.SendAsync("FileListing", new
            {
                Name = hfListing.Name,
                Path = hfListing.Path.Replace(rootPath, "").Trim('/'),
                FullPath = hfListing.FullPath.Replace(rootPath, "").Trim('/'),
                Type = hfListing.Type
            })));
            
            tasks.AddRange(files.Select(hfListing => Clients.Caller.SendAsync("FileListing", new
            {
                Name = hfListing.Name,
                Path = hfListing.Path.Replace(rootPath, ""),
                FullPath = hfListing.FullPath.Replace(rootPath, ""),
                Type = hfListing.Type
            })));

            await Task.WhenAll(tasks);
        }

        public async Task LoadFileInfo(string path)
        {
            path = NormalizePath(path);
            await Clients.Caller.SendAsync("FileInfo", fileBackend.LoadFileInfo(path));
        }

        private string NormalizePath(string path)
        {
            var rootDirectory = currentUserDirectoryService.GetCurrentUserDirectory();
            return '/' + rootDirectory.Trim('/') + '/' + path.Trim('/') + '/';
        }
        
    }
}