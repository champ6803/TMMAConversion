using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMMAConversions.DAL.Models
{
    public class AssignMaterialToRoutingFileViewModel : ResponseModel
    {
        public List<AssignMaterialToRoutingFileModel> List { get; set; }
        public AssignMaterialToRoutingFileFilterModel Filter { get; set; }
    }

    public class AssignMaterialToRoutingFileModel
    {
        public int AssignMaterialToRoutingFileID { get; set; }
        public string RecObjectName { get; set; }
        public string UserSAP { get; set; }
        public string AssignMaterialToRoutingFilePath { get; set; }
        public decimal? AssignMaterialToRoutingFileVersion { get; set; }
        public int AssignMaterialToRoutingFileStatus { get; set; }
        public int ProductsTypeID { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public FileStatusModel FileStatus { get; set; }
    }

    public class AssignMaterialToRoutingFileFilterModel
    {
        public string Keywords { get; set; }
        public int? AssignMaterialToRoutingFileID { get; set; }
        public int[] AssignMaterialToRoutingFileIDs { get; set; }
        public string RecObjectName { get; set; }
        public string UserSAP { get; set; }
        public string AssignMaterialToRoutingFilePath { get; set; }
        public decimal? AssignMaterialToRoutingFileVersion { get; set; }
        public int? AssignMaterialToRoutingFileStatus { get; set; }
        public int? ProductsTypeID { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public FileStatusModel FileStatus { get; set; }
        public PaginationModel Pagination { get; set; }

        public AssignMaterialToRoutingFileFilterModel()
        {
            Pagination = new PaginationModel();
        }
    }
}