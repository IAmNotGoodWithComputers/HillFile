using Microsoft.AspNetCore.Mvc;

namespace HillFile.Web.Controllers
{
    [Route("/api/file")]
    public class FileController: Controller
    {
        [Route("filestream")]
        public IActionResult FileStream([FromQuery] string file)
        {
            return Ok();
        }
    }
}