using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOC_PBIAdmin;

namespace IOC_PBIAdmin_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //PBIManager.CloneReport("IOC", "IOC Test", "Machine Learning Dashboard");
            //PBIManager.ListReports();
            //PBIManager.ListReportsREST("IOC Development");

            /*
            PBIGroup pbiGroup = PBIManager.GetGroupByNameREST("IOC Test");
            PBIReports pbiReports = PBIManager.GetReportsREST(pbiGroup.id);

            foreach (PBIReport report in pbiReports.List.Where(x => x.isOwnedByMe = true).ToList())
            {
                Console.WriteLine(report.name);
            }
            */

            //PBIManager.ExportReportREST("IOC Test", "Machine Learning Dashboard");
            //PBIManager.CloneReportREST("IOC Test", "Machine Learning Dashboard", "IOC Admin Test", "Machine Learning Dashboard.pbix");

            //PBIManager.GetEmbedTokenExample();

            //            https://api.powerbi.com/v1.0/myorg/groups/ce5ceba0-aad3-4230-93a1-bf1f91dce682/datasets/7a5732b0-a82f-472f-8662-9d7d1a2d39e2/refreshes
            // PBIManager.DumpRefreshesREST();

            PBIManager.GetGroupsREST();
            Console.ReadLine();
        }
    }
}
