using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace UdemyIdentityServer.Client1.Controllers
{
    [Authorize]
    public class UserController : Controller
    {

        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index");
            //await HttpContext.SignOutAsync("oidc"); identity serverdan çıkma
        }

        public async Task<IActionResult> GetRefreshToken()
        {
            var refreshToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

            HttpClient httpClient = new HttpClient();

            var discovery = await httpClient.GetDiscoveryDocumentAsync("https://localhost:5001");

            if (discovery.IsError)
            {
                //loglama yap
            }

            RefreshTokenRequest refreshTokenRequest = new()
            {
                ClientId = _configuration["Client1ResourceOwnerMvc:ClientId"],

                ClientSecret = _configuration["Client1ResourceOwnerMvc:ClientSecret"],

                RefreshToken = refreshToken,

                Address = discovery.TokenEndpoint
            };

            var token = await httpClient.RequestRefreshTokenAsync(refreshTokenRequest);

            if (token.IsError)
            {
                //yönlendirme yap
            }

            var tokens = new List<AuthenticationToken>()
            {
                new AuthenticationToken{Name=OpenIdConnectParameterNames.IdToken , Value=token.IdentityToken},

                new AuthenticationToken{Name=OpenIdConnectParameterNames.AccessToken , Value=token.AccessToken},

                new AuthenticationToken{Name=OpenIdConnectParameterNames.RefreshToken , Value=token.RefreshToken},

                new AuthenticationToken{Name=OpenIdConnectParameterNames.ExpiresIn , Value=DateTime.UtcNow.AddSeconds(token.ExpiresIn).ToString("o",CultureInfo.InvariantCulture)}
            };

            var authenticationResult = await HttpContext.AuthenticateAsync();

            var properties = authenticationResult.Properties;

            properties.StoreTokens(tokens);

            await HttpContext.SignInAsync("Cookies", authenticationResult.Principal, properties);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public IActionResult AdminAction()
        {
            return View();
        }

        [Authorize(Roles = "admin,customer")]
        public IActionResult CustomerAction()
        {
            return View();
        }
    }
}
