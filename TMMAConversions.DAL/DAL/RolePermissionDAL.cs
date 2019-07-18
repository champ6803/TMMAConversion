using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMMAConversions.DAL.Models;
using TMMAConversions.DAL.Entities;

namespace TMMAConversions.DAL.DAL
{
    public class RolePermissionDAL
    {
        static string SOURCE = "RolePermissionDAL";
        static string ACTION = "";

        internal static RolePermissionModel Mapping(USR_TMMA_ROLE_PERMISSION o)
        {
            try
            {
                if (o != null)
                {
                    return new RolePermissionModel()
                    {
                        RolePermissionID = o.RolePermissionID,
                        RoleID = o.RoleID,
                        PermissionID = o.PermissionID,
                        IsActive = o.IsActive == 1,
                        CreatedBy = o.CreatedBy,
                        CreatedDate = o.CreatedDate,
                        UpdatedBy = o.UpdatedBy,
                        UpdatedDate = o.UpdatedDate,
                        Permission = MasterPermissionDAL.GetMasterPermission(o.PermissionID)
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static List<RolePermissionModel> Mapping(List<USR_TMMA_ROLE_PERMISSION> list)
        {
            ACTION = "Mapping(List<USR_TMMA_ROLE_PERMISSION>)";

            try
            {
                if (list != null && list.Count > 0)
                {
                    List<RolePermissionModel> mList = new List<RolePermissionModel>();

                    foreach (USR_TMMA_ROLE_PERMISSION o in list)
                    {
                        mList.Add(new RolePermissionModel()
                        {
                            RolePermissionID = o.RolePermissionID,
                            RoleID = o.RoleID,
                            PermissionID = o.PermissionID,
                            IsActive = o.IsActive == 1,
                            CreatedBy = o.CreatedBy,
                            CreatedDate = o.CreatedDate,
                            UpdatedBy = o.UpdatedBy,
                            UpdatedDate = o.UpdatedDate,
                            Permission = MasterPermissionDAL.GetMasterPermission(o.PermissionID)
                        });
                    }

                    return mList;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static RolePermissionModel GetRolePermission(int rolePermissionID)
        {
            ACTION = "GetRolePermission(rolePermissionID)";

            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    USR_TMMA_ROLE_PERMISSION obj = context.USR_TMMA_ROLE_PERMISSION.Where(o => o.RolePermissionID == rolePermissionID).FirstOrDefault();
                    RolePermissionModel m = Mapping(obj);
                    return m;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<RolePermissionModel> GetRolePermissionByRoleIDList(int roleID)
        {
            ACTION = "GetRolePermissionByRoleIDList(roleID)";

            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    List<USR_TMMA_ROLE_PERMISSION> list = context.USR_TMMA_ROLE_PERMISSION.Where(o => o.RoleID == roleID).ToList();
                    List<RolePermissionModel> mList = Mapping(list);
                    return mList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}