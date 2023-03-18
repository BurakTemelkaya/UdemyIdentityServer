using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using UdemyIdentityServer.Client1.Models;
using UdemyIdentityServer.Client1.Service;

namespace UdemyIdentityServer.Client1.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly IApiResourceHttpClient _apiResourceHttpClient;

        public LoginController(IConfiguration configuration, IApiResourceHttpClient apiResourceHttpClient)
        {
            _configuration = configuration;
            _apiResourceHttpClient = apiResourceHttpClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync(_configuration["AuthServer:Url"]);

            if (disco.IsError)
            {
                //hata ve loglama kısmı
            }

            var password = new PasswordTokenRequest();

            password.Address = disco.TokenEndpoint;

            password.UserName = loginViewModel.Email;

            password.Password = loginViewModel.Password;

            password.ClientId = _configuration["Client1ResourceOwnerMvc:ClientId"];

            password.ClientSecret = _configuration["Client1ResourceOwnerMvc:ClientSecret"];

            var token = await client.RequestPasswordTokenAsync(password);

            if (token.IsError)
            {
                ModelState.AddModelError("", "Email veya parolanız hatalı.");
                return View(loginViewModel);

                //hata yakalama ve loglama
            }

            var userInfoRequest = new UserInfoRequest();

            userInfoRequest.Token = token.AccessToken;

            userInfoRequest.Address = disco.UserInfoEndpoint;


            var userInfo = await client.GetUserInfoAsync(userInfoRequest);

            if (userInfo.IsError)
            {
                //hata yakalama ve loglama
            }

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(userInfo.Claims, CookieAuthenticationDefaults.AuthenticationScheme, "name", "role");

            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var authenticationProperties = new AuthenticationProperties();

            authenticationProperties.StoreTokens(new List<AuthenticationToken> {

                new AuthenticationToken{Name=OpenIdConnectParameterNames.AccessToken , Value=token.AccessToken},

                new AuthenticationToken{Name=OpenIdConnectParameterNames.RefreshToken , Value=token.RefreshToken},

                new AuthenticationToken{Name=OpenIdConnectParameterNames.ExpiresIn , Value=DateTime.UtcNow.AddSeconds(token.ExpiresIn).ToString("o",CultureInfo.InvariantCulture)}
            });

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);

            return RedirectToAction("Index", "User");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserSaveViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _apiResourceHttpClient.SaveUserAsync(model);

            if (result != null)
            {
                result.ForEach(x =>
                {
                    ModelState.AddModelError("", x);
                });
                return View(model);
                
            }

            return RedirectToAction("Index");
        }
    }
}
