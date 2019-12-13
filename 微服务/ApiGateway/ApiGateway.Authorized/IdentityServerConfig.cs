using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Authorized
{
    public class IdentityServerConfig
    {
        /// <summary>
        ///  定义API资源
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("default-api", "Default (all) API")
                {
                    Description = "AllFunctionalityYouHaveInTheApplication",
                    ApiSecrets= {new Secret("secret") }
                }
            };
        }

        #region 定义身份资源

        /// <summary>
        ///  定义身份资源
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Phone(),
                new IdentityResources.Address()
            };
        }

        //public static IEnumerable<IdentityResource> GetIdentityResources()
        //{
        //    var customProfile = new IdentityResource(
        //        name: "custom.profile", 
        //        displayName: "Custom profile", 
        //        claimTypes: new[] { "name", "email", "status" }
        //        );

        //    return new List<IdentityResource>
        //    {
        //        new IdentityResources.OpenId(),
        //        new IdentityResources.Profile(),
        //        customProfile
        //    };
        //}

        #endregion

        /// <summary>
        ///  定义测试客服端
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            var clients = new List<Client>();
            foreach (var child in configuration.GetSection("IdentityServer:Clients").GetChildren())
            {
                clients.Add(
                    new Client {
                        ClientId = child["ClientId"],
                        ClientName = child["ClientName"],
                        AllowedGrantTypes = child.GetSection("AllowedGrantTypes").GetChildren().Select(c => c.Value).ToArray(),
                        RequireConsent = bool.Parse(child["RequireConsent"] ?? "false"),
                        AllowOfflineAccess = bool.Parse(child["AllowOfflineAccess"] ?? "false"),
                        ClientSecrets = child.GetSection("ClientSecrets").GetChildren().Select(secret => new Secret(secret["Value"].Sha256())).ToArray(),
                        AllowedScopes = child.GetSection("AllowedScopes").GetChildren().Select(c => c.Value).ToArray(),
                        RedirectUris = child.GetSection("RedirectUris").GetChildren().Select(c => c.Value).ToArray(),
                        PostLogoutRedirectUris = child.GetSection("PostLogoutRedirectUris").GetChildren().Select(c => c.Value).ToArray(), }
                    );
            }
            return clients;
        }

    }
}
