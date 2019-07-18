using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMMAConversions.DAL.Models
{
    public class ProductionVersionModel
    {
        public string Plant { get; set; }
        public string MaterialCode { get; set; }
        public string ProductionVersion { get; set; }
        public string ProductionVersionDescription { get; set; }
        public string ValidDateFrom { get; set; }
        public string ValidDateTo { get; set; }
        public string TaskListType { get; set; }
        public string Group { get; set; }
        public string GroupCounter { get; set; }
        public string BOMAlt { get; set; }
        public string BOMUsage { get; set; }
        public string ProductionLine { get; set; }
        public string IssueStorageLocation { get; set; }
        public string ReceivingStorageLocation { get; set; }
    }
}