using System.Linq;
using HillFile.Lib.Abstractions.Interfaces;
using HillFile.Lib.Etc;
using Microsoft.AspNetCore.Mvc;

namespace HillFile.Web.Controllers
{
    [Route("/api/file")]
    public class FileController: Controller
    {
        private readonly IFileBackend fileBackend;

        public FileController(IFileBackend fileBackend)
        {
            this.fileBackend = fileBackend;
        }

        [Route("filestream")]
        public IActionResult FileStream([FromQuery] string file)
        {
            var fs = fileBackend.LoadFileStream(file);
            var fileInfo = fileBackend.LoadFileInfo(file);

            var extension = "."+fileInfo.Name.Split('.').Last();
            var contentType = "application/octet-stream";
            if (FileTypes.ContentTypes.ContainsKey(extension))
            {
                contentType = FileTypes.ContentTypes[extension];
            }
            
            return File(fs, contentType, fileInfo.Name);
        }
    }
}