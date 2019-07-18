using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMMAConversions.DAL.Models
{
    public class MasterPermissionViewModel : ResponseModel
    {
        public List<MasterPermissionModel> List { get; set; }
        public MasterPermissionFilterModel Filter { get; set; }
    }

    public class MasterPermissionModel
    {
        public int PermissionID { get; set; }
        public string PermissionName  { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public class MasterPermissionFilterModel
    {
        public string Keywords { get; set; }
        public int? PermissionID { get; set; }
        public int[] PermissionIDs { get; set; }
        public string PermissionName { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public PaginationModel Pagination = new PaginationModel();

        public MasterPermissionFilterModel()
        {
            Pagination = new PaginationModel();
        }
    }
}