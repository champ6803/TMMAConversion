using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMMAConversions.DAL.Models
{
    public class BOMFileViewModel : ResponseModel
    {
        public List<BOMFileModel> List { get; set; }
        public BOMFileFilterModel Filter { get; set; }
    }

    public class BOMFileModel
    {
        public int BOMFileID { get; set; }
        public string RecObjectName { get; set; }
        public string UserSAP { get; set; }
        public string BOMFilePath { get; set; }
        public decimal? BOMFileVersion { get; set; }
        public int BOMFileStatus { get; set; }
        public int ProductsTypeID { get; set; }
        public DateTime? ValidDate { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public FileStatusModel FileStatus { get; set; }
    }

    public class BOMFileFilterModel
    {
        public string Keywords { get; set; }
        public int? BOMFileID { get; set; }
        public int[] BOMFileIDs { get; set; }
        public string RecObjectName { get; set; }
        public string UserSAP { get; set; }
        public string BOMFilePath { get; set; }
        public decimal? BOMFileVersion { get; set; }
        public int? BOMFileStatus { get; set; }
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
        public decimal? LastBOMFileVersion { get; set; }

        public BOMFileFilterModel()
        {
            Order = "CreatedDate";
            Sort = "asc";
            Pagination = new PaginationModel();
        }
    }
}