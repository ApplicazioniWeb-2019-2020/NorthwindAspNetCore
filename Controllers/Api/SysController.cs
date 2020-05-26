using System;
using Microsoft.AspNetCore.Mvc;

namespace NorthwindAspNetCore.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SysController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> GetInfo() => Environment.OSVersion.Platform.ToString();
    }
}
