using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.PowerBI.Api.V2;
using Microsoft.PowerBI.Api.V2.Models;
using Microsoft.AspNetCore.Authentication;

namespace PowerBIWebApp.Pages
{
    public class AdminModel : PageModel
    {
        public List<Group> adminGroups { get; set; }
      

        public string Message { get; set; }
        private readonly IConfiguration _configuration;
        private PowerBIClient client { get; set; }
        public AdminModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
            Message = "Your Admin page.";

            string token = GetToken();
            string apiUrl = _configuration["AppSettings:ApiUrl"];

            PowerBIClient pbiClient = new PowerBIClient(new Uri(apiUrl), new Microsoft.Rest.TokenCredentials(token));

            adminGroups = pbiClient.Groups.GetGroupsAsAdmin().Value.ToList();
        }

        internal List<Group> GetGroups()
        {
            Task<ODataResponseListGroup> callTask = Task.Run(() => GetGroupsAsync());
            callTask.Wait();
            var groups = callTask.Result;

            return groups.Value.ToList();
        }

        internal async Task<ODataResponseListGroup> GetGroupsAsync()
        {
            return await client.Groups.GetGroupsAsync();
        }

        internal string GetToken()
        {
            Task<string> callTask = Task.Run(() => GetTokenasync());
            callTask.Wait();
            var token = callTask.Result;

            return token;
        }
        
        internal async Task<string> GetTokenasync()
        {
            string accessToken = await HttpContext.GetTokenAsync("access_token");
            return accessToken;
        }
/*
        internal string GetToken()
        {
            Task<string> callTask = Task.Run(() => GetTokenasync());
            callTask.Wait();
            var token = callTask.Result;

            return token;
        }

        internal async Task<string> GetTokenasync()
        {
            string clientId = _configuration["Authentication:ClientId"];
            string clientsecret = _configuration["Authentication:ClientSecret"];
//            string authority = _configuration["Authentication:Authority"];
//            string authority = _configuration["AppSettings:Authority"];
            string authority = "https://login.windows.net/common/oauth2/authorize/";
            string resource = _configuration["Authentication:Resource"];


            ClientCredential cred = new ClientCredential(clientId, clientsecret);
            AuthenticationContext authContext = new AuthenticationContext(authority);
            AuthenticationResult result = await authContext.AcquireTokenAsync(resource, cred);

            return result.AccessToken;
        }
*/
        /*
        internal string GetToken()
        {
            Task<string> callTask = Task.Run(() => GetTokenasync());
            callTask.Wait();
            var token = callTask.Result;

            return token;
        }

        internal async Task<string> GetTokenasync()
        {
            string accessToken = await HttpContext.GetTokenAsync("access_token");
            return accessToken;
        }
        */
    }
}
