using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMMAConversions.DAL.DAL;
using TMMAConversions.DAL.Models;

namespace TMMAConversions.BLL.BLL
{
    public class WorkCenterFileBLL
    {
        private static string SOURCE = "WorkCenterFileBLL";
        private static string ACTION = "";

        public static WorkCenterFileViewModel GetWorkCenterFileView(WorkCenterFileFilterModel filter)
        {
            ACTION = "GetWorkCenterFileView(WorkCenterFileFilterModel)";
            try
            {
                ResponseModel response = new ResponseModel();
                int totalRecord = 0;
                decimal lastVersion = 0;

                var model = WorkCenterFileDAL.GetWorkCenterFileList(filter, ref totalRecord, ref response, ref lastVersion);
                filter.Pagination.TotalRecord = totalRecord;

                PaginationModel pagination = new PaginationModel(totalRecord, filter.Pagination.Page, filter.Pagination.Take);
                filter.Pagination = pagination;
                filter.LastWorkCenterFileVersion = lastVersion;
    
                return new WorkCenterFileViewModel()
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
                return new WorkCenterFileViewModel()
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

        public static ResponseModel AddWorkCenterFile(WorkCenterFileModel m, ref int _newID)
        {
            ACTION = "AddWorkCenterFile(WorkCenterFileModel)";
            try
            {
                _newID = 0;
                ResponseModel res = WorkCenterFileDAL.AddWorkCenterFile(m, ref _newID);
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

        public static ResponseModel UpdateStatusWorkCenterFile(int WorkCenterFileID, int WorkCenterFileStatus)
        {
            ACTION = "UpdateStatusWorkCenterFile(WorkCenterFileID, WorkCenterFileStatus)";
            try
            {
                ResponseModel res = WorkCenterFileDAL.UpdateStatusWorkCenterFile(WorkCenterFileID, WorkCenterFileStatus);
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

        public static WorkCenterFileModel GetWorkCenterFileLastVersion()
        {
            ACTION = "GetWorkCenterFileLastVersion()";
            try
            {
                return WorkCenterFileDAL.GetWorkCenterFileLastVersion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}