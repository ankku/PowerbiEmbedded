using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.PowerBI.Api.V2;

namespace IOC_PBIAdmin
{
    internal static class PBIConfig
    {
        private static string authMode = null;
        public static string AuthMode
        {
            get { return (authMode != null) ? authMode : ConfigurationManager.AppSettings["authMode"]; }
            set { authMode = value; }
        }

        private static string clientId = null;
        public static string ClientId
        {
            get { return (clientId != null) ? clientId : ConfigurationManager.AppSettings["applicationId"]; }
            set { clientId= value; }
        }

        private static string clientSecret = null;
        public static string ClientSecret
        {
            get { return (clientSecret != null) ? clientSecret: ConfigurationManager.AppSettings["secret"]; }
            set { clientSecret = value; }
        }

        private static string userName = null;
        public static string UserName
        {
            get { return (userName != null) ? userName : ConfigurationManager.AppSettings["userName"]; }
            set { userName = value; }
        }

        //URLs
        public static string apiBaseUrl = "https://api.powerbi.com/v1.0/myorg/";
        public static string APIbaseURL
        {
            get { return (apiBaseUrl != null) ? apiBaseUrl : ConfigurationManager.AppSettings["APIbaseURL"]; }
            set { apiBaseUrl = value; }
        }

        private static string apiUrl = null;
        public static string ApiUrl
        {
            get { return (apiUrl != null) ? apiUrl : ConfigurationManager.AppSettings["apiUrl"]; }
            set { apiUrl = value; }
        }

        //RedirectUri you used when you register your app.
        //For a client app, a redirect uri gives Azure AD more details on the application that it will authenticate.
        // You can use this redirect uri for your client app
        private static string redirectUrl = "urn:ietf:wg:oauth:2.0:oob";
        public static string RedirectUrl
        {
            get { return (redirectUrl != null) ? redirectUrl : ConfigurationManager.AppSettings["redirectUrl"]; }
            set { redirectUrl = value; }
        }

        //Resource Uri for Power BI API
        private static string resourceUrl = "https://analysis.windows.net/powerbi/api";
        public static string ResourceUrl
        {
            get { return (resourceUrl != null) ? resourceUrl : ConfigurationManager.AppSettings["resourceUrl"]; }
            set { resourceUrl = value; }
        }

        //OAuth2 authority Uri
        private static string authorityUrl = "https://login.microsoftonline.com/common/oauth2/authorize";
        public static string AuthorityUrl
        {
            get { return (authorityUrl != null) ? authorityUrl : ConfigurationManager.AppSettings["authorityUrl"]; }
            set { authorityUrl = value; }
        }

        //Sample GUIDs
        private static string sampleReportId = null;
        public static string SampleReportId
        {
            get { return (sampleReportId != null) ? sampleReportId : ConfigurationManager.AppSettings["reportId"]; }
            set { sampleReportId = value; }
        }

        private static string sampleGroupId = null;
        public static string SampleGroupId
        {
            get { return (sampleGroupId != null) ? sampleGroupId : ConfigurationManager.AppSettings["workspaceId"]; }
            set { sampleGroupId = value; }
        }
    }

    public class User
    {
        public string ClientId { get; }
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
