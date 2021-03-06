﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMMAConversions.DAL.Models
{
    public class RoutingItemModel
    {
        public string MaterialCode { get; set; }
        public string Plant { get; set; }
        public string OperationNo { get; set; }
        public string WorkCenter { get; set; }
        public string OperationDecription { get; set; }
        public string StandardTextKey { get; set; }
        public string StandardValueKey { get; set; }
        public decimal OperationBaseQuantity { get; set; }
        public string OperationOUM { get; set; }
        public string ConversionOfOUMN { get; set; }
        public string ConversionOfOUMD { get; set; }
        public string ActivityType1 { get; set; }
        public decimal StandardValue1 { get; set; }
        public string StandardValue1OUM { get; set; }
        public string ActivityType2 { get; set; }
        public decimal StandardValue2 { get; set; }
        public string StandardValue2OUM { get; set; }
        public string ActivityType3 { get; set; }
        public decimal StandardValue3 { get; set; }
        public string StandardValue3OUM { get; set; }
        public string ActivityType4 { get; set; }
        public decimal StandardValue4 { get; set; }
        public string StandardValue4OUM { get; set; }
        public string ActivityType5 { get; set; }
        public decimal StandardValue5 { get; set; }
        public string StandardValue5OUM { get; set; }
        public string ActivityType6 { get; set; }
        public decimal StandardValue6 { get; set; }
        public string StandardValue6OUM { get; set; }
        public string Scarp { get; set; }
    }
}