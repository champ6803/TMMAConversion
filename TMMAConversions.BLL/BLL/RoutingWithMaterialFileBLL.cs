using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMMAConversions.DAL.DAL;
using TMMAConversions.DAL.Models;

namespace TMMAConversions.BLL.BLL
{
    public class RoutingWithMaterialFileBLL
    {
        private static string SOURCE = "RoutingWithMaterialFileBLL";
        private static string ACTION = "";

        public static RoutingWithMaterialFileViewModel GetRoutingWithMaterialFileView(RoutingWithMaterialFileFilterModel filter)
        {
            ACTION = "GetRoutingWithMaterialFileView(RoutingWithMaterialFileFilterModel)";
            try
            {
                ResponseModel response = new ResponseModel();
                int totalRecord = 0;

                var model = RoutingWithMaterialFileDAL.GetRoutingWithMaterialFileList(filter, ref totalRecord, ref response);
                filter.Pagination.TotalRecord = totalRecord;

                PaginationModel pagination = new PaginationModel(totalRecord, filter.Pagination.Page, filter.Pagination.Take);

                filter.Pagination = pagination;

                return new RoutingWithMaterialFileViewModel()
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
                return new RoutingWithMaterialFileViewModel()
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

        public static RoutingWithMaterialFileModel GetRoutingWithMaterialFile(int RoutingWithMaterialFileID)
        {
            ACTION = "GetRoutingWithMaterialFile(RoutingWithMaterialFileID)";
            try
            {
                return RoutingWithMaterialFileDAL.GetRoutingWithMaterialFile(RoutingWithMaterialFileID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ResponseModel AddRoutingWithMaterialFile(RoutingWithMaterialFileModel m, ref int _newID)
        {
            ACTION = "AddRoutingWithMaterialFile(RoutingWithMaterialFileModel)";
            try
            {
                _newID = 0;
                ResponseModel res = RoutingWithMaterialFileDAL.AddRoutingWithMaterialFile(m, ref _newID);
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

        public static ResponseModel UpdateStatusRoutingWithMaterialFile(int RoutingWithMaterialFileID, int RoutingWithMaterialFileStatus)
        {
            ACTION = "UpdateStatusRoutingWithMaterialFile(RoutingWithMaterialFileID, RoutingWithMaterialFileStatus)";
            try
            {
                ResponseModel res = RoutingWithMaterialFileDAL.UpdateStatusRoutingWithMaterialFile(RoutingWithMaterialFileID, RoutingWithMaterialFileStatus);
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

        public static RoutingWithMaterialFileModel GetRoutingWithMaterialFileLastVersion(int ProductsTypeID)
        {
            return RoutingWithMaterialFileDAL.GetRoutingWithMaterialFileLastVersion(ProductsTypeID);
        }
    }
}