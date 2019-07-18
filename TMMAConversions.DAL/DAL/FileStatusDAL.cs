using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMMAConversions.DAL.Models;
using TMMAConversions.DAL.Entities;

namespace TMMAConversions.DAL.DAL
{
    public class FileStatusDAL
    {
        static string SOURCE = "FileStatusDAL";
        static string ACTION = "";

        internal static FileStatusModel Mapping(USR_TMMA_FILE_STATUS o)
        {
            try
            {
                if (o != null)
                {
                    return new FileStatusModel()
                    {
                        FileStatusID = o.FileStatusID,
                        FileStatusName = o.FileStatusName,
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

        public static FileStatusModel GetFileStatus(int fileStatusID)
        {
            ACTION = "GetFileStatus(fileStatusID)";

            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    USR_TMMA_FILE_STATUS obj = context.USR_TMMA_FILE_STATUS.Where(o => o.FileStatusID == fileStatusID).FirstOrDefault();
                    FileStatusModel m = Mapping(obj);
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