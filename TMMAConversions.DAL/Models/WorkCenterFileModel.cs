using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMMAConversions.DAL.Models
{
    public class WorkCenterFileViewModel : ResponseModel
    {
        public List<WorkCenterFileModel> List { get; set; }
        public WorkCenterFileFilterModel Filter { get; set; }
    }

    public class WorkCenterFileModel
    {
        public int WorkCenterFileID { get; set; }
        public string RecObjectName { get; set; }
        public string UserSAP { get; set; }
        public string WorkCenterFilePath { get; set; }
        public decimal? WorkCenterFileVersion { get; set; }
        public int WorkCenterFileStatus { get; set; }
        public int ProductsTypeID { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public FileStatusModel FileStatus { get; set; }
    }

    public class WorkCenterFileFilterModel
    {
        public string Keywords { get; set; }
        public int? WorkCenterFileID { get; set; }
        public int[] WorkCenterFileIDs { get; set; }
        public string RecObjectName { get; set; }
        public string UserSAP { get; set; }
        public string WorkCenterFilePath { get; set; }
        public decimal? WorkCenterFileVersion { get; set; }
        public int? WorkCenterFileStatus { get; set; }
        public int? ProductsTypeID { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public FileStatusModel FileStatus { get; set; }
        public PaginationModel Pagination { get; set; }
        public string Order { get; set; }
        public string Sort { get; set; }
        public decimal? LastWorkCenterFileVersion { get; set; }

        public WorkCenterFileFilterModel()
        {
            Order = "CreatedDate";
            Sort = "asc";
            Pagination = new PaginationModel();
        }
    }
}