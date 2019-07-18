using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using TMMAConversions.DAL.Entities;
using TMMAConversions.DAL.Models;

namespace TMMAConversions.DAL.DAL
{
    public class WorkCenterRoutingFileDAL
    {
        private static string SOURCE = "WorkCenterRoutingFileDAL";
        private static string ACTION = "";

        internal static WorkCenterRoutingFileModel Mapping(USR_TMMA_WORKCENTER_ROUTING_FILE o)
        {
            try
            {
                if (o != null)
                {
                    return new WorkCenterRoutingFileModel()
                    {
                        WorkCenterRoutingFileID = o.WorkCenterRoutingFileID,
                        RecObjectName = o.RecObjectName,
                        UserSAP = o.UserSAP,
                        WorkCenterRoutingFilePath = o.WorkCenterRoutingFilePath,
                        WorkCenterRoutingFileVersion = o.WorkCenterRoutingFileVersion,
                        WorkCenterRoutingFileStatus = o.WorkCenterRoutingFileStatus,
                        FileStatus = FileStatusDAL.GetFileStatus(o.WorkCenterRoutingFileStatus),
                        ProductsTypeID = o.ProductsTypeID,
                        IsActive = o.IsActive == 1,
                        CreatedBy = o.CreatedBy,
                        CreatedDate = o.CreatedDate,
                        UpdatedBy = o.UpdatedBy,
                        UpdatedDate = o.UpdatedDate,
                        ValidDate = o.ValidDate
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static USR_TMMA_WORKCENTER_ROUTING_FILE Mapping(WorkCenterRoutingFileModel o)
        {
            try
            {
                if (o != null)
                {
                    return new USR_TMMA_WORKCENTER_ROUTING_FILE()
                    {
                        WorkCenterRoutingFileID = o.WorkCenterRoutingFileID,
                        RecObjectName = o.RecObjectName,
                        UserSAP = o.UserSAP,
                        WorkCenterRoutingFilePath = o.WorkCenterRoutingFilePath,
                        WorkCenterRoutingFileVersion = o.WorkCenterRoutingFileVersion,
                        WorkCenterRoutingFileStatus = o.WorkCenterRoutingFileStatus,
                        ProductsTypeID = o.ProductsTypeID,
                        IsActive = o.IsActive ? 1 : 0,
                        CreatedBy = o.CreatedBy,
                        CreatedDate = o.CreatedDate,
                        UpdatedBy = o.UpdatedBy,
                        UpdatedDate = o.UpdatedDate,
                        ValidDate = o.ValidDate
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static List<WorkCenterRoutingFileModel> Mapping(List<USR_TMMA_WORKCENTER_ROUTING_FILE> list)
        {
            ACTION = "Mapping(List<USR_TMMA_WORKCENTER_ROUTING_FILE>)";

            try
            {
                if (list != null && list.Count > 0)
                {
                    List<WorkCenterRoutingFileModel> mList = new List<WorkCenterRoutingFileModel>();

                    foreach (USR_TMMA_WORKCENTER_ROUTING_FILE o in list)
                    {
                        mList.Add(new WorkCenterRoutingFileModel()
                        {
                            WorkCenterRoutingFileID = o.WorkCenterRoutingFileID,
                            RecObjectName = o.RecObjectName,
                            UserSAP =  o.UserSAP,
                            WorkCenterRoutingFilePath = o.WorkCenterRoutingFilePath,
                            WorkCenterRoutingFileVersion = o.WorkCenterRoutingFileVersion,
                            WorkCenterRoutingFileStatus = o.WorkCenterRoutingFileStatus,
                            FileStatus = FileStatusDAL.GetFileStatus(o.WorkCenterRoutingFileStatus),
                            ProductsTypeID = o.ProductsTypeID,
                            IsActive = o.IsActive == 1,
                            CreatedBy = o.CreatedBy,
                            CreatedDate = o.CreatedDate,
                            UpdatedBy = o.UpdatedBy,
                            UpdatedDate = o.UpdatedDate,
                            ValidDate = o.ValidDate
                        });
                    }

                    return mList;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static WorkCenterRoutingFileModel GetWorkCenterRoutingFile(int workCenterRoutingFileID)
        {
            ACTION = "GetWorkCenterRoutingFile(workCenterRoutingFileID)";

            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    USR_TMMA_WORKCENTER_ROUTING_FILE obj = context.USR_TMMA_WORKCENTER_ROUTING_FILE.Where(o => o.WorkCenterRoutingFileID == workCenterRoutingFileID).FirstOrDefault();
                    WorkCenterRoutingFileModel m = Mapping(obj);
                    return m;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<WorkCenterRoutingFileModel> GetWorkCenterRoutingFileList(WorkCenterRoutingFileFilterModel filter, ref int totalRecord, ref ResponseModel response, ref decimal lastVersion)
        {
            ACTION = "GetWorkCenterRoutingFileList(WorkCenterRoutingFileFilterModel)";
            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    filter.WorkCenterRoutingFileIDs = filter.WorkCenterRoutingFileIDs != null ? filter.WorkCenterRoutingFileIDs : new int[] { };
                    var IQuery = context.USR_TMMA_WORKCENTER_ROUTING_FILE
                    .Where(o =>
                        (!string.IsNullOrEmpty(filter.Keywords) ?
                            (o.RecObjectName.Contains(filter.Keywords)) ||
                            (o.CreatedBy.Contains(filter.Keywords))
                        : true)
                        && (filter.WorkCenterRoutingFileID.HasValue ? o.WorkCenterRoutingFileID == filter.WorkCenterRoutingFileID.Value : true)
                        && (filter.WorkCenterRoutingFileIDs.Count() > 0 ? filter.WorkCenterRoutingFileIDs.Contains(o.WorkCenterRoutingFileID) : true)
                        && (!string.IsNullOrEmpty(filter.RecObjectName) ? filter.CreatedBy.Contains(o.RecObjectName) : true)
                        && (filter.ProductsTypeID.HasValue ? o.ProductsTypeID == filter.ProductsTypeID.Value : true)
                        && (filter.IsActive.HasValue ? o.IsActive == 1 : true)
                    );

                    totalRecord = IQuery.Count();

                    var lists = filter.Sort == "asc"
                        ? filter.Order == "RecObjectName" ? IQuery.OrderBy(o => o.RecObjectName)
                        : filter.Order == "UserSAP" ? IQuery.OrderBy(o => o.UserSAP)
                        : filter.Order == "Version" ? IQuery.OrderBy(o => o.WorkCenterRoutingFileVersion)
                        : filter.Order == "Status" ? IQuery.OrderBy(o => o.WorkCenterRoutingFileStatus)
                        : filter.Order == "CreatedBy" ? IQuery.OrderBy(o => o.CreatedBy)
                        : filter.Order == "CreatedDate" ? IQuery.OrderBy(o => o.CreatedDate)
                        : IQuery.OrderBy(o => o.CreatedDate)
                        : filter.Order == "RecObjectName" ? IQuery.OrderByDescending(o => o.RecObjectName)
                        : filter.Order == "UserSAP" ? IQuery.OrderByDescending(o => o.UserSAP)
                        : filter.Order == "Version" ? IQuery.OrderByDescending(o => o.WorkCenterRoutingFileVersion)
                        : filter.Order == "Status" ? IQuery.OrderByDescending(o => o.WorkCenterRoutingFileStatus)
                        : filter.Order == "CreatedBy" ? IQuery.OrderByDescending(o => o.CreatedBy)
                        : filter.Order == "CreatedDate" ? IQuery.OrderByDescending(o => o.CreatedDate)
                        : IQuery.OrderByDescending(o => o.CreatedDate);

                    lastVersion = IQuery.OrderByDescending(o => o.WorkCenterRoutingFileVersion).FirstOrDefault().WorkCenterRoutingFileVersion.GetValueOrDefault();

                    List<USR_TMMA_WORKCENTER_ROUTING_FILE> list = filter.Pagination.IsPaging ? lists.Skip(filter.Pagination.Skip).Take(filter.Pagination.Take).ToList() : lists.ToList();

                    List<WorkCenterRoutingFileModel> mList = Mapping(list);

                    response = new ResponseModel()
                    {
                        Source = SOURCE,
                        Action = ACTION,
                        Status = true,
                        Message = "Success"
                    };

                    return mList;
                }
            }
            catch (Exception ex)
            {
                totalRecord = 0;

                response = new ResponseModel()
                {
                    Source = SOURCE,
                    Action = ACTION,
                    Status = false,
                    Message = ex.Message
                };

                return null;
            }
        }

        public static ResponseModel AddWorkCenterRoutingFile(WorkCenterRoutingFileModel m, ref int _newID)
        {
            string action = "AddWorkCenterRoutingFile(WorkCenterRoutingFileModel, out _newID)";
            _newID = 0;
            try
            {
                if (m != null)
                {
                    using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                    {
                        USR_TMMA_WORKCENTER_ROUTING_FILE _obj = Mapping(m);

                        context.USR_TMMA_WORKCENTER_ROUTING_FILE.Add(_obj);

                        if (context.SaveChanges() > 0)
                        {
                            _newID = _obj.WorkCenterRoutingFileID;

                            return new ResponseModel()
                            {
                                Source = SOURCE,
                                Action = action,
                                Status = true,
                                Message = "Success"
                            };
                        }

                        return new ResponseModel()
                        {
                            Source = SOURCE,
                            Action = action,
                            Status = false,
                            Message = "Fail"
                        };
                    }
                }

                return new ResponseModel()
                {
                    Source = SOURCE,
                    Action = action,
                    Status = false,
                    Message = "Null"
                };
            }
            catch (DbEntityValidationException ex)
            {
                return new ResponseModel()
                {
                    Source = SOURCE,
                    Action = action,
                    Status = false,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel()
                {
                    Source = SOURCE,
                    Action = action,
                    Status = false,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                };
            }
        }

        public static WorkCenterRoutingFileModel GetWorkCenterRoutingFileLastVersion()
        {
            ACTION = "GetWorkCenterRoutingFileLastVersion()";
            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    List<USR_TMMA_WORKCENTER_ROUTING_FILE> list = context.USR_TMMA_WORKCENTER_ROUTING_FILE.ToList();
                    var o = list.Count > 0 ? list.Last() : null;
                    WorkCenterRoutingFileModel m = Mapping(o);

                    return m;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ResponseModel UpdateStatusWorkCenterRoutingFile(int WorkCenterRoutingFileID, int WorkCenterRoutingFileStatus)
        {
            string action = "UpdateStatusWorkCenterRoutingFile(WorkCenterRoutingFileID, WorkCenterRoutingFileStatus)";
            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    USR_TMMA_WORKCENTER_ROUTING_FILE _obj = context.USR_TMMA_WORKCENTER_ROUTING_FILE.FirstOrDefault(o => o.WorkCenterRoutingFileID == WorkCenterRoutingFileID);


                    if (_obj != null)
                    {
                        _obj.WorkCenterRoutingFileStatus = WorkCenterRoutingFileStatus;
                        context.SaveChanges();
                        return new ResponseModel()
                        {
                            Source = SOURCE,
                            Action = action,
                            Status = true,
                            Message = "Success"
                        };
                    }
                    return new ResponseModel()
                    {
                        Source = SOURCE,
                        Action = action,
                        Status = false,
                        Message = "WorkCenterFileID Not Exist"
                    };
                }

            }
            catch (DbEntityValidationException ex)
            {
                return new ResponseModel()
                {
                    Source = SOURCE,
                    Action = action,
                    Status = false,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel()
                {
                    Source = SOURCE,
                    Action = action,
                    Status = false,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                };
            }
        }
    }
}