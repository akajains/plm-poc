using IdentityServer4.Core;
using IdentityServer4.Core.Models;
using IdentityServer4.Core.Services.InMemory;
using System.Collections.Generic;
using System.Security.Claims;

namespace PLM.Authorization
{
    public class InMemoryManager
    {
        public List<InMemoryUser> GetUsers()
        {
            return new List<InMemoryUser> {
                new InMemoryUser
                {
                    Subject="akajain@gmail.com",
                    Username="ajain@esa.edu.au",
                    Password="esa123",
                    Claims = new []
                    {
                        new Claim(Constants.ClaimTypes.ExternalProviderUserId, "Aakash Jain")
                    }
                }

            };

        }

        public IEnumerable<Scope> GetScopes()
        {
            return new[] {
                StandardScopes.OpenId,
                StandardScopes.Profile,
                StandardScopes.OfflineAccess,
                new Scope
                {
                    Name="read",
                    DisplayName="Read User Data"
                }
            };


        }

        public IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId="socialnetwork",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret ("secret".Sha256())
                    },
                    ClientName="CmsService",
                    Flow=Flows.ResourceOwner,
                    AllowedScopes =new List<string>
                    {                       
                        "read"
                    },
                    Enabled=true
                },
                new Client
                {
                    ClientId = "socialnetwork_implicit",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    ClientName = "SocialNetwork",
                    Flow = Flows.Implicit,                   
                   
                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        "read"
                    },
                    RedirectUris = new List<string>
                    {
                        "http://localhost:8000/index.html/private"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:8000/index.html/post-private"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        "*"
                    },
                    Enabled=true

                }
            };

        }

    }
}