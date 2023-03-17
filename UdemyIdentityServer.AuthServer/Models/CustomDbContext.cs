using Microsoft.EntityFrameworkCore;

namespace UdemyIdentityServer.AuthServer.Models
{
    public class CustomDbContext:DbContext
    {
        public CustomDbContext(DbContextOptions<CustomDbContext> opts):base(opts)
        {
            
        }

        public DbSet<CustomUser> CustomUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomUser>().HasData(new CustomUser()
            {Id=1 ,Email="temelkayaburak@gmail.com", Password ="password",City="İstanbul",UserName="burak"}, new CustomUser()
            { Id = 2, Email = "fatihcakiroglu@gmail.com", Password = "password", City = "Ankara", UserName = "fcakiroglu" },
            new CustomUser()
            { Id = 3, Email = "mehmet@gmail.com", Password = "password", City = "Yozgat", UserName = "Konya" });

            base.OnModelCreating(modelBuilder);
        }
    }
}
