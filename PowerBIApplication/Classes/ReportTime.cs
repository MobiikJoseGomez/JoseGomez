using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PowerBIApplication.Classes
{
    public class ReportTime
    {
        public string EmbedUrl { get; set; }
        public string Name { get; set; }
    }

    public class ReportsTimeFromJson
    {
        public ReportTime[] Value { get; set; }
    }
}