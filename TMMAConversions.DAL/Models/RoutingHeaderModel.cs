using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMMAConversions.DAL.Models
{
    public class RoutingHeaderModel
    {
        public string MaterialCode { get; set; }
        public string MaterialDescription { get; set; }
        public string Plant { get; set; }
        public string RoutingGroup { get; set; }
        public string RoutingCounter { get; set; }
        public DateTime ValidDate { get; set; }
        public string GroupCounter { get; set; }
        public string RoutingDescription { get; set; }
        public string Usage { get; set; }
        public string OverAllStatus { get; set; }
        public int LotSizeFrom { get; set; }
        public int LotSizeTo { get; set; }
        public string BaseUnit { get; set; }
    }
}