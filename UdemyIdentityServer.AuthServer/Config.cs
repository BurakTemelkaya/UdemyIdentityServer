using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace UdemyIdentityServer.AuthServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource("resource_api1"){
                    Scopes={ "api1.read", "api1.write", "api1.update" },
                    ApiSecrets=new[]{new Secret("secretapi1".Sha256())}
                },
                new ApiResource("resource_api2")
                {
                    Scopes={ "api2.read", "api2.write", "api2.update" },
                    ApiSecrets=new[]{new Secret("secretapi2".Sha256())}
                }
            };
        }
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>()
            {
                new ApiScope("api1.read","API 1 için okuma izni"),
                new ApiScope("api1.write","API 1 için yazma izni"),
                new ApiScope("api1.update","API 1 için güncelleme izni"),
                new ApiScope("api2.read","API 2 için okuma izni"),
                new ApiScope("api2.write","API 2 için yazma izni"),
                new ApiScope("api2.update","API 2 için güncelleme izni")
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client
                {
                    ClientId="Client1",
                    ClientName="Client 1 app Uygulaması",
                    ClientSecrets=new[]{new Secret("secret".Sha256())},
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    AllowedScopes= {"api1.read" }
                },
                new Client
                {
                    ClientId="Client2",
                    ClientName="Client 2 app Uygulaması",
                    ClientSecrets=new[]{new Secret("secret".Sha256())},
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    AllowedScopes= {"api1.read","api1.update","api2.write","api2.update" }
                },
                new Client
                {
                    ClientId="Client1-Mvc",
                    RequirePkce= false,
                    ClientName="Client 1 app mvc uygulaması",
                    ClientSecrets=new[]{new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RedirectUris=new List<string>{ "https://localhost:5006/signin-oidc" },
                    PostLogoutRedirectUris=new List<string>{"https://localhost:5006/signout-callback-oidc"},
                    AllowedScopes= {IdentityServerConstants.StandardScopes.OpenId,IdentityServerConstants.StandardScopes.Profile, "api1.read" ,IdentityServerConstants.StandardScopes.OfflineAccess},
                    AccessTokenLifetime=2*60*60,
                    AllowOfflineAccess=true,
                    RefreshTokenUsage=TokenUsage.ReUse,
                    RefreshTokenExpiration=TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime=(int)(DateTime.Now.AddMonths(2)-DateTime.Now).TotalSeconds,                  
                }
            };
        }
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }
        public static IEnumerable<TestUser> GetTestUsers()
        {
            return new List<TestUser>()
            {
                new TestUser
                {
                    SubjectId="1",Username="burak",Password="password",
                    Claims=new List<Claim>
                    {
                        new Claim("given_name","Burak"),
                        new Claim("family_name","Temelkaya")
                    }
                },
                new TestUser
                {
                    SubjectId="2",Username="fcakiroglu",Password="password",
                    Claims=new List<Claim>
                    {
                        new Claim("given_name","Fatih"),
                        new Claim("family_name","Çakıroğlu")
                    }
                }
            };
        }
    }
}
