using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMMAConversions.DAL.Models
{
    public class WorkCenterRoutingModel
    {
        // require field
        public string MaterialCode { get; set; }
        public string WorkCenter { get; set; }
        public string WorkCenterCategory { get; set; }
        public string WorkCenterGroup { get; set; }
        public string Plant { get; set; }
        public string WorkCenterName { get; set; }
        public string PersonResponsible { get; set; }
        public string TaskListUsage { get; set; }
        public string StandardValueKeyHeader { get; set; }
        public string ControlKey { get; set; }
        public string UnitOfMeasurement1 { get; set; }
        public string UnitOfMeasurement2 { get; set; }
        public string UnitOfMeasurement3 { get; set; }
        public string UnitOfMeasurement4 { get; set; }
        public string UnitOfMeasurement5 { get; set; }
        public string UnitOfMeasurement6 { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CostCenter { get; set; }
        public string CostCenterDescription { get; set; }
        public string RoutingGroup { get; set; }
        public string RoutingGroupDescription { get; set; }
        public string GroupCounter { get; set; }
        public string OperationNo { get; set; }
        public string OperationDecription { get; set; }
        public decimal OperationBaseQuantity { get; set; }
        public string OperationOUM { get; set; }
        public string ConversionOfOUMN { get; set; }
        public string ConversionOfOUMD { get; set; }
        public DateTime ValidDate { get; set; }
        public string HeaderFlag { get; set; }
        public string ItemFlag { get; set; }
        public string GroupFlag { get; set; }
        public string FactoryCalendarID { get; set; }

        // 
        public string MaterialDescription { get; set; }
        public string RoutingCounter { get; set; }
        public string RoutingDescription { get; set; }
        public string Usage { get; set; }
        public string OverAllStatus { get; set; }
        public int LotSizeFrom { get; set; }
        public int LotSizeTo { get; set; }
        public string BaseUnit { get; set; }
        public string WorkCenterDescription { get; set; }

        // 
        public string StandardTextKey { get; set; }
        public string StandardValue1 { get; set; }
        public string StandardValue1OUM { get; set; }
        public string StandardValue2 { get; set; }
        public string StandardValue2OUM { get; set; }
        public string StandardValue3 { get; set; }
        public string StandardValue3OUM { get; set; }
        public string StandardValue4 { get; set; }
        public string StandardValue4OUM { get; set; }
        public string StandardValue5 { get; set; }
        public string StandardValue5OUM { get; set; }
        public string StandardValue6 { get; set; }
        public string StandardValue6OUM { get; set; }
        public string Scarp { get; set; }

        // optional field
        public string Indicator { get; set; }
        public string CapacityCategory { get; set; }
        public string CapacityResponsible { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string CapacityUtilization { get; set; }
        public string CumulativeLength { get; set; }
        public string NoIndCapacities { get; set; }
        public string CapacityBaseUnit { get; set; }
        public string CapacityUnit { get; set; }
        public string Overload { get; set; }
        public string LongtermPlanning { get; set; }
        public string ActivityDescNo1 { get; set; }
        public string ActivityDescNo2 { get; set; }
        public string ActivityDescNo3 { get; set; }
        public string ActivityDescNo4 { get; set; }
        public string ActivityDescNo5 { get; set; }
        public string ActivityDescNo6 { get; set; }
        public string ActivityTypeNo1 { get; set; }
        public string ActivityTypeNo2 { get; set; }
        public string ActivityTypeNo3 { get; set; }
        public string ActivityTypeNo4 { get; set; }
        public string ActivityTypeNo5 { get; set; }
        public string ActivityTypeNo6 { get; set; }
        public string UnitOfActNo1 { get; set; }
        public string UnitOfActNo2 { get; set; }
        public string UnitOfActNo3 { get; set; }
        public string UnitOfActNo4 { get; set; }
        public string UnitOfActNo5 { get; set; }
        public string UnitOfActNo6 { get; set; }
        public string FomularKeyNo1 { get; set; }
        public string FomularKeyNo2 { get; set; }
        public string FomularKeyNo3 { get; set; }
        public string FomularKeyNo4 { get; set; }
        public string FomularKeyNo5 { get; set; }
        public string FomularKeyNo6 { get; set; }
        public string RefIndicatorNo1 { get; set; }
        public string RefIndicatorNo2 { get; set; }
        public string RefIndicatorNo3 { get; set; }
        public string RefIndicatorNo4 { get; set; }
        public string RefIndicatorNo5 { get; set; }
        public string RefIndicatorNo6 { get; set; }
        public string FormulaForSetupCapacityRequirements { get; set; }
        public string CapacityRelevantToFiniteScheduling { get; set; }
        public string SeveralOperationsCanUseCapacity { get; set; }
        public string FirstWorkCenterParameter { get; set; }
        public string ParameterValue { get; set; }
        public string Backflushing { get; set; }
    }
}