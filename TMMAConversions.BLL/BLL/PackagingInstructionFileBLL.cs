using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMMAConversions.DAL.DAL;
using TMMAConversions.DAL.Models;

namespace TMMAConversions.BLL.BLL
{
    public class PackagingInstructionFileBLL
    {
        private static string SOURCE = "PackagingInstructionFileBLL";
        private static string ACTION = "";

        public static PackagingInstructionFileViewModel GetPackagingInstructionFileView(PackagingInstructionFileFilterModel filter)
        {
            ACTION = "GetPackagingInstructionFileView(PackagingInstructionFileFilterModel)";
            try
            {
                ResponseModel response = new ResponseModel();
                int totalRecord = 0;
                decimal lastVersion = 0;

                var model = PackagingInstructionFileDAL.GetPackagingInstructionFileList(filter, ref totalRecord, ref response, ref lastVersion);
                filter.Pagination.TotalRecord = totalRecord;

                PaginationModel pagination = new PaginationModel(totalRecord, filter.Pagination.Page, filter.Pagination.Take);
                filter.Pagination = pagination;
                filter.LastPackagingInstructionFileVersion = lastVersion;

                return new PackagingInstructionFileViewModel()
                {
                    Action = ACTION,
                    Source = SOURCE,
                    Status = response.Status,
                    Message = response.Message,
                    List = model,
                    Filter = filter
                };
            }
            catch (Exception ex)
            {
                return new PackagingInstructionFileViewModel()
                {
                    Action = ACTION,
                    Source = SOURCE,
                    Status = false,
                    Message = ex.Message,
                    List = null,
                    Filter = filter
                };
            }
        }

        public static PackagingInstructionFileModel GetPackagingInstructionFile(int PackagingInstructionFileID)
        {
            ACTION = "GetPackagingInstructionFile(PackagingInstructionFileID)";
            try
            {
                return PackagingInstructionFileDAL.GetPackagingInstructionFile(PackagingInstructionFileID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ResponseModel AddPackagingInstructionFile(PackagingInstructionFileModel m, ref int _newID)
        {
            ACTION = "AddPackagingInstructionFile(PackagingInstructionFileModel)";
            try
            {
                _newID = 0;
                ResponseModel res = PackagingInstructionFileDAL.AddPackagingInstructionFile(m, ref _newID);
                return new ResponseModel()
                {
                    Action = ACTION,
                    Source = SOURCE,
                    Message = res.Message,
                    Status = res.Status
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel()
                {
                    Action = ACTION,
                    Source = SOURCE,
                    Message = ex.Message,
                    Status = false
                };
            }
        }

        public static ResponseModel UpdateStatusPackagingInstructionFile(int PackagingInstructionFileID, int PackagingInstructionFileStatus)
        {
            ACTION = "UpdateStatusPackagingInstructionFile(PackagingInstructionFileID, PackagingInstructionFileStatus)";
            try
            {
                ResponseModel res = PackagingInstructionFileDAL.UpdateStatusPackagingInstructionFile(PackagingInstructionFileID, PackagingInstructionFileStatus);
                return new ResponseModel()
                {
                    Action = ACTION,
                    Source = SOURCE,
                    Message = res.Message,
                    Status = res.Status
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel()
                {
                    Action = ACTION,
                    Source = SOURCE,
                    Message = ex.Message,
                    Status = false
                };
            }
        }

        public static PackagingInstructionFileModel GetPackagingInstructionFileLastVersion(int ProductsTypeID)
        {
            return PackagingInstructionFileDAL.GetPackagingInstructionFileLastVersion(ProductsTypeID);
        }
    }
}