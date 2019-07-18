using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMMAConversions.DAL.Models
{
    public class InpectionPlanModel
    {
        public string MaterialCode { get; set; }
        public string Plant { get; set; }
        public string TaskListGroup { get; set; }
        public DateTime ValidDate { get; set; }
        public string GroupCounter { get; set; }
        public string MIC { get; set; }
        public string MICVersion { get; set; }
        public string ShortText { get; set; }
        public string InspectionMethod { get; set; }
        public string InspectionMethodVersion { get; set; }
        
    }
}