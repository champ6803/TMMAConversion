using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMMAConversions.DAL.Models
{
    public class PackagingInstructionFileViewModel : ResponseModel
    {
        public List<PackagingInstructionFileModel> List { get; set; }
        public PackagingInstructionFileFilterModel Filter { get; set; }
    }

    public class PackagingInstructionFileModel
    {
        public int PackagingInstructionFileID { get; set; }
        public string RecObjectName { get; set; }
        public string UserSAP { get; set; }
        public string PackagingInstructionFilePath { get; set; }
        public decimal? PackagingInstructionFileVersion { get; set; }
        public int PackagingInstructionFileStatus { get; set; }
        public int ProductsTypeID { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public FileStatusModel FileStatus { get; set; }
    }

    public class PackagingInstructionFileFilterModel
    {
        public string Keywords { get; set; }
        public int? PackagingInstructionFileID { get; set; }
        public int[] PackagingInstructionFileIDs { get; set; }
        public string RecObjectName { get; set; }
        public string UserSAP { get; set; }
        public string PackagingInstructionFilePath { get; set; }
        public decimal? PackagingInstructionFileVersion { get; set; }
        public int? PackagingInstructionFileStatus { get; set; }
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
        public decimal? LastPackagingInstructionFileVersion { get; set; }

        public PackagingInstructionFileFilterModel()
        {
            Order = "CreatedDate";
            Sort = "asc";
            Pagination = new PaginationModel();
        }
    }
}