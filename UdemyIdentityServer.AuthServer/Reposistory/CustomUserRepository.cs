using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UdemyIdentityServer.AuthServer.Models;
using UdemyIdentityServer.AuthServer.Repository;

namespace UdemyIdentityServer.AuthServer.Reposistory
{
    public class CustomUserRepository : ICustomUserRepository
    {
        private readonly CustomDbContext _context;

        public CustomUserRepository(CustomDbContext context)
        {
            _context = context;
        }

        public async Task<CustomUser> FindByEmailAsync(string email)
        {
            return await _context.CustomUsers.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<CustomUser> FindByIdAsync(int id)
        {
            return await _context.CustomUsers.FindAsync(id);
        }

        public async Task<bool> ValidateAsync(string email, string password)
        {
            return await _context.CustomUsers.AnyAsync(x=> x.Email == email && x.Password == password);
        }
    }
}
