using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMMAConversions.DAL.Models
{
    public class WorkCenterRoutingItemModel
    {
        public string ItemFlag { get; set; }
        public int ActivityNo { get; set; }
        public string ActivityDescription { get; set; }
        public string ActivityUnit { get; set; }
        public string CostDiverUnit { get; set; }
        public string Remark { get; set; }
        public string ActivityType { get; set; }
        public string CostElement { get; set; }
        public string CostElementDescription { get; set; }
        public string CostingFormular { get; set; }
        public string RefIndicator { get; set; }
    }
}