using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMMAConversions.DAL.DAL;
using TMMAConversions.DAL.Models;

namespace TMMAConversions.BLL.BLL
{
    public class RoutingWithoutMaterialFileBLL
    {
        private static string SOURCE = "RoutingWithoutMaterialFileBLL";
        private static string ACTION = "";

        public static RoutingWithoutMaterialFileViewModel GetRoutingWithoutMaterialFileView(RoutingWithoutMaterialFileFilterModel filter)
        {
            ACTION = "GetRoutingWithoutMaterialFileView(RoutingWithoutMaterialFileFilterModel)";
            try
            {
                ResponseModel response = new ResponseModel();
                int totalRecord = 0;

                var model = RoutingWithoutMaterialFileDAL.GetRoutingWithoutMaterialFileList(filter, ref totalRecord, ref response);
                filter.Pagination.TotalRecord = totalRecord;

                PaginationModel pagination = new PaginationModel(totalRecord, filter.Pagination.Page, filter.Pagination.Take);

                filter.Pagination = pagination;

                return new RoutingWithoutMaterialFileViewModel()
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
                return new RoutingWithoutMaterialFileViewModel()
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

        public static RoutingWithoutMaterialFileModel GetRoutingWithoutMaterialFile(int RoutingWithoutMaterialFileID)
        {
            ACTION = "GetRoutingWithoutMaterialFile(RoutingWithoutMaterialFileID)";
            try
            {
                return RoutingWithoutMaterialFileDAL.GetRoutingWithoutMaterialFile(RoutingWithoutMaterialFileID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ResponseModel AddRoutingWithoutMaterialFile(RoutingWithoutMaterialFileModel m, ref int _newID)
        {
            ACTION = "AddRoutingWithoutMaterialFile(RoutingWithoutMaterialFileModel)";
            try
            {
                _newID = 0;
                ResponseModel res = RoutingWithoutMaterialFileDAL.AddRoutingWithoutMaterialFile(m, ref _newID);
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

        public static ResponseModel UpdateStatusRoutingWithoutMaterialFile(int RoutingWithoutMaterialFileID, int RoutingWithoutMaterialFileStatus)
        {
            ACTION = "UpdateStatusRoutingWithoutMaterialFile(RoutingWithoutMaterialFileID, RoutingWithoutMaterialFileStatus)";
            try
            {
                ResponseModel res = RoutingWithoutMaterialFileDAL.UpdateStatusRoutingWithoutMaterialFile(RoutingWithoutMaterialFileID, RoutingWithoutMaterialFileStatus);
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

        public static RoutingWithoutMaterialFileModel GetRoutingWithoutMaterialFileLastVersion(int ProductsTypeID)
        {
            return RoutingWithoutMaterialFileDAL.GetRoutingWithoutMaterialFileLastVersion(ProductsTypeID);
        }
    }
}