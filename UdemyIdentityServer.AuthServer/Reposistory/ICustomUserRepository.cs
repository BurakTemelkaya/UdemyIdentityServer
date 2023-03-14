using System.Threading.Tasks;
using UdemyIdentityServer.AuthServer.Models;

namespace UdemyIdentityServer.AuthServer.Repository
{
    public interface ICustomUserRepository
    {
        Task<bool> ValidateAsync(string email, string password);

        Task<CustomUser> FindByIdAsync(int id);

        Task<CustomUser> FindByEmailAsync(string email);
    }
}
