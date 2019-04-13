using System.Collections.Generic;
using System.Threading.Tasks;
using HillFile.Lib.Abstractions.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace HillFile.Web.Hubs
{
    public class FileListingHub: Hub
    {
        private readonly IFileBackend fileBackend;

        public FileListingHub(IFileBackend fileBackend)
        {
            this.fileBackend = fileBackend;
        }

        public async Task ListDirectory(string directory)
        {
            var tasks = new List<Task>();
            
            var directories = fileBackend.ListDirectories(directory);
            var files = fileBackend.ListFiles(directory);
            
            foreach (var hfListing in directories)
            {
                tasks.Add(Clients.Caller.SendAsync("FileListing", hfListing));
            }
            
            foreach (var hfListing in files)
            {
                tasks.Add(Clients.Caller.SendAsync("FileListing", hfListing));
            }
            
            await Task.WhenAll(tasks);
        }

        public async Task LoadFileInfo(string path)
        {
            await Clients.Caller.SendAsync("FileInfo", fileBackend.LoadFileInfo(path));
        }
    }
}