using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMMAConversions.DAL.Models
{
    public class WorkCenterRoutingFileViewModel : ResponseModel
    {
        public List<WorkCenterRoutingFileModel> List { get; set; }
        public WorkCenterRoutingFileFilterModel Filter { get; set; }
    }

    public class WorkCenterRoutingFileModel
    {
        public int WorkCenterRoutingFileID { get; set; }
        public string RecObjectName { get; set; }
        public string UserSAP { get; set; }
        public string WorkCenterRoutingFilePath { get; set; }
        public decimal? WorkCenterRoutingFileVersion { get; set; }
        public int WorkCenterRoutingFileStatus { get; set; }
        public int ProductsTypeID { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? ValidDate { get; set; }
        public FileStatusModel FileStatus { get; set; }
    }

    public class WorkCenterRoutingFileFilterModel
    {
        public string Keywords { get; set; }
        public int? WorkCenterRoutingFileID { get; set; }
        public int[] WorkCenterRoutingFileIDs { get; set; }
        public string RecObjectName { get; set; }
        public string UserSAP { get; set; }
        public string WorkCenterRoutingFilePath { get; set; }
        public decimal? WorkCenterRoutingFileVersion { get; set; }
        public int? WorkCenterRoutingFileStatus { get; set; }
        public int? ProductsTypeID { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? ValidDate { get; set; }
        public FileStatusModel FileStatus { get; set; }
        public PaginationModel Pagination { get; set; }
        public string Order { get; set; }
        public string Sort { get; set; }
        public decimal? LastWorkCenterRoutingFileVersion { get; set; }

        public WorkCenterRoutingFileFilterModel()
        {
            Order = "CreatedDate";
            Sort = "asc";
            Pagination = new PaginationModel();
        }
    }
}