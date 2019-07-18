using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMMAConversions.DAL.Models
{
    public class InspectionPlanItemModel
    {
        public string MaterialCode { get; set; }
        public string MIC { get; set; }
        public string MICVersion { get; set; }
        public string ShortText { get; set; }
        public string InpectionMethod { get; set; }
        public string InpectionMethodVersion { get; set; }
        public string ItemDescription { get; set; }
        public string SetUpValue1 { get; set; }
        public string SetUpValue2 { get; set; }
        public string SetUpValue3 { get; set; }
        public string SetUpValue4 { get; set; }
        public string SetUpValue5 { get; set; }
        public string SetUpValue6 { get; set; }
    }
}