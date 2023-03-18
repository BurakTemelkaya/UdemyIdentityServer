using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UdemyIdentityServer.Client1.Models;

namespace UdemyIdentityServer.Client1.Service
{
    public interface IApiResourceHttpClient
    {
        Task<HttpClient> GetHttpClientAsync();

        Task<List<string>> SaveUserAsync(UserSaveViewModel model);
    }
}
