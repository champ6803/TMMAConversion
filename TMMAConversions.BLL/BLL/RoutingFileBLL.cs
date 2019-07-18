using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMMAConversions.DAL.DAL;
using TMMAConversions.DAL.Models;

namespace TMMAConversions.BLL.BLL
{
    public class RoutingFileBLL
    {
        private static string SOURCE = "RoutingFileBLL";
        private static string ACTION = "";

        public static RoutingFileViewModel GetRoutingFileView(RoutingFileFilterModel filter)
        {
            ACTION = "GetRoutingFileView(RoutingFileFilterModel)";
            try
            {
                ResponseModel response = new ResponseModel();
                int totalRecord = 0;
                decimal lastVersion = 0;

                var model = RoutingFileDAL.GetRoutingFileList(filter, ref totalRecord, ref response, ref lastVersion);
                filter.Pagination.TotalRecord = totalRecord;

                PaginationModel pagination = new PaginationModel(totalRecord, filter.Pagination.Page, filter.Pagination.Take);
                filter.Pagination = pagination;
                filter.LastRoutingFileVersion = lastVersion;

                return new RoutingFileViewModel()
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
                return new RoutingFileViewModel()
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

        public static RoutingFileModel GetRoutingFile(int RoutingFileID)
        {
            ACTION = "GetRoutingFile(RoutingFileID)";
            try
            {
                return RoutingFileDAL.GetRoutingFile(RoutingFileID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ResponseModel AddRoutingFile(RoutingFileModel m, ref int _newID)
        {
            ACTION = "AddRoutingFile(RoutingFileModel)";
            try
            {
                _newID = 0;
                ResponseModel res = RoutingFileDAL.AddRoutingFile(m, ref _newID);
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

        public static ResponseModel UpdateStatusRoutingFile(int RoutingFileID, int RoutingFileStatus)
        {
            ACTION = "UpdateStatusRoutingFile(RoutingFileID, RoutingFileStatus)";
            try
            {
                ResponseModel res = RoutingFileDAL.UpdateStatusRoutingFile(RoutingFileID, RoutingFileStatus);
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

        public static RoutingFileModel GetRoutingFileLastVersion(int ProductsTypeID)
        {
            return RoutingFileDAL.GetRoutingFileLastVersion(ProductsTypeID);
        }
    }
}