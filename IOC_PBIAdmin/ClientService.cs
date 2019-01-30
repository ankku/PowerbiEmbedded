using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Rest;
using Microsoft.PowerBI.Api.V2;

namespace IOC_PBIAdmin
{
    internal static class ClientService
    {
        private static PowerBIClient client = null;
        public static PowerBIClient Client
        {
            get
            {
                if (client == null)
                {
                    TokenCredentials tokenCredentials = new TokenCredentials(TokenService.FetchToken());
                    client = new PowerBIClient(new Uri(PBIConfig.ApiUrl), tokenCredentials);

                }
                return client;
            }
        }
    }
}
