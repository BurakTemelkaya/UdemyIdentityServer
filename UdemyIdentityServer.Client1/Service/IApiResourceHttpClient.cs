using System.Net.Http;
using System.Threading.Tasks;

namespace UdemyIdentityServer.Client1.Service
{
    public interface IApiResourceHttpClient
    {
        Task<HttpClient> GetHttpClientAsync();
    }
}
