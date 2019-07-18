using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMMAConversions.DAL.DAL;
using TMMAConversions.DAL.Models;

namespace TMMAConversions.BLL.BLL
{
    public class WorkCenterRoutingFileBLL
    {
        private static string SOURCE = "WorkCenterRoutingFileBLL";
        private static string ACTION = "";

        public static WorkCenterRoutingFileViewModel GetWorkCenterRoutingFileView(WorkCenterRoutingFileFilterModel filter)
        {
            ACTION = "GetWorkCenterRoutingFileView(WorkCenterRoutingFileFilterModel)";
            try
            {
                ResponseModel response = new ResponseModel();
                int totalRecord = 0;
                decimal lastVersion = 0;

                var model = WorkCenterRoutingFileDAL.GetWorkCenterRoutingFileList(filter, ref totalRecord, ref response, ref lastVersion);
                filter.Pagination.TotalRecord = totalRecord;

                PaginationModel pagination = new PaginationModel(totalRecord, filter.Pagination.Page, filter.Pagination.Take);
                filter.Pagination = pagination;
                filter.LastWorkCenterRoutingFileVersion = lastVersion;
    
                return new WorkCenterRoutingFileViewModel()
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
                return new WorkCenterRoutingFileViewModel()
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

        public static ResponseModel AddWorkCenterRoutingFile(WorkCenterRoutingFileModel m, ref int _newID)
        {
            ACTION = "AddWorkCenterRoutingFile(WorkCenterRoutingFileModel)";
            try
            {
                _newID = 0;
                ResponseModel res = WorkCenterRoutingFileDAL.AddWorkCenterRoutingFile(m, ref _newID);
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

        public static ResponseModel UpdateStatusWorkCenterRoutingFile(int WorkCenterRoutingFileID, int WorkCenterRoutingFileStatus)
        {
            ACTION = "UpdateStatusWorkCenterRoutingFile(WorkCenterRoutingFileID, WorkCenterRoutingFileStatus)";
            try
            {
                ResponseModel res = WorkCenterRoutingFileDAL.UpdateStatusWorkCenterRoutingFile(WorkCenterRoutingFileID, WorkCenterRoutingFileStatus);
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

        public static WorkCenterRoutingFileModel GetWorkCenterRoutingFileLastVersion()
        {
            ACTION = "GetWorkCenterRoutingFileLastVersion()";
            try
            {
                return WorkCenterRoutingFileDAL.GetWorkCenterRoutingFileLastVersion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}