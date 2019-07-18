using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMMAConversions.DAL.Models;
using TMMAConversions.DAL.Entities;

namespace TMMAConversions.DAL.DAL
{
    public class MasterRoleDAL
    {
        static string SOURCE = "MasterRoleDAL";
        static string ACTION = "";

        internal static MasterRoleModel Mapping(USR_TMMA_MASTER_ROLE o)
        {
            try
            {
                if (o != null)
                {
                    return new MasterRoleModel()
                    {
                        RoleID = o.RoleID,
                        RoleName = o.RoleName,
                        IsActive = o.IsActive == 1,
                        CreatedBy = o.CreatedBy,
                        CreatedDate = o.CreatedDate,
                        UpdatedBy = o.UpdatedBy,
                        UpdatedDate = o.UpdatedDate,
                        RolePermissionList = RolePermissionDAL.GetRolePermissionByRoleIDList(o.RoleID)
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MasterRoleModel GetMasterRole(int roleID)
        {
            ACTION = "GetMasterRole(roleID)";

            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    USR_TMMA_MASTER_ROLE obj = context.USR_TMMA_MASTER_ROLE.Where(o => o.RoleID == roleID).FirstOrDefault();
                    MasterRoleModel m = Mapping(obj);
                    return m;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}