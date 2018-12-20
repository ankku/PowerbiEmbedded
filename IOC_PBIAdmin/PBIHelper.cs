using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace IOC_PBIAdmin
{
    public static class PBIConfig
    {
        static PBIConfig()
        {
            AuthMode = ConfigurationManager.AppSettings["authMode"];
            //Credentials
            ClientId = ConfigurationManager.AppSettings["applicationId"];
            UserName = ConfigurationManager.AppSettings["pbiUsername"];
            Password = ConfigurationManager.AppSettings["pbiPassword"];

            //URLs
            APIbaseURL = ConfigurationManager.AppSettings["APIbaseURL"];
            ApiUrl = ConfigurationManager.AppSettings["apiUrl"];
            Token = ConfigurationManager.AppSettings["token"];

            //RedirectUri you used when you register your app.
            //For a client app, a redirect uri gives Azure AD more details on the application that it will authenticate.
            // You can use this redirect uri for your client app
            RedirectUrl = ConfigurationManager.AppSettings["redirectUrl"];
            
            //Resource Uri for Power BI API
            ResourceUrl = ConfigurationManager.AppSettings["resourceUrl"];

            //OAuth2 authority Uri
            AuthorityUrl = ConfigurationManager.AppSettings["authorityUrl"];

            //Sample GUIDs
            SampleReportId = ConfigurationManager.AppSettings["reportId"];
            SampleGroupId = ConfigurationManager.AppSettings["workspaceId"];

        }

        public static string AuthMode { get;  }
        public static string ClientId { get;  }
        public static string UserName { get; }
        public static string Password { get; }

        //URLs
        public static string APIbaseURL { get; }
        public static string ApiUrl { get; }
        public static string Token { get; }

        //The client id that Azure AD created when you registered your client app.
        //private static PowerBIClient Client = null;

        //RedirectUri you used when you register your app.
        //For a client app, a redirect uri gives Azure AD more details on the application that it will authenticate.
        // You can use this redirect uri for your client app
        public static string RedirectUrl { get;  }

        //Resource Uri for Power BI API
        public static string ResourceUrl { get; }

        //OAuth2 authority Uri
        public static string AuthorityUrl { get; }

        //Sample GUIDs
        public static string SampleReportId { get; }
        public static string SampleGroupId { get; }
    }

    public class User
    {
        public string ClientId {get; }
        public string DisplayableId { get; }
        public string FamilyName { get; }
        public string GivenName { get; }
        User(string clientId, string displayableId, string familyName, string givenName)
        {
            ClientId = clientId;
            DisplayableId = displayableId;
            FamilyName = familyName;
            GivenName = givenName;
        }
    }
    public class PBIDashboards
    {
        public PBIDashboard[] value { get; set; }

        public List<PBIDashboard> List
        {
            get { return value.ToList(); }
        }
    }

    public class PBIDashboard
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public string embedUrl { get; set; }
        public bool isReadOnly { get; set; }
    }

    public class PBIReports
    {
        public PBIReport[] value { get; set; }

        public List<PBIReport> List
        {
            get { return value.ToList(); }
        }
    }

    public class PBIReport
    {
        public string id { get; set; }
        public string name { get; set; }
        public string webUrl { get; set; }
        public string embedUrl { get; set; }
        public bool isOwnedByMe { get; set; }
        public string dataSetId { get; set; }
    }

    public class PBIGroups
    {
        public PBIGroup[] value { get; set; }
        public List<PBIGroup> List
        {
            get { return value.ToList(); }
        }
    }

    public class PBIGroup
    {
        public string id { get; set; }
        public bool isReadOnly { get; set; }
        public bool isOnDedicatedCapacity { get; set; }
        public string capacityId { get; set; }
        public string name { get; set; }
    }

    public class PBIDataSets
    {
        public PBIDataSet[] value { get; set; }
        public List<PBIDataSet> List
        {
            get { return value.ToList(); }
        }
    }

    public class PBIDataSet
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool addRowsAPIEnabled { get; set; }
        public string configuredBy { get; set; }
        public bool isRefreshable { get; set; }
        public bool isEffectiveIdentityRequired { get; set; }
        public bool isEffectiveIdentityRolesRequired { get; set; }
        public bool isOnPremGatewayRequired { get; set; }
    }

    public class PBIRefreshes
    {
        public PBIRefresh[] value { get; set; }
        public List<PBIRefresh> List
        {
            get { return value.ToList(); }
        }
    }
    public class PBIRefresh
    {
        public string id { get; set; }
        public string refreshType { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public string serviceExceptionJson { get; set; }
        public string status { get; set; }
    }
}
