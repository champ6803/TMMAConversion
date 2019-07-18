using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMMAConversions.DAL.Models
{
    public class RoutingFileViewModel : ResponseModel
    {
        public List<RoutingFileModel> List { get; set; }
        public RoutingFileFilterModel Filter { get; set; }
    }

    public class RoutingFileModel
    {
        public int RoutingFileID { get; set; }
        public string RecObjectName { get; set; }
        public string UserSAP { get; set; }
        public string RoutingFilePath { get; set; }
        public decimal? RoutingFileVersion { get; set; }
        public int RoutingFileStatus { get; set; }
        public int ProductsTypeID { get; set; }
        public DateTime? ValidDate { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public FileStatusModel FileStatus { get; set; }
    }

    public class RoutingFileFilterModel
    {
        public string Keywords { get; set; }
        public int? RoutingFileID { get; set; }
        public int[] RoutingFileIDs { get; set; }
        public string RecObjectName { get; set; }
        public string UserSAP { get; set; }
        public string RoutingFilePath { get; set; }
        public decimal? RoutingFileVersion { get; set; }
        public int? RoutingFileStatus { get; set; }
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
        public decimal? LastRoutingFileVersion { get; set; }

        public RoutingFileFilterModel()
        {
            Order = "CreatedDate";
            Sort = "asc";
            Pagination = new PaginationModel();
        }
    }
}