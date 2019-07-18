using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMMAConversions.DAL.Models
{
    public class InspectionPlanHeaderModel
    {
        public string MaterialCode { get; set; }
        public string MaterialDescription { get; set; }
        public string Plant { get; set; }
        public string Group { get; set; }
        public DateTime ValidDate { get; set; }
        public string GroupCounter { get; set; }
        public string HeaderDescription { get; set; }
        public string TaskListUsage { get; set; }
        public string Status { get; set; }
        public int LotSizeFrom { get; set; }
        public int LotSizeTo { get; set; }
        public string BaseUnit { get; set; }
        public string InspectionPoint { get; set; }
    }
}