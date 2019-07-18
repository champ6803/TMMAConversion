using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMMAConversions.DAL.Models
{
    public class RoutingWithMaterialHeaderModel
    {
        public string MaterialCode { get; set; }
        public string Plant { get; set; }
        public string RoutingGroup { get; set; }
        public string ValidDate { get; set; }
        public string GroupCounter { get; set; }
        public string Usage { get; set; }
        public string OverAllStatus { get; set; }
        public decimal LotSizeTo { get; set; }
        public string BaseUnit { get; set; }
    }
}