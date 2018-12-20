using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;

namespace IOC_PBIAdmin
{
    public class PBIManagerREST
    {
        private static string RESTWrapper(string RESTurl)
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
        public static PBIGroups GetGroups()
        {
            string responseContent = RESTWrapper("groups");
            PBIGroups pbiGroups = JsonConvert.DeserializeObject<PBIGroups>(responseContent);

            return pbiGroups;
        }

        public static PBIDataSets GetDatasets(string groupId)
        {
            string responseContent = RESTWrapper(String.Format("groups/{0}/datasets", groupId));
            PBIDataSets pbidatasets = JsonConvert.DeserializeObject<PBIDataSets>(responseContent);

            return pbidatasets;
        }

        public static PBIGroup GetGroupByName(string groupName)
        {
            string responseContent = RESTWrapper(String.Format("groups?$filter=name eq '{0}'", groupName));
            PBIGroups pbiGroups = JsonConvert.DeserializeObject<PBIGroups>(responseContent);

            return pbiGroups.List[0];
        }

        public static PBIReports GetReports(string groupId)
        {
            string responseContent = RESTWrapper("groups/" + groupId + "/reports");
            PBIReports pbiReports = JsonConvert.DeserializeObject<PBIReports>(responseContent);

            return pbiReports;
        }
        public static PBIReport GetReportByName(string groupName, string reportName)
        {
            PBIGroup group = GetGroupByName(groupName);

            string responseContent = RESTWrapper(String.Format("groups/{0}/Reports?$filter=name eq '{1}'", group.id, reportName));
            PBIReports pbiReports = JsonConvert.DeserializeObject<PBIReports>(responseContent);

            return pbiReports.List[0];
        }

        public static void ListReports()
        {
            PBIGroups groups = GetGroups();

            foreach (PBIGroup group in groups.List)
            {
                Console.WriteLine(group.name);

                PBIReports reports = GetReports(group.id);

                foreach (PBIReport report in reports.List)
                    Console.WriteLine("     " + report.name);
            }

        }

        public static PBIRefreshes GetRefreshes(string groupId, string datasetId)
        {
            string refreshesContent = RESTWrapper(String.Format("groups/{0}/datasets/{1}/refreshes", groupId, datasetId));
            PBIRefreshes refreshes = JsonConvert.DeserializeObject<PBIRefreshes>(refreshesContent);

            return refreshes;
        }

        #region Admin
        public static PBIGroups GetGroupsAdmin()
        {
            string adminGroupsContent = RESTWrapper("admin/groups/");
            PBIGroups adminGroups = JsonConvert.DeserializeObject<PBIGroups>(adminGroupsContent);

            return adminGroups;
        }

        #endregion

        #region Clone Report
        public static string CloneReport(string sourceGroupName, string sourceReportName, string targetGroupName, string targetReportName)
        {
            byte[] file = ExportReport(sourceGroupName, sourceReportName);
            return ImportReport(targetGroupName, targetReportName, file);
        }

        public static byte[] ExportReport(string sourceGroupName, string sourceReportName)
        {
            PBIGroup group = GetGroupByName(sourceGroupName);
            PBIReport report = GetReportByName(sourceGroupName, sourceReportName);

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

        public static string ImportReport(string targetGroupName, string targetReportName, byte[] file)
        {
            PBIGroup group = GetGroupByName(targetGroupName);

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
        #endregion

        #region DumpReportRefreshesToCsv
        public static void DumpReportRefreshesToCsv()
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(String.Format(@".\Refreshes_{0}.csv", DateTime.Now.ToString("yyyyMMddhhmmss")));
            sw.WriteLine("Workspace;DataSet;RefreshType;StartTime;EndTime;Status");

            Dictionary<PBIGroup, PBIDataSets> groupDatasets = new Dictionary<PBIGroup, PBIDataSets>();

            string groupName = null, datasetName = null;
            int datasetCount = 0, groupCount = 0;

            try
            {
                groupName = "";
                PBIGroups groups = GetGroups();

                foreach (PBIGroup group in groups.List)
                {
                    groupCount++;
                    groupName = group.name;
                    datasetCount = 0;
                    datasetName = "";

                    try
                    {
                        PBIDataSets datasets = GetDatasets(group.id);
                        Console.WriteLine(String.Format("Group: {0} ({1} / {2})", group.name, groupCount, groups.List.Count));

                        foreach (PBIDataSet dataset in datasets.List)
                        {
                            try
                            {
                                datasetCount++;
                                datasetName = dataset.name;
                                PBIRefreshes refreshes = GetRefreshes(group.id, dataset.id);

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
