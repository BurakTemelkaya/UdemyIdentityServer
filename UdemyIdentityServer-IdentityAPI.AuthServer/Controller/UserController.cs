using IdentityServer4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using UdemyIdentityServer_IdentityAPI.AuthServer.Models;
using UdemyIdentityServerIdentityAPI.AuthServer.Models;
using static IdentityServer4.IdentityServerConstants;

namespace UdemyIdentityServerIdentityAPI.AuthServer.Controller
{
    [Route("api/[controller]/[action]")]
    [Authorize(LocalApi.PolicyName)]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserSaveViewModel userSaveViewModel)
        {
            var user = new ApplicationUser
            {
                UserName = userSaveViewModel.UserName,

                Email = userSaveViewModel.Email
            };

            var result = await _userManager.CreateAsync(user, userSaveViewModel.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(x => x.Description));
            }

            return Ok("üye başarıyla kayıt edildi.");
        }
    }
}
