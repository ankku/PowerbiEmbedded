using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Identity.Client;
using Microsoft.PowerBI.Api.V2;
using Microsoft.PowerBI.Api.V2.Models;
using Microsoft.Rest;
using Newtonsoft.Json;

namespace IOC_PBIAdmin
{
    internal static class TokenService
    {
        public static AuthenticationContext AuthContext { get; }
        public static bool HasAuthenticatedUser { get { return AuthContext.TokenCache.Count > 0; } }
        public static string AuthenticatedUser { get { return AuthContext.TokenCache.ReadItems().First().GivenName + " " + AuthContext.TokenCache.ReadItems().First().FamilyName; } }
        public static string AuthenticatedUserEmail { get { return AuthContext.TokenCache.ReadItems().First().DisplayableId; } }




        static TokenService()
        {
            AuthContext = new AuthenticationContext(PBIConfig.AuthorityUrl, new FileCache());
        }

        #region Microsoft.IdentityModel.Clients.ActiveDirectory.AcquireToken
        public static string FetchToken()
        {
            string result = null;
            try
            {
                Task<string> callTask = Task.Run(() => FetchTokenAsync());
                callTask.Wait();
                result = callTask.Result;
            }
            catch (Exception e)
            {
                //Log error
            }
            return result;
        }

        public static async Task<string> FetchTokenAsync()
        {
            /*In case of using different profiles (AAD users) from one client
             *      1. After Authentication during acquiring token, User Id shoud be recorded and saved
             *      2. UserIdentifier should be created using previously saved UserId, as a hint for context, when acquiring token again
             *          e.g.: UserIdentifier userId = new UserIdentifier(PBIConfig.Profile, UserIdentifierType.UniqueId);
             *      3. If you want to add new profile (user), than "Prompt Behavior" platform parameter should be set to "Always"
             *          a. in this case, the newly returned client id should be recorded again, and used as context information...
             */

            Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationResult result = null;

            // first, try to get a token silently
            try
            {
                switch (PBIConfig.AuthMode)
                {
                    case "Secret":
                        result = await AuthContext.AcquireTokenAsync(PBIConfig.ResourceUrl, new Microsoft.IdentityModel.Clients.ActiveDirectory.ClientCredential(PBIConfig.ClientId, PBIConfig.ClientSecret));
                        break;
                    case "ClientId":
                        result = await AuthContext.AcquireTokenSilentAsync(PBIConfig.ResourceUrl, PBIConfig.ClientId);
                        break;
                    case "UserName":
                        result = await AuthContext.AcquireTokenAsync(PBIConfig.ResourceUrl, PBIConfig.ClientId, new UserCredential(PBIConfig.UserName));
                        break;
                }   
            }
            catch (AdalException adalException)
            {
                // There is no token in the cache; prompt the user to sign-in.
                if (adalException.ErrorCode == AdalError.FailedToAcquireTokenSilently
                    || adalException.ErrorCode == AdalError.InteractionRequired)
                {
                    try
                    {
                        result = await AuthContext.AcquireTokenAsync(PBIConfig.ResourceUrl, PBIConfig.ClientId, new Uri(PBIConfig.RedirectUrl), new PlatformParameters(PromptBehavior.Auto));
                    }
                    catch (Exception e)
                    {
                        //Log Exception
                    }
                }
                else
                {
                    //Log exception
                }

                // An unexpected error occurred.
                //                ShowError(adalException);
            }
            catch (Exception e)
            {
                //Log Exception
            }

            return result.AccessToken;
        }
        #endregion

        #region Microsoft.Identity.Client.ConfidentialClientApplication.AcquireToken
        public static string FetchTokenConfidential()
        {
            string result = null;
            try
            {
                Task<string> callTask = Task.Run(() => FetchTokenConfidentalAsync(new string[] { "https://analysis.windows.net/powerbi/api/Tenant.Read.All" }));
                callTask.Wait();
                result = callTask.Result;
            }
            catch (Exception e)
            {
                //Log error
            }
            return result;
        }

        public static async Task<string> FetchTokenConfidentalAsync(string[] scopes)
        {
            try
            {
                Microsoft.Identity.Client.ConfidentialClientApplication cc = new Microsoft.Identity.Client.ConfidentialClientApplication(PBIConfig.ClientId, PBIConfig.RedirectUrl, new Microsoft.Identity.Client.ClientCredential(PBIConfig.ClientSecret), null, null);
                //            var accounts = await cc.GetAccountsAsync();
                //            Microsoft.Identity.Client.AuthenticationResult result = await cc.AcquireTokenSilentAsync(scopes, accounts.First());
                Microsoft.Identity.Client.AuthenticationResult result = await cc.AcquireTokenForClientAsync(scopes);
                return result.AccessToken;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
        #endregion

        public static void ClearCache()
        {
            AuthContext.TokenCache.Clear();
        }
    }
}
