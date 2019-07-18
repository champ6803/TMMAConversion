using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMMAConversions.DAL.DAL;
using TMMAConversions.DAL.Models;

namespace TMMAConversions.BLL.BLL
{
    public class AssignMaterialToRoutingFileBLL
    {
        private static string SOURCE = "AssignMaterialToRoutingFileBLL";
        private static string ACTION = "";

        public static AssignMaterialToRoutingFileViewModel GetAssignMaterialToRoutingFileView(AssignMaterialToRoutingFileFilterModel filter)
        {
            ACTION = "GetAssignMaterialToRoutingFileView(AssignMaterialToRoutingFileFilterModel)";
            try
            {
                ResponseModel response = new ResponseModel();
                int totalRecord = 0;

                var model = AssignMaterialToRoutingFileDAL.GetAssignMaterialToRoutingFileList(filter, ref totalRecord, ref response);
                filter.Pagination.TotalRecord = totalRecord;

                PaginationModel pagination = new PaginationModel(totalRecord, filter.Pagination.Page, filter.Pagination.Take);

                filter.Pagination = pagination;

                return new AssignMaterialToRoutingFileViewModel()
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
                return new AssignMaterialToRoutingFileViewModel()
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

        public static AssignMaterialToRoutingFileModel GetAssignMaterialToRoutingFile(int AssignMaterialToRoutingFileID)
        {
            ACTION = "GetAssignMaterialToRoutingFile(AssignMaterialToRoutingFileID)";
            try
            {
                return AssignMaterialToRoutingFileDAL.GetAssignMaterialToRoutingFile(AssignMaterialToRoutingFileID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ResponseModel AddAssignMaterialToRoutingFile(AssignMaterialToRoutingFileModel m, ref int _newID)
        {
            ACTION = "AddAssignMaterialToRoutingFile(AssignMaterialToRoutingFileModel)";
            try
            {
                _newID = 0;
                ResponseModel res = AssignMaterialToRoutingFileDAL.AddAssignMaterialToRoutingFile(m, ref _newID);
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

        public static ResponseModel UpdateStatusAssignMaterialToRoutingFile(int AssignMaterialToRoutingFileID, int AssignMaterialToRoutingFileStatus)
        {
            ACTION = "UpdateStatusRoutingWithoutMaterialFile(RoutingWithoutMaterialFileID, RoutingWithoutMaterialFileStatus)";
            try
            {
                ResponseModel res = AssignMaterialToRoutingFileDAL.UpdateStatusAssignMaterialToRoutingFile(AssignMaterialToRoutingFileID, AssignMaterialToRoutingFileStatus);
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

        public static AssignMaterialToRoutingFileModel GetAssignMaterialToRoutingFileLastVersion(int ProductsTypeID)
        {
            return AssignMaterialToRoutingFileDAL.GetAssignMaterialToRoutingFileLastVersion(ProductsTypeID);
        }
    }
}