using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMMAConversions.DAL.Models;
using TMMAConversions.DAL.Entities;

namespace TMMAConversions.DAL.DAL
{
    public class MasterPermissionDAL
    {
        static string SOURCE = "MasterPermissionDAL";
        static string ACTION = "";

        internal static MasterPermissionModel Mapping(USR_TMMA_MASTER_PERMISSION o)
        {
            try
            {
                if (o != null)
                {
                    return new MasterPermissionModel()
                    {
                        PermissionID = o.PermissionID,
                        PermissionName = o.PermissionName,
                        IsActive = o.IsActive == 1,
                        CreatedBy = o.CreatedBy,
                        CreatedDate = o.CreatedDate,
                        UpdatedBy = o.UpdatedBy,
                        UpdatedDate = o.UpdatedDate
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MasterPermissionModel GetMasterPermission(int permissionID)
        {
            ACTION = "GetMasterPermission(permissionID)";

            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    USR_TMMA_MASTER_PERMISSION obj = context.USR_TMMA_MASTER_PERMISSION.Where(o => o.PermissionID == permissionID).FirstOrDefault();
                    MasterPermissionModel m = Mapping(obj);
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