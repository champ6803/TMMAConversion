using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMMAConversions.DAL.Models
{
    public class AssignMaterialToRoutingModel
    {
        public string MaterialCode { get; set; }
        public string Plant { get; set; }
        public string RoutingGroup { get; set; }
        public string Validate { get; set; }
        public string GroupCounter { get; set; }
    }
}