using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMMAConversions.DAL.Models
{
    public class ProductionVersionFileViewModel : ResponseModel
    {
        public List<ProductionVersionFileModel> List { get; set; }
        public ProductionVersionFileFilterModel Filter { get; set; }
    }

    public class ProductionVersionFileModel
    {
        public int ProductionVersionFileID { get; set; }
        public string RecObjectName { get; set; }
        public string UserSAP { get; set; }
        public string ProductionVersionFilePath { get; set; }
        public decimal? ProductionVersionFileVersion { get; set; }
        public int ProductionVersionFileStatus { get; set; }
        public int ProductsTypeID { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public FileStatusModel FileStatus { get; set; }
        public DateTime? ValidDate { get; set; }
    }

    public class ProductionVersionFileFilterModel
    {
        public string Keywords { get; set; }
        public int? ProductionVersionFileID { get; set; }
        public int[] ProductionVersionFileIDs { get; set; }
        public string RecObjectName { get; set; }
        public string UserSAP { get; set; }
        public string ProductionVersionFilePath { get; set; }
        public decimal? ProductionVersionFileVersion { get; set; }
        public int? ProductionVersionFileStatus { get; set; }
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
        public decimal? LastProductionVersionFileVersion { get; set; } 
        public ProductionVersionFileFilterModel()
        {
            Order = "CreatedDate";
            Sort = "asc";
            Pagination = new PaginationModel();
        }
    }
}