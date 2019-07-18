using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMMAConversions.DAL.Models
{
    public class UserViewModel : ResponseModel
    {
        public List<UserModel> List { get; set; }
        public UserFilterModel Filter { get; set; }
    }

    public class UserModel
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public string Username  { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; } 
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public MasterRoleModel Role { get; set; }
    }

    public class UserFilterModel
    {
        public string Keywords { get; set; }
        public int? UserID { get; set; }
        public int[] UserIDs { get; set; }
        public int? RoleID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public PaginationModel Pagination = new PaginationModel();

        public UserFilterModel()
        {
            Pagination = new PaginationModel();
        }
    }
}