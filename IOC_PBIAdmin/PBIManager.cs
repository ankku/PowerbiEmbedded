using System;
using System.Linq;
using System.Net;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.PowerBI.Api.V2;
using Microsoft.PowerBI.Api.V2.Models;
using System.Threading.Tasks;

namespace IOC_PBIAdmin
{
    public class PBIManager
    {
        #region Constructor
        public PBIManager()
        {
        }

        public PBIManager(string clientId, string userName)
        {
            PBIConfig.AuthMode = "ClientId";
            PBIConfig.ClientId = clientId;
            PBIConfig.UserName = userName;
        }
        #endregion

        #region .Net General Methods
        /* .NET SDK Reference
         * https://docs.microsoft.com/en-us/dotnet/api/microsoft.powerbi.api.v2?view=azure-dotnet
         */

        /* Embedding resources 
         * https://docs.microsoft.com/en-us/power-bi/developer/embed-sample-for-customers
         * https://github.com/Microsoft/PowerBI-Developer-Samples
         * https://microsoft.github.io/PowerBI-JavaScript/demo/v2-demo/index.html#
         */

        public void NETListReports()
        {
            ODataResponseListGroup groups = ClientService.Client.Groups.GetGroups();
            foreach (Group group in groups.Value)
            {
                Console.WriteLine(group.Name);
                ODataResponseListReport reports = ClientService.Client.Reports.GetReports(group.Id);

                foreach (Report report in reports.Value)
                    Console.WriteLine("     " + report.Name);
            }
        }

        private Group NETGetGroupByName(string groupName)
        {
            ODataResponseListGroup groups = ClientService.Client.Groups.GetGroups("name eq '" + groupName + "'");
            if (groups.Value.Count == 1)
                return groups.Value[0];
            else
                return null;
        }
        #endregion

        #region .Net Complex Methods
        private Report NETGetReportByName(string groupName, string reportName)
        {
            Group group = NETGetGroupByName(groupName);
            ODataResponseListReport reports = ClientService.Client.Reports.GetReports(group.Id);
            return reports.Value.First(x => x.Name.Equals(reportName));
        }

        private void NETCloneReport(Group targetGroup, Report report)
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
            ClientService.Client.Reports.CloneReportInGroup(targetGroup.Id, report.Id, request);
        }

        public void NETCloneReport(string targetGroupName, string sourceGroupName, string sourceReportName)
        {
            Group targetGroup = NETGetGroupByName(targetGroupName);
            Report sourceReport = NETGetReportByName(sourceGroupName, sourceReportName);

            NETCloneReport(targetGroup, sourceReport);
        }

        #endregion

        #region .NET Admin Methods
        private void NETGetGroupsAsAdmin()
        {
            ClientService.Client.Groups.GetGroupsAsAdmin();
        }
        #endregion

        #region .NET Embedding

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
        public void NETGetEmbedTokenExample()
        {
            EmbedToken embedToken = ClientService.Client.Reports.GenerateTokenInGroup(PBIConfig.SampleGroupId, PBIConfig.SampleReportId, new GenerateTokenRequest(accessLevel: "view", datasetId: "[DATASET ID]"));
            Report report = ClientService.Client.Reports.GetReport(PBIConfig.SampleGroupId, PBIConfig.SampleReportId);

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
            Console.WriteLine(PBIConfig.SampleReportId);

            Console.WriteLine("");

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("*** REPORT EMBED URL ***");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(report.EmbedUrl);
        }
        #endregion

        #region REST General Methods
        public PBIGroups RESTGetGroups()
        {
            string responseContent = RESTWrapper("groups");
            PBIGroups pbiGroups = JsonConvert.DeserializeObject<PBIGroups>(responseContent);

            return pbiGroups;
        }

        public PBIDataSets RESTGetDatasets(string groupId)
        {
            string responseContent = RESTWrapper(String.Format("groups/{0}/datasets", groupId));
            PBIDataSets pbidatasets = JsonConvert.DeserializeObject<PBIDataSets>(responseContent);

            return pbidatasets;
        }

        public PBIGroup RESTGetGroupByName(string groupName)
        {
            string responseContent = RESTWrapper(String.Format("groups?$filter=name eq '{0}'", groupName));
            PBIGroups pbiGroups = JsonConvert.DeserializeObject<PBIGroups>(responseContent);

            return pbiGroups.List[0];
        }

        public PBIReports RESTGetReports(string groupId)
        {
            string responseContent = RESTWrapper("groups/" + groupId + "/reports");
            PBIReports pbiReports = JsonConvert.DeserializeObject<PBIReports>(responseContent);

            return pbiReports;
        }
        public PBIReport RESTGetReportByName(string groupName, string reportName)
        {
            PBIGroup group = RESTGetGroupByName(groupName);

            string responseContent = RESTWrapper(String.Format("groups/{0}/Reports?$filter=name eq '{1}'", group.id, reportName));
            PBIReports pbiReports = JsonConvert.DeserializeObject<PBIReports>(responseContent);

            return pbiReports.List[0];
        }

        public void RESTListReports()
        {
            PBIGroups groups = RESTGetGroups();

            foreach (PBIGroup group in groups.List)
            {
                Console.WriteLine(group.name);

                PBIReports reports = RESTGetReports(group.id);

                foreach (PBIReport report in reports.List)
                    Console.WriteLine("     " + report.name);
            }

        }

        public PBIRefreshes RESTGetRefreshes(string groupId, string datasetId)
        {
            string refreshesContent = RESTWrapper(String.Format("groups/{0}/datasets/{1}/refreshes", groupId, datasetId));
            PBIRefreshes refreshes = JsonConvert.DeserializeObject<PBIRefreshes>(refreshesContent);

            return refreshes;
        }

        #endregion

        #region REST Admin Methods
        public PBIGroups RESTGetGroupsAdmin()
        {
            string adminGroupsContent = RESTWrapper("admin/groups");
            PBIGroups adminGroups = JsonConvert.DeserializeObject<PBIGroups>(adminGroupsContent);

            return adminGroups;
        }

        #endregion

        #region REST Complex Methods
        public byte[] RESTExportReport(string sourceGroupName, string sourceReportName)
        {
            PBIGroup group = RESTGetGroupByName(sourceGroupName);
            PBIReport report = RESTGetReportByName(sourceGroupName, sourceReportName);

            //Export .pbix file from source workspace, to create dataset in the target workspace
            string exportUrl = String.Format(PBIConfig.APIbaseURL + "groups/{0}/reports/{1}/Export", group.id, report.id);

            HttpWebRequest exportRequest = (HttpWebRequest)WebRequest.Create(exportUrl);
            exportRequest.ContentLength = 0;
            exportRequest.Method = "GET";
            exportRequest.Headers.Add("Authorization", String.Format("Bearer {0}", TokenService.FetchToken()));

            HttpWebResponse exportResponse = (HttpWebResponse)exportRequest.GetResponse();
            Stream exportResponseStream = exportResponse.GetResponseStream();
            MemoryStream output = new MemoryStream();
            exportResponseStream.CopyTo(output);
            byte[] file = output.ToArray();

            return file;
        }

        public string RESTImportReport(string targetGroupName, string targetReportName, byte[] file)
        {
            PBIGroup group = RESTGetGroupByName(targetGroupName);

            //Import
            string importUrl = PBIConfig.APIbaseURL + String.Format("groups/{0}/imports?datasetDisplayName={1}&nameConflict=Abort", group.id, targetReportName);

            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest importRequest = (HttpWebRequest)WebRequest.Create(importUrl);
            importRequest.ContentType = "multipart/form-data; boundary=" + boundary;
            importRequest.Method = "POST";
            importRequest.KeepAlive = true;
            importRequest.Headers.Add("Authorization", String.Format("Bearer {0}", TokenService.FetchToken()));

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

        public string RESTCloneReport(string sourceGroupName, string sourceReportName, string targetGroupName, string targetReportName)
        {
            byte[] file = RESTExportReport(sourceGroupName, sourceReportName);
            return RESTImportReport(targetGroupName, targetReportName, file);
        }

        public void RESTDumpReportRefreshesToCsv()
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(String.Format(@".\Refreshes_{0}.csv", DateTime.Now.ToString("yyyyMMddhhmmss")));
            sw.WriteLine("Workspace;DataSet;RefreshType;StartTime;EndTime;Status");

            Dictionary<PBIGroup, PBIDataSets> groupDatasets = new Dictionary<PBIGroup, PBIDataSets>();

            string groupName = null, datasetName = null;
            int datasetCount = 0, groupCount = 0;

            try
            {
                groupName = "";
                PBIGroups groups = RESTGetGroups();

                foreach (PBIGroup group in groups.List)
                {
                    groupCount++;
                    groupName = group.name;
                    datasetCount = 0;
                    datasetName = "";

                    try
                    {
                        PBIDataSets datasets = RESTGetDatasets(group.id);
                        Console.WriteLine(String.Format("Group: {0} ({1} / {2})", group.name, groupCount, groups.List.Count));

                        foreach (PBIDataSet dataset in datasets.List)
                        {
                            try
                            {
                                datasetCount++;
                                datasetName = dataset.name;
                                PBIRefreshes refreshes = RESTGetRefreshes(group.id, dataset.id);

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

        #region Helper Methods
        private string RESTWrapper(string RESTurl)
        {
            string url = PBIConfig.APIbaseURL + RESTurl;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.ContentType = "application/json";
            request.MediaType = "application/json";
            request.Accept = "application/json";

            request.ContentLength = 0;
            request.Method = "GET";
            request.Headers.Add("Authorization", String.Format("Bearer {0}", TokenService.FetchToken()));

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string responseContent = (new StreamReader(response.GetResponseStream())).ReadToEnd();

            return responseContent;
        }

        public static string test()
        {
            string result = null;
            try
            {
                Task callTask = Task.Run(() => testAsync());
                callTask.Wait();
            }
            catch (Exception e)
            {
                //Log error
            }
            return result;
        }
        public static async Task testAsync()
        {
            Microsoft.IdentityModel.Clients.ActiveDirectory.ClientCredential cred = new Microsoft.IdentityModel.Clients.ActiveDirectory.ClientCredential(PBIConfig.ClientId, PBIConfig.ClientSecret);
            Microsoft.IdentityModel.Clients.ActiveDirectory.UserIdentifier user = new Microsoft.IdentityModel.Clients.ActiveDirectory.UserIdentifier("43e514c7-cc35-4226-93a3-eb83a2852ad5", Microsoft.IdentityModel.Clients.ActiveDirectory.UserIdentifierType.OptionalDisplayableId);

            var res = await TokenService.AuthContext.AcquireTokenSilentAsync(PBIConfig.ResourceUrl, cred, user);

            string token = "AQABAAAAAACEfexXxjamQb3OeGQ4Gugv71VOKGNVXNJGkcjBwMuqpryMOp69ZfOaiQ4be_oP3tt2JIQ4U0p5lJpCgdbV5V9eeNseQzaQMxqlPogHyD-FJuH2BCfWEOorBav1Z6Zh6qV7vGYxQmte6oNh-lqV8N1H3cFFqVBbYwQsPsA3RevZ3HJ8phgxf_mRBlx6OsWR-8_LeP8QgDcVUXEbdw93ZCA6v5wX0v8J5Z8yp5YIgvdrPA2LQlSfmhM5u5Vedauue-m0YvtxgZpXwAfyX2NHLP3QRP_5_D224Tv_Au7kOETRbMRBKmrCCQzBbjRC-g3FCWuWyAIYbTJFoqmEcndPGiEWAjny6VYD2lsZTRBbSXAeA0Kqa1loQ8A4QnTAECJBMY0IWppVGzDCvgAO_kT_qwYRqP9fR9YVCeTsF9d4rFMq4Nil-8bc7GDfKcJpa0Z52DxYrIsIYiM9WSuX1Xf0vVpgpY7QFn-YDbuLrlf--bp9wJJ_BHmguHhzWxw6b5rYyqL0IaVlk-1SrmvPy4IoUgslyrF6azQaKLb4EIvkimXC_kmiRVOcFii7wyefO0pikNq3JllpmDQ-FvoqTehNZVvlF4d3dYmISYhzS1xk8ghklb3Br0RrhLj88Yyf47SNFli3YD79nFIxs8v3fybdyxqhp54gCqJN0kf2jkmitX8gR1KH5nwG1pdFDRt9Z3nidIYgAA";
            string url = "https://api.powerbi.com/v1.0/myorg/admin/groups";

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

        public static void OpenId()
        {
            //https://docs.microsoft.com/en-us/azure/active-directory/develop/v1-protocols-openid-connect-code
            string authorityUrl = "https://login.microsoftonline.com/3aa4a235-b6e2-48d5-9195-7fcf05b459b0/oauth2/authorize";
            string url = String.Format(authorityUrl + "?client_id={0}&redirect_uri={1}&scope={2}&response_type=code&response_mode=form_post" //"&response_mode=query&login=login&state=1234yxz"
                                            , "e309e45d-5bd9-4fc3-a3a5-9ad55efa0393"
                                            , "urn%3Aietf%3Awg%3Aoauth%3A2.0%3Aoob"
//                                            , "https%3A%2F%2Fanalysis.windows.net%2Fpowerbi%2Fapi%2FTenant.Read.All"
                                            , "openid"
                                    );

            HttpWebRequest request = (HttpWebRequest)WebRequest.CreateHttp(url);
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

//            TokenService.AuthContext.AcquireTokenByAuthorizationCodeAsync()

            /* 
                        AuthContext.AcquireTokenByAuthorizationCodeAsync(
                            context.ProtocolMessage.Code, new Uri(currentUri), credential, context.Options.Resource);
            */
        }


        #endregion
    }
}
