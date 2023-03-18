using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static IdentityServer4.IdentityServerConstants;

namespace UdemyIdentityServerIdentityAPI.AuthServer.Controller
{
    [Route("api/[controller]/[action]")]
    [Authorize(LocalApi.PolicyName)]
    [ApiController]
    
    public class UserController : ControllerBase
    {
        [HttpPost]
        public IActionResult SignUp()
        {
            return Ok("signup çalıştı");
        }
    }
}
