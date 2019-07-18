using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMMAConversions.DAL.Models
{
    public class BOMHeaderModel
    {
        public string MaterialCode { get; set; }
        public string Plant { get; set; }
        public decimal BaseQuantity { get; set; }
        public string BaseUnit { get; set; }
        public string BOMUsage { get; set; }
        public string BOMAlt { get; set; }
        public DateTime ValidDate { get; set; }
        public string BOMHeaderText { get; set; }
        public string BOMStatus { get; set; }
        public string ProductionLine { get; set; }
        public string ProdSupervisor { get; set; }
        public string PhantomIndicator { get; set; }
        public string Condition { get; set; }
        public string RoutingGroup { get; set; }
        public string GroupCounter { get; set; }
        public string ProductionVersion { get; set; }
        public string WorkCenter { get; set; }
        public string StandardTextKey { get; set; }
        public string LotSizeFrom { get; set; }
        public string LotSizeTo { get; set; }
        public string StorageLocation { get; set; }
    }
}