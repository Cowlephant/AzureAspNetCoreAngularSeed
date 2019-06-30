using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Logging;

namespace AzureAspNetCoreAngularSeed
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;

                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                    if (env.IsDevelopment())
                        config.AddUserSecrets<Startup>();

                    var builtConfig = config.Build();

                    var azureServiceTokenProvider = new AzureServiceTokenProvider();
                    var keyVaultClient = new KeyVaultClient(
                        new KeyVaultClient.AuthenticationCallback(
                            azureServiceTokenProvider.KeyVaultTokenCallback));

                    //  Accomodate accessing keyvault in all scenarios... in production, dev on domain, and dev off domain
                    // If on domain, it'll pass credentials so we don't need client secret or client id
                    // If production in azure, it'll have access through access policy
                    if (!env.IsDevelopment() || builtConfig["ClientSecret"] == null || builtConfig["ClientId"] == null)
                        config.AddAzureKeyVault(
                              $"https://{builtConfig["KeyVaultName"]}.vault.azure.net/",
                              keyVaultClient,
                              new DefaultKeyVaultSecretManager());
                    else // We're developing while not on the domain and need access with ClientId and ClientSecret
                        config.AddAzureKeyVault(
                              $"https://{builtConfig["KeyVaultName"]}.vault.azure.net/",
                              builtConfig["ClientId"],
                              builtConfig["ClientSecret"]);
                })
                .UseStartup<Startup>();
    }
}
