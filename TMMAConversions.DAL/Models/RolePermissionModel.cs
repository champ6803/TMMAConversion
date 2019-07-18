using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMMAConversions.DAL.Models
{
    public class RolePermissionViewModel : ResponseModel
    {
        public List<RolePermissionModel> List { get; set; }
        public RolePermissionFilterModel Filter { get; set; }
    }

    public class RolePermissionModel
    {
        public int RolePermissionID { get; set; }
        public int RoleID { get; set; }
        public int PermissionID  { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public MasterPermissionModel Permission { get; set; }

        public MasterRoleModel Role { get; set; }
    }

    public class RolePermissionFilterModel
    {
        public string Keywords { get; set; }
        public int? RolePermissionID { get; set; }
        public int[] RolePermissionIDs { get; set; }
        public int? RoleID { get; set; }
        public int? PermissionID { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public PaginationModel Pagination = new PaginationModel();

        public RolePermissionFilterModel()
        {
            Pagination = new PaginationModel();
        }
    }
}