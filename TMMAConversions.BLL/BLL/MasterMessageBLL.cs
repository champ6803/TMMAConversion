using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMMAConversions.DAL.DAL;
using TMMAConversions.DAL.Models;

namespace TMMAConversions.BLL.BLL
{
    public class MasterMessageBLL
    {
        private static string SOURCE = "MasterMessageBLL";
        private static string ACTION = "";

        public static BOMFileViewModel GetBOMFileView(BOMFileFilterModel filter)
        {
            ACTION = "GetBOMFileView(BOMFileFilterModel)";
            try
            {
                ResponseModel response = new ResponseModel();
                int totalRecord = 0;
                decimal lastVersion = 0;

                var model = BOMFileDAL.GetBOMFileList(filter, ref totalRecord, ref response, ref lastVersion);
                filter.Pagination.TotalRecord = totalRecord;

                PaginationModel pagination = new PaginationModel(totalRecord, filter.Pagination.Page, filter.Pagination.Take);
                filter.Pagination = pagination;
                filter.LastBOMFileVersion = lastVersion;

                return new BOMFileViewModel()
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
                return new BOMFileViewModel()
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

        public static BOMFileModel GetBOMFile(int BOMFileID)
        {
            ACTION = "GetBOMFile(BOMFileID)";
            try
            {
                return BOMFileDAL.GetBOMFile(BOMFileID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ResponseModel AddBOMFile(BOMFileModel m, ref int _newID)
        {
            ACTION = "AddBOMFile(BOMFileModel)";
            try
            {
                _newID = 0;
                ResponseModel res = BOMFileDAL.AddBOMFile(m, ref _newID);
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

        public static ResponseModel UpdateStatusBOMFile(int BOMFileID, int BOMFileStatus)
        {
            ACTION = "UpdateStatusBOMFile(BOMFileID, BOMFileStatus)";
            try
            {
                ResponseModel res = BOMFileDAL.UpdateStatusBOMFile(BOMFileID, BOMFileStatus);
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

        public static BOMFileModel GetBOMFileLastVersion(int ProductsTypeID)
        {
            return BOMFileDAL.GetBOMFileLastVersion(ProductsTypeID);
        }
    }
}