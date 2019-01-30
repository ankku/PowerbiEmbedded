using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.PowerBI.Api.V2;
using Microsoft.PowerBI.Api.V2.Models;
using Microsoft.AspNetCore.Authentication;

namespace PowerBIWebApp.Pages
{
    public class AdministrationModel : PageModel
    {
        public List<Group> adminGroups { get; set; }
        public Dataset adminGroupsDs { get; set; }

        private readonly IConfiguration _configuration;
        public AdministrationModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
            string token = GetToken();
            string apiUrl = _configuration["AppSettings:ApiUrl"];

            PowerBIClient pbiClient = new PowerBIClient(new Uri(apiUrl), new Microsoft.Rest.TokenCredentials(token));

            adminGroups = pbiClient.Groups.GetGroupsAsAdmin().Value.ToList();
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
    }
}