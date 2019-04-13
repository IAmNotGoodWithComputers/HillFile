using System;
using Microsoft.AspNetCore.Mvc;

namespace HillFile.Web.Controllers
{
    [Route("api/user")]
    public class UserController: Controller
    {
        [Route("homedir")]
        public IActionResult HomeDir()
        {
            return Ok(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
        }
    }
}