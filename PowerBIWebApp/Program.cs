using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Logging;

namespace PowerBIWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    IHostingEnvironment env = builderContext.HostingEnvironment;
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

//                    config.AddAzureKeyVault("https://statoil-mlenart.vault.azure.net/", new KeyVaultSecretManager());

                        //this need to be added back for Equinor deployment !!                    
//                    config.AddAzureKeyVault("https://statoil-mlenart.vault.azure.net/");
                });

    }
    /*
        public class KeyVaultSecretManager : IKeyVaultSecretManager
        {
            public string GetKey(SecretBundle secret)
            {
                return secret.SecretIdentifier.Name;
            }

            public bool Load(SecretItem secret)
            {
                //Loads a secret, when it is called "statoil-powerbiapp-secret"
                return secret.Identifier.Name.Equals("statoil-powerbiapp-secret");
            }
        }
    */
}
