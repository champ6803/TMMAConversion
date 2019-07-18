using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMMAConversions.DAL.Models
{
    public class BOMItemModel
    {
        public string BOMItem { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialGroup { get; set; }
        public string RoutingGroup { get; set; }
        public string Plant { get; set; }
        public string BOMUsage { get; set; }
        public string BOMAlt { get; set; }
        public DateTime ValidDate { get; set; }
        public string ComponentMaterial { get; set; }
        public decimal ComponentQuantity { get; set; }
        public string ComponentUnit { get; set; }
        public string ComponentDescription { get; set; }
        public string ComponentScrap { get; set; }
        public string FixedQty { get; set; }
        public string OperationScrap { get; set; }
        public string NetScrap { get; set; }
        public string Indicator { get; set; }
        public string CostingRelevency { get; set; }
        public string OperationNo { get; set; }
        public string WorkCenter { get; set; }
        public int ActivityNo { get; set; }
        public string Activity { get; set; }
        public string StandardValueKey { get; set; }
        public string StandardValueKeyText { get; set; }
        public string StandardValueKeyOUM { get; set; }
        public string ConversionOfUOMNumerator { get; set; }
        public string ConversionOfUOMDenominator { get; set; }
    }
}