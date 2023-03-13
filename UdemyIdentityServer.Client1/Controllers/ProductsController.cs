using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UdemyIdentityServer.Client1.Models;
using UdemyIdentityServer.Client1.Service;

namespace UdemyIdentityServer.Client1.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {

        private readonly IApiResourceHttpClient _apiResourceHttpClient;

        public ProductsController(IApiResourceHttpClient apiResourceHttpClient)
        {
            _apiResourceHttpClient = apiResourceHttpClient;
        }

        public async Task<IActionResult> Index()
        {
            List<Product> products = new List<Product>();

            //var discovery = await httpClient.GetDiscoveryDocumentAsync("https://localhost:5001");

            //if (discovery.IsError)
            //{
            //    //loglama yap
            //}

            //ClientCredentialsTokenRequest clientCredentialsTokenRequest = new ClientCredentialsTokenRequest();

            //clientCredentialsTokenRequest.ClientId = _configuration["Client:Client"];
            //clientCredentialsTokenRequest.ClientSecret = _configuration["Client:ClientSecret"];
            //clientCredentialsTokenRequest.Address = discovery.TokenEndpoint;

            //var token = await httpClient.RequestClientCredentialsTokenAsync(clientCredentialsTokenRequest);

            //if (token.IsError)
            //{
            //    //discovery.Error;
            //    //loglama yap
            //}

            var client = await _apiResourceHttpClient.GetHttpClientAsync();

            var response = await client.GetAsync("https://localhost:5016/api/products/getproducts");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                products = JsonConvert.DeserializeObject<List<Product>>(content);
            }
            else
            {
                //loglama yap
            }

            return View(products);
        }
    }
}
