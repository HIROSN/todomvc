using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TodoMvc.Models;
using TodoMvc.Services;

namespace TodoMvc.Controllers
{
    public class LoginController : Controller
    {
        private ISignInService _signInService;

        public LoginController(ISignInService signInService)
        {
            _signInService = signInService;
        }

        [HttpPost("api/login")]
        public async Task Login([FromBody]TodoUserModel loginUser)
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;

            if (ModelState.IsValid)
            {
                if (await _signInService.SignIn(loginUser))
                {
                    Response.StatusCode = (int)HttpStatusCode.OK;
                }
            }
        }

        [Authorize]
        [HttpGet("api/login")]
        public JsonResult IsAuthenticated()
        {
            return Json(new {userName = User.Identity.Name});
        }

        [Authorize]
        [HttpPost("api/logout")]
        public async Task Logout()
        {
            await _signInService.SignOut();
        }
    }
}
