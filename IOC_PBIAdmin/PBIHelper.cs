using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC_PBIAdmin
{
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
        public string name { get; set; }
    }
}
