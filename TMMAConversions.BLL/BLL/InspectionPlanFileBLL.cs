using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMMAConversions.DAL.DAL;
using TMMAConversions.DAL.Models;

namespace TMMAConversions.BLL.BLL
{
    public class InspectionPlanFileBLL
    {
        private static string SOURCE = "BOMFileBLL";
        private static string ACTION = "";

        public static InspectionPlanFileViewModel GetInspectionPlanFileView(InspectionPlanFileFilterModel filter)
        {
            ACTION = "GetInspectionPlanFileView(InspectionPlanFileFilterModel)";
            try
            {
                ResponseModel response = new ResponseModel();
                int totalRecord = 0;
                decimal lastVersion = 0;

                var model = InspectionPlanFileDAL.GetInspectionPlanFileList(filter, ref totalRecord, ref response, ref lastVersion);
                filter.Pagination.TotalRecord = totalRecord;

                PaginationModel pagination = new PaginationModel(totalRecord, filter.Pagination.Page, filter.Pagination.Take);
                filter.Pagination = pagination;
                filter.LastInspectionPlanFileVersion = lastVersion;

                return new InspectionPlanFileViewModel()
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
                return new InspectionPlanFileViewModel()
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

        public static InspectionPlanFileModel GetInspectionPlanFile(int InspectionPlanFileID)
        {
            ACTION = "GetInspectionPlanFile(InspectionPlanFileID)";
            try
            {
                return InspectionPlanFileDAL.GetInspectionPlanFile(InspectionPlanFileID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ResponseModel AddInspectionPlanFile(InspectionPlanFileModel m, ref int _newID)
        {
            ACTION = "AddInspectionPlanFile(InspectionPlanFileModel)";
            try
            {
                _newID = 0;
                ResponseModel res = InspectionPlanFileDAL.AddInspectionPlanFile(m, ref _newID);
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

        public static ResponseModel UpdateStatusInspectionPlanFile(int InspectionPlanFileID, int InspectionPlanFileStatus)
        {
            ACTION = "UpdateStatusInspectionPlanFile(InspectionPlanFileID, InspectionPlanFileStatus)";
            try
            {
                ResponseModel res = InspectionPlanFileDAL.UpdateStatusInspectionPlanFile(InspectionPlanFileID, InspectionPlanFileStatus);
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

        public static InspectionPlanFileModel GetInspectionPlanFileLastVersion(int ProductsTypeID)
        {
            return InspectionPlanFileDAL.GetInspectionPlanFileLastVersion(ProductsTypeID);
        }
    }
}