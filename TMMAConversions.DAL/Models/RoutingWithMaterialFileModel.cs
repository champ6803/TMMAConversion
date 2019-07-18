using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMMAConversions.DAL.Models
{
    public class RoutingWithMaterialFileViewModel : ResponseModel
    {
        public List<RoutingWithMaterialFileModel> List { get; set; }
        public RoutingWithMaterialFileFilterModel Filter { get; set; }
    }

    public class RoutingWithMaterialFileModel
    {
        public int RoutingWithMaterialFileID { get; set; }
        public string RecObjectName { get; set; }
        public string UserSAP { get; set; }
        public string RoutingWithMaterialFilePath { get; set; }
        public decimal? RoutingWithMaterialFileVersion { get; set; }
        public int RoutingWithMaterialFileStatus { get; set; }
        public int ProductsTypeID { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public FileStatusModel FileStatus { get; set; }
    }

    public class RoutingWithMaterialFileFilterModel
    {
        public string Keywords { get; set; }
        public int? RoutingWithMaterialFileID { get; set; }
        public int[] RoutingWithMaterialFileIDs { get; set; }
        public string RecObjectName { get; set; }
        public string UserSAP { get; set; }
        public string RoutingWithMaterialFilePath { get; set; }
        public decimal? RoutingWithMaterialFileVersion { get; set; }
        public int? RoutingWithMaterialFileStatus { get; set; }
        public int? ProductsTypeID { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public FileStatusModel FileStatus { get; set; }
        public PaginationModel Pagination { get; set; }

        public RoutingWithMaterialFileFilterModel()
        {
            Pagination = new PaginationModel();
        }
    }
}