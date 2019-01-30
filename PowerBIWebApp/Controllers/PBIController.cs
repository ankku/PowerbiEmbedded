using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.IO;

using Microsoft.AspNetCore.Authentication;

namespace PowerBIWebApp.Controllers
{
    public class PBIController : Controller
    {
        public IActionResult GetData()
        {
            return View();
        }

        internal void RestWrapper(string query)
        {             
            Task<string> callTask = Task.Run(() => GetToken());
            callTask.Wait();
            var token = callTask.Result;

            string baseUri = "https://analysis.windows.net/powerbi/api/"; // _configuration["AppSettings:BaseUri"];
            string url = baseUri + query;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.ContentType = "application/json";
            request.MediaType = "application/json";
            request.Accept = "application/json";

            request.ContentLength = 0;
            request.Method = "GET";
            request.Headers.Add("Authorization", String.Format("Bearer {0}", token));

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string responseContent = (new StreamReader(response.GetResponseStream())).ReadToEnd();
        }

        internal async Task<string> GetToken()
        {
            string accessToken = await HttpContext.GetTokenAsync("access_token");
            return accessToken;
        }
    }
}