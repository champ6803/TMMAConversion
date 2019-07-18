using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMMAConversions.DAL.Models
{
    public class PackagingInstructionModel
    {
        public string PackagingInstruction { get; set; }
        public string Description { get; set; }
        public string ReferenceMaterial { get; set; }
        public string ComponentPackage { get; set; }
        public string ComponentReferenceMaterial { get; set; }
        public string TargetQuantity { get; set; }
        public string MinimumQuantity { get; set; }
    }
}