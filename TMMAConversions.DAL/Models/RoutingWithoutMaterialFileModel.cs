using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMMAConversions.DAL.Models
{
    public class RoutingWithoutMaterialFileViewModel : ResponseModel
    {
        public List<RoutingWithoutMaterialFileModel> List { get; set; }
        public RoutingWithoutMaterialFileFilterModel Filter { get; set; }
    }

    public class RoutingWithoutMaterialFileModel
    {
        public int RoutingWithoutMaterialFileID { get; set; }
        public string RecObjectName { get; set; }
        public string UserSAP { get; set; }
        public string RoutingWithoutMaterialFilePath { get; set; }
        public decimal? RoutingWithoutMaterialFileVersion { get; set; }
        public int RoutingWithoutMaterialFileStatus { get; set; }
        public int ProductsTypeID { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public FileStatusModel FileStatus { get; set; }
    }

    public class RoutingWithoutMaterialFileFilterModel
    {
        public string Keywords { get; set; }
        public int? RoutingWithoutMaterialFileID { get; set; }
        public int[] RoutingWithoutMaterialFileIDs { get; set; }
        public string RecObjectName { get; set; }
        public string UserSAP { get; set; }
        public string RoutingWithoutMaterialFilePath { get; set; }
        public decimal? RoutingWithoutMaterialFileVersion { get; set; }
        public int? RoutingWithoutMaterialFileStatus { get; set; }
        public int? ProductsTypeID { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public FileStatusModel FileStatus { get; set; }
        public PaginationModel Pagination { get; set; }

        public RoutingWithoutMaterialFileFilterModel()
        {
            Pagination = new PaginationModel();
        }
    }
}