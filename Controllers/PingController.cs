using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace TodoMvc.Controllers
{
    public class PingController : Controller
    {
        [HttpGet("api")]
        public void Ping()
        {
            if (Startup.Configuration["Data:UseSqlBackend"] != "true")
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
        }

        [HttpGet("learn.json")]
        public JsonResult Learn()
        {
            return Json(new {});
        }
    }
}
