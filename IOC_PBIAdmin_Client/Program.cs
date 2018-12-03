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

            Console.WriteLine("*******************");
            Console.WriteLine("*** Query Start ***");
            Console.WriteLine("*******************");
            Console.WriteLine("");

            PBIManager.DumpRefreshesREST();

            Console.WriteLine("");
            Console.WriteLine("*******************");
            Console.WriteLine("***  Query End  ***");
            Console.WriteLine("*******************");

            Console.WriteLine("(Press ENTER)");

            Console.ReadLine();
        }
    }
}
