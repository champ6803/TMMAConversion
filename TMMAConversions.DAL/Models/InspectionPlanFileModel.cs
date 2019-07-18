using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMMAConversions.DAL.Models
{
    public class InspectionPlanFileViewModel : ResponseModel
    {
        public List<InspectionPlanFileModel> List { get; set; }
        public InspectionPlanFileFilterModel Filter { get; set; }
    }

    public class InspectionPlanFileModel
    {
        public int InspectionPlanFileID { get; set; }
        public string RecObjectName { get; set; }
        public string UserSAP { get; set; }
        public string InspectionPlanFilePath { get; set; }
        public decimal? InspectionPlanFileVersion { get; set; }
        public int InspectionPlanFileStatus { get; set; }
        public int ProductsTypeID { get; set; }
        public DateTime? ValidDate { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public FileStatusModel FileStatus { get; set; }
    }

    public class InspectionPlanFileFilterModel
    {
        public string Keywords { get; set; }
        public int? InspectionPlanFileID { get; set; }
        public int[] InspectionPlanFileIDs { get; set; }
        public string RecObjectName { get; set; }
        public string UserSAP { get; set; }
        public string InspectionPlanFilePath { get; set; }
        public decimal? InspectionPlanFileVersion { get; set; }
        public int? InspectionPlanFileStatus { get; set; }
        public int? ProductsTypeID { get; set; }
        public DateTime? ValidDate { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public FileStatusModel FileStatus { get; set; }
        public PaginationModel Pagination { get; set; }
        public string Order { get; set; }
        public string Sort { get; set; }
        public decimal? LastInspectionPlanFileVersion { get; set; } 

        public InspectionPlanFileFilterModel()
        {
            Order = "CreatedDate";
            Sort = "asc";
            Pagination = new PaginationModel();
        }
    }
}