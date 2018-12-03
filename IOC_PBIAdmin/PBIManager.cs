using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.PowerBI.Api.V2;
using Microsoft.PowerBI.Api.V2.Models;
using Microsoft.Rest;
using Newtonsoft.Json;

namespace IOC_PBIAdmin
{
    public class PBIManager
    {
        #region Global Variables
        //Credentials
        private static readonly string AuthMode = ConfigurationManager.AppSettings["authMode"];
        private static readonly string ClientId = ConfigurationManager.AppSettings["applicationId"];
        private static readonly string UserName = ConfigurationManager.AppSettings["pbiUsername"];
        private static readonly string Password = ConfigurationManager.AppSettings["pbiPassword"];
        //URLs
        private static readonly string APIbaseURL = ConfigurationManager.AppSettings["APIbaseURL"];
        private static readonly string ApiUrl = ConfigurationManager.AppSettings["apiUrl"];
        public static string Token = ConfigurationManager.AppSettings["token"];
        //The client id that Azure AD created when you registered your client app.
        private static PowerBIClient Client = null;
        //RedirectUri you used when you register your app.
        //For a client app, a redirect uri gives Azure AD more details on the application that it will authenticate.
        // You can use this redirect uri for your client app
        private static string RedirectUrl = ConfigurationManager.AppSettings["redirectUrl"];
        //Resource Uri for Power BI API
        private static string ResourceUrl = ConfigurationManager.AppSettings["resourceUrl"];
        //OAuth2 authority Uri
        private static string AuthorityUrl = ConfigurationManager.AppSettings["authorityUrl"];
        //Sample GUIDs
        private static string SampleReportId = ConfigurationManager.AppSettings["reportId"];
        private static string SampleGroupId = ConfigurationManager.AppSettings["workspaceId"];
        #endregion

        #region Constructors
        static PBIManager()
        {
            if (Token == "")
            {
                switch (AuthMode)
                {
                    case "UserNamePassword": Token = GetToken(UserName, Password).Result; break;
                    case "ClientId": Token = GetToken(ClientId).Result; break;
                }
            }

            TokenCredentials tokenCredentials = new TokenCredentials(Token);
            Client = new PowerBIClient(new Uri(ApiUrl), tokenCredentials);
        }

        #region Constructor Helpers
        private static async Task<string> GetToken(string clientId)
        {
            // TODO: Install-Package Microsoft.IdentityModel.Clients.ActiveDirectory -Version 2.21.301221612
            // and add using Microsoft.IdentityModel.Clients.ActiveDirectory

            //Get access token:
            // To call a Power BI REST operation, create an instance of AuthenticationContext and call AcquireToken
            // AuthenticationContext is part of the Active Directory Authentication Library NuGet package
            // To install the Active Directory Authentication Library NuGet package in Visual Studio,
            // run "Install-Package Microsoft.IdentityModel.Clients.ActiveDirectory" from the nuget Package Manager Console.

            // AcquireToken will acquire an Azure access token
            // Call AcquireToken to get an Azure token from Azure Active Directory token issuance endpoint
            AuthenticationContext authContext = new AuthenticationContext(AuthorityUrl);
            AuthenticationResult authResult = await authContext.AcquireTokenAsync(ResourceUrl, clientId, new Uri(RedirectUrl), new PlatformParameters(PromptBehavior.Always));

            return authResult.AccessToken;
        }

        private static async Task<string> GetToken(string userName, string password)
        {
            UserPasswordCredential userPasswordCredential = new UserPasswordCredential(userName, password);
            AuthenticationContext authContext = new AuthenticationContext(AuthorityUrl);
            AuthenticationResult authResult = await authContext.AcquireTokenAsync(ResourceUrl, ClientId, userPasswordCredential);

            return authResult.AccessToken;
        }
        #endregion

        #endregion

        #region .NET SDK based methods
        /* .NET SDK Reference
         * https://docs.microsoft.com/en-us/dotnet/api/microsoft.powerbi.api.v2?view=azure-dotnet
         */

        /* Embedding resources 
         * https://docs.microsoft.com/en-us/power-bi/developer/embed-sample-for-customers
         * https://github.com/Microsoft/PowerBI-Developer-Samples
         * https://microsoft.github.io/PowerBI-JavaScript/demo/v2-demo/index.html#
         */
        /*
                public static async Task<EmbedConfig> GetEmbedConfig()
                {
                    EmbedToken embedToken = await Client.Reports.GenerateTokenInGroupAsync(SampleGroupId, SampleReportId, new GenerateTokenRequest(accessLevel: "view", datasetId: "[DATASET ID]"));
                    Report report = await Client.Reports.GetReportAsync(SampleGroupId, SampleReportId);

                    var result = new EmbedConfig()
                    {
                        Id = report.Id,
                        EmbedToken = embedToken,
                        EmbedUrl = report.EmbedUrl
                    };

                    return result;
                }
        */
        public static void GetEmbedTokenExample()
        {


            EmbedToken embedToken = Client.Reports.GenerateTokenInGroup(SampleGroupId, SampleReportId, new GenerateTokenRequest(accessLevel: "view", datasetId: "[DATASET ID]"));
            Report report = Client.Reports.GetReport(SampleGroupId, SampleReportId);

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("*** EMBETD TOKEN ***");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Token: " + embedToken.Token);
            Console.WriteLine("Expiration: " + embedToken.Expiration.Value.ToString());

            Console.WriteLine("");

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("*** REPORT ID ***");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(SampleReportId);

            Console.WriteLine("");

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("*** REPORT EMBED URL ***");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(report.EmbedUrl);
        }

        public static void ListReports()
        {
            ODataResponseListGroup groups = Client.Groups.GetGroups();
            foreach (Group group in groups.Value)
            {
                Console.WriteLine(group.Name);
                ODataResponseListReport reports = Client.Reports.GetReports(group.Id);

                foreach (Report report in reports.Value)
                    Console.WriteLine("     " + report.Name);
            }
        }

        private static Group GetGroupByName(string groupName)
        {
            ODataResponseListGroup groups = Client.Groups.GetGroups("name eq '" + groupName + "'");
            if (groups.Value.Count == 1)
                return groups.Value[0];
            else
                return null;
        }

        private static Report GetReportByName(string groupName, string reportName)
        {
            Group group = GetGroupByName(groupName);
            ODataResponseListReport reports = Client.Reports.GetReports(group.Id);
            return reports.Value.First(x => x.Name.Equals(reportName));
        }

        public static void CloneReport(string targetGroupName, string sourceGroupName, string sourceReportName)
        {
            Group targetGroup = GetGroupByName(targetGroupName);
            Report sourceReport = GetReportByName(sourceGroupName, sourceReportName);

            CloneReport(targetGroup, sourceReport);
        }

        private static void CloneReport(Group targetGroup, Report report)
        {
            /*
                        Dataset sourceDataSet = Client.Datasets.GetDatasetById(report.DatasetId);
                        Datasource sourceDataSource = sourceDataSet.Datasources[0];

                        Datasource datasource = new Datasource(
                                                                sourceDataSource.Name,
                                                                sourceDataSource.ConnectionString,
                                                                sourceDataSource.DatasourceType,
                                                                sourceDataSource.ConnectionDetails,
                                                                sourceDataSource.GatewayId,
                                                                sourceDataSource.DatasourceId);
                        Dataset dataset = new Dataset();
                        dataset.Datasources.Add(datasource);
            */


            CloneReportRequest request = new CloneReportRequest(report.Name, targetGroup.Id, report.DatasetId);
            Client.Reports.CloneReportInGroup(targetGroup.Id, report.Id, request);
        }
        #endregion

        #region REST API basd methods
        /*
         * https://docs.microsoft.com/en-us/rest/api/power-bi/
         */
        private static string RESTWrapper(string RESTurl)
        {
            string url = APIbaseURL + RESTurl;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.ContentType = "application/json";
            request.MediaType = "application/json";
            request.Accept = "application/json";

            request.ContentLength = 0;
            request.Method = "GET";
            request.Headers.Add("Authorization", String.Format("Bearer {0}", Token));

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string responseContent = (new StreamReader(response.GetResponseStream())).ReadToEnd();

            return responseContent;
        }

        public static PBIGroups GetGroupsREST()
        {
            string responseContent = RESTWrapper("groups");
            PBIGroups pbiGroups = JsonConvert.DeserializeObject<PBIGroups>(responseContent);

            return pbiGroups;
        }

        public static PBIDataSets GetDatasetsREST(string groupId)
        {
            string responseContent = RESTWrapper(String.Format("groups/{0}/datasets", groupId));
            PBIDataSets pbidatasets = JsonConvert.DeserializeObject<PBIDataSets>(responseContent);

            return pbidatasets;
        }

        public static PBIGroup GetGroupByNameREST(string groupName)
        {
            string responseContent = RESTWrapper(String.Format("groups?$filter=name eq '{0}'", groupName));
            PBIGroups pbiGroups = JsonConvert.DeserializeObject<PBIGroups>(responseContent);

            return pbiGroups.List[0];
        }

        public static PBIReports GetReportsREST(string groupId)
        {
            string responseContent = RESTWrapper("groups/" + groupId + "/reports");
            PBIReports pbiReports = JsonConvert.DeserializeObject<PBIReports>(responseContent);

            return pbiReports;
        }
        public static PBIReport GetReportByNameREST(string groupName, string reportName)
        {
            PBIGroup group = GetGroupByNameREST(groupName);

            string responseContent = RESTWrapper(String.Format("groups/{0}/Reports?$filter=name eq '{1}'", group.id, reportName));
            PBIReports pbiReports = JsonConvert.DeserializeObject<PBIReports>(responseContent);

            return pbiReports.List[0];
        }

        public static byte[] ExportReportREST(string sourceGroupName, string sourceReportName)
        {
            PBIGroup group = GetGroupByNameREST(sourceGroupName);
            PBIReport report = GetReportByNameREST(sourceGroupName, sourceReportName);

            //Export .pbix file from source workspace, to create dataset in the target workspace
            string exportUrl = String.Format(APIbaseURL + "groups/{0}/reports/{1}/Export", group.id, report.id);

            HttpWebRequest exportRequest = (HttpWebRequest)WebRequest.Create(exportUrl);
            exportRequest.ContentLength = 0;
            exportRequest.Method = "GET";
            exportRequest.Headers.Add("Authorization", String.Format("Bearer {0}", Token));

            HttpWebResponse exportResponse = (HttpWebResponse)exportRequest.GetResponse();
            Stream exportResponseStream = exportResponse.GetResponseStream();
            MemoryStream output = new MemoryStream();
            exportResponseStream.CopyTo(output);
            byte[] file = output.ToArray();

            return file;
        }

        public static string ImportReportREST(string targetGroupName, string targetReportName, byte[] file)
        {
            PBIGroup group = GetGroupByNameREST(targetGroupName);

            //Import
            string importUrl = APIbaseURL + String.Format("groups/{0}/imports?datasetDisplayName={1}&nameConflict=Abort", group.id, targetReportName);

            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest importRequest = (HttpWebRequest)WebRequest.Create(importUrl);
            importRequest.ContentType = "multipart/form-data; boundary=" + boundary;
            importRequest.Method = "POST";
            importRequest.KeepAlive = true;
            importRequest.Headers.Add("Authorization", String.Format("Bearer {0}", Token));

            using (Stream rs = importRequest.GetRequestStream())
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);

                string headerTemplate = "Content-Disposition: form-data; filename=\"{0}\"\r\nContent-Type: application / octet - stream\r\n\r\n";
                string header = string.Format(headerTemplate, "testdataset");
                byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
                rs.Write(headerbytes, 0, headerbytes.Length);

                rs.Write(file, 0, file.Length);

                byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                rs.Write(trailer, 0, trailer.Length);
            }

            HttpWebResponse importResponse = null;
            try
            {
                importResponse = (HttpWebResponse)importRequest.GetResponse();
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                        Console.WriteLine(text);
                    }
                }
            }

            Stream importResponseStream = importResponse.GetResponseStream();
            string importResponseContent = (new StreamReader(importResponseStream)).ReadToEnd();

            return importResponseContent;
        }

        public static string CloneReportREST(string sourceGroupName, string sourceReportName, string targetGroupName, string targetReportName)
        {
            byte[] file = ExportReportREST(sourceGroupName, sourceReportName);
            return ImportReportREST(targetGroupName, targetReportName, file);
        }

        public static void ListReportsREST()
        {
            PBIGroups groups = GetGroupsREST();

            foreach (PBIGroup group in groups.List)
            {
                Console.WriteLine(group.name);

                PBIReports reports = GetReportsREST(group.id);

                foreach (PBIReport report in reports.List)
                    Console.WriteLine("     " + report.name);
            }

        }

        public static PBIRefreshes GetRefreshesREST(string groupId, string datasetId)
        {
            string refreshesContent = RESTWrapper(String.Format("groups/{0}/datasets/{1}/refreshes", groupId, datasetId));
            PBIRefreshes refreshes = JsonConvert.DeserializeObject<PBIRefreshes>(refreshesContent);

            return refreshes;
        }

        public static void DumpRefreshesREST()
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(String.Format(@".\Refreshes_{0}.csv", DateTime.Now.ToString("yyyyMMddhhmmss")));
            sw.WriteLine("Workspace;DataSet;RefreshType;StartTime;EndTime;Status");

            Dictionary<PBIGroup, PBIDataSets> groupDatasets = new Dictionary<PBIGroup, PBIDataSets>();

            string groupName = null, datasetName = null;
            int datasetCount = 0, groupCount = 0;

            try
            {
                groupName = "";
                PBIGroups groups = GetGroupsREST();

                foreach (PBIGroup group in groups.List)
                {
                    groupCount++;
                    groupName = group.name;
                    datasetCount = 0;
                    datasetName = "";

                    try
                    {
                        PBIDataSets datasets = GetDatasetsREST(group.id);
                        Console.WriteLine(String.Format("Group: {0} ({1} / {2})", group.name, groupCount, groups.List.Count));

                        foreach (PBIDataSet dataset in datasets.List)
                        {
                            try
                            {
                                datasetCount++;
                                datasetName = dataset.name;
                                PBIRefreshes refreshes = GetRefreshesREST(group.id, dataset.id);

                                Console.WriteLine(String.Format("     Dataset: {0} ({1} / {2}) - {3} refreshes", dataset.name, datasetCount, datasets.List.Count, refreshes.List.Count));

                                foreach (PBIRefresh refresh in refreshes.List)
                                {

                                    datasetName = dataset.name;

                                    sw.WriteLine(String.Join(";", new String[]{
                                                group.name,
                                                dataset.name,
                                                refresh.refreshType,
                                                refresh.startTime.ToString("yyyy.MM.dd hh:mm:ss"),
                                                refresh.endTime.ToString("yyyy.MM.dd hh:mm:ss"),
                                                refresh.status
                                            }));
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("     Query error (group: " + groupName + ", dataset: " + datasetName + ")");
                                sw.WriteLine(groupName + ";" + datasetName + ";;;;");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Query error (group: " + groupName + ")");
                        sw.WriteLine(groupName + ";;;;;");
                    }
                }
            }
            catch
            {
                Console.WriteLine("Query error");
            }

            sw.Close();
        }

    #endregion
    }
}
