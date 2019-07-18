using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMMAConversions.DAL.Models
{
    public class MasterRoleViewModel : ResponseModel
    {
        public List<MasterRoleModel> List { get; set; }
        public MasterRoleFilterModel Filter { get; set; }
    }

    public class MasterRoleModel
    {
        public int RoleID { get; set; }
        public string RoleName  { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public List<RolePermissionModel> RolePermissionList { get; set; }
    }

    public class MasterRoleFilterModel
    {
        public string Keywords { get; set; }
        public int? RoleID { get; set; }
        public int[] RoleIDs { get; set; }
        public string RoleName { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public PaginationModel Pagination = new PaginationModel();

        public MasterRoleFilterModel()
        {
            Pagination = new PaginationModel();
        }
    }
}