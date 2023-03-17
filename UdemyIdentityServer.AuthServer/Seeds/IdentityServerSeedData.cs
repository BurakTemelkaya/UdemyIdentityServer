using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using System.Linq;

namespace UdemyIdentityServer.AuthServer.Seeds
{
    public static class IdentityServerSeedData
    {
        public static void Seed(ConfigurationDbContext context)
        {

            if (!context.Clients.Any())
            {
                foreach (var client in Config.GetClients())
                {
                    context.Clients.Add(client.ToEntity());
                }
            }

            if (!context.ApiResources.Any())
            {
                foreach (var apiResource in Config.GetApiResources())
                {
                    context.ApiResources.Add(apiResource.ToEntity());
                }
            }

            if (!context.ApiScopes.Any())
            {
                Config.GetApiScopes().ToList().ForEach(
                    apiScope =>
                    {
                        context.ApiScopes.Add(apiScope.ToEntity());
                    });
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var identityResource in Config.GetIdentityResources())
                {
                    context.IdentityResources.Add(identityResource.ToEntity());
                }
            }

            context.SaveChanges();
        }
    }
}
