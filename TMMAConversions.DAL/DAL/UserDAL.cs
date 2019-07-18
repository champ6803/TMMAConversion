using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMMAConversions.DAL.Models;
using TMMAConversions.DAL.Entities;

namespace TMMAConversions.DAL.DAL
{
    public class UserDAL
    {
        static string SOURCE = "UserDAL";
        static string ACTION = "";

        internal static UserModel Mapping(USR_TMMA_USER o)
        {
            try
            {
                if (o != null)
                {
                    return new UserModel()
                    {
                        UserID = o.UserID,
                        RoleID = o.RoleID,
                        Username = o.Username,
                        PasswordHash = o.PasswordHash,
                        Salt = o.Salt,
                        IsActive = o.IsActive == 1,
                        CreatedBy = o.CreatedBy,
                        CreatedDate = o.CreatedDate,
                        UpdatedBy = o.UpdatedBy,
                        UpdatedDate = o.UpdatedDate,
                        Role = MasterRoleDAL.GetMasterRole(o.RoleID)
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static List<UserModel> Mapping(List<USR_TMMA_USER> list)
        {
            ACTION = "Mapping(List<USR_TMMA_USER>)";

            try
            {
                if (list != null && list.Count > 0)
                {
                    List<UserModel> mList = new List<UserModel>();

                    foreach (USR_TMMA_USER o in list)
                    {
                        mList.Add(new UserModel()
                        {
                            UserID = o.UserID,
                            RoleID = o.RoleID,
                            Username = o.Username,
                            PasswordHash = o.PasswordHash,
                            Salt = o.Salt,
                            IsActive = o.IsActive == 1,
                            CreatedBy = o.CreatedBy,
                            CreatedDate = o.CreatedDate,
                            UpdatedBy = o.UpdatedBy,
                            UpdatedDate = o.UpdatedDate
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

        public static UserModel GetUser(int userID)
        {
            ACTION = "GetUser(userID)";

            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    USR_TMMA_USER obj = context.USR_TMMA_USER.Where(o => o.UserID == userID).FirstOrDefault();
                    UserModel m = Mapping(obj);
                    return m;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static UserModel GetUserByUsername(string username)
        {
            ACTION = "GetUserByUsername(username)";

            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    USR_TMMA_USER obj = context.USR_TMMA_USER.Where(x => string.Compare(x.Username, username, true) == 0).FirstOrDefault();
                    UserModel m = Mapping(obj);
                    return m;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static UserModel GetUserByUsernamePassword(string username, string password)
        {
            ACTION = "GetUserByUsernamePassword(username, password)";

            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    USR_TMMA_USER obj = context.USR_TMMA_USER.Where(o => o.Username == username && o.PasswordHash == password).FirstOrDefault();
                    UserModel m = Mapping(obj);
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