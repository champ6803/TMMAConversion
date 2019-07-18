using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using TMMAConversions.DAL.Entities;
using TMMAConversions.DAL.Models;

namespace TMMAConversions.DAL.DAL
{
    public class WorkCenterFileDAL
    {
        private static string SOURCE = "WorkCenterFileDAL";
        private static string ACTION = "";

        internal static WorkCenterFileModel Mapping(USR_TMMA_WORK_CENTER_FILE o)
        {
            try
            {
                if (o != null)
                {
                    return new WorkCenterFileModel()
                    {
                        WorkCenterFileID = o.WorkCenterFileID,
                        RecObjectName = o.RecObjectName,
                        UserSAP = o.UserSAP,
                        WorkCenterFilePath = o.WorkCenterFilePath,
                        WorkCenterFileVersion = o.WorkCenterFileVersion,
                        WorkCenterFileStatus = o.WorkCenterFileStatus,
                        FileStatus = FileStatusDAL.GetFileStatus(o.WorkCenterFileStatus),
                        ProductsTypeID = o.ProductsTypeID,
                        IsActive = o.IsActive == 1,
                        CreatedBy = o.CreatedBy,
                        CreatedDate = o.CreatedDate,
                        UpdatedBy = o.UpdatedBy,
                        UpdatedDate = o.UpdatedDate
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static USR_TMMA_WORK_CENTER_FILE Mapping(WorkCenterFileModel o)
        {
            try
            {
                if (o != null)
                {
                    return new USR_TMMA_WORK_CENTER_FILE()
                    {
                        WorkCenterFileID = o.WorkCenterFileID,
                        RecObjectName = o.RecObjectName,
                        UserSAP = o.UserSAP,
                        WorkCenterFilePath = o.WorkCenterFilePath,
                        WorkCenterFileVersion = o.WorkCenterFileVersion,
                        WorkCenterFileStatus = o.WorkCenterFileStatus,
                        ProductsTypeID = o.ProductsTypeID,
                        IsActive = o.IsActive ? 1 : 0,
                        CreatedBy = o.CreatedBy,
                        CreatedDate = o.CreatedDate,
                        UpdatedBy = o.UpdatedBy,
                        UpdatedDate = o.UpdatedDate
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static List<WorkCenterFileModel> Mapping(List<USR_TMMA_WORK_CENTER_FILE> list)
        {
            ACTION = "Mapping(List<USR_TMMA_WORK_CENTER_FILE>)";

            try
            {
                if (list != null && list.Count > 0)
                {
                    List<WorkCenterFileModel> mList = new List<WorkCenterFileModel>();

                    foreach (USR_TMMA_WORK_CENTER_FILE o in list)
                    {
                        mList.Add(new WorkCenterFileModel()
                        {
                            WorkCenterFileID = o.WorkCenterFileID,
                            RecObjectName = o.RecObjectName,
                            UserSAP =  o.UserSAP,
                            WorkCenterFilePath = o.WorkCenterFilePath,
                            WorkCenterFileVersion = o.WorkCenterFileVersion,
                            WorkCenterFileStatus = o.WorkCenterFileStatus,
                            FileStatus = FileStatusDAL.GetFileStatus(o.WorkCenterFileStatus),
                            ProductsTypeID = o.ProductsTypeID,
                            IsActive = o.IsActive == 1,
                            CreatedBy = o.CreatedBy,
                            CreatedDate = o.CreatedDate,
                            UpdatedBy = o.UpdatedBy,
                            UpdatedDate = o.UpdatedDate
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

        public static WorkCenterFileModel GetWorkCenterFile(int workCenterFileID)
        {
            ACTION = "GetWorkCenterFile(workCenterFileID)";

            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    USR_TMMA_WORK_CENTER_FILE obj = context.USR_TMMA_WORK_CENTER_FILE.Where(o => o.WorkCenterFileID == workCenterFileID).FirstOrDefault();
                    WorkCenterFileModel m = Mapping(obj);
                    return m;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<WorkCenterFileModel> GetWorkCenterFileList(WorkCenterFileFilterModel filter, ref int totalRecord, ref ResponseModel response, ref decimal lastVersion)
        {
            ACTION = "GetWorkCenterFileList(WorkCenterFileFilterModel)";
            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    filter.WorkCenterFileIDs = filter.WorkCenterFileIDs != null ? filter.WorkCenterFileIDs : new int[] { };
                    var IQuery = context.USR_TMMA_WORK_CENTER_FILE
                    .Where(o =>
                        (!string.IsNullOrEmpty(filter.Keywords) ?
                            (o.RecObjectName.Contains(filter.Keywords)) ||
                            (o.CreatedBy.Contains(filter.Keywords))
                        : true)
                        && (filter.WorkCenterFileID.HasValue ? o.WorkCenterFileID == filter.WorkCenterFileID.Value : true)
                        && (filter.WorkCenterFileIDs.Count() > 0 ? filter.WorkCenterFileIDs.Contains(o.WorkCenterFileID) : true)
                        && (!string.IsNullOrEmpty(filter.RecObjectName) ? filter.CreatedBy.Contains(o.RecObjectName) : true)
                        && (filter.ProductsTypeID.HasValue ? o.ProductsTypeID == filter.ProductsTypeID.Value : true)
                        && (filter.IsActive.HasValue ? o.IsActive == 1 : true)
                    );

                    totalRecord = IQuery.Count();

                    var lists = filter.Sort == "asc"
                        ? filter.Order == "RecObjectName" ? IQuery.OrderBy(o => o.RecObjectName)
                        : filter.Order == "UserSAP" ? IQuery.OrderBy(o => o.UserSAP)
                        : filter.Order == "Version" ? IQuery.OrderBy(o => o.WorkCenterFileVersion)
                        : filter.Order == "Status" ? IQuery.OrderBy(o => o.WorkCenterFileStatus)
                        : filter.Order == "CreatedBy" ? IQuery.OrderBy(o => o.CreatedBy)
                        : filter.Order == "CreatedDate" ? IQuery.OrderBy(o => o.CreatedDate)
                        : IQuery.OrderBy(o => o.CreatedDate)
                        : filter.Order == "RecObjectName" ? IQuery.OrderByDescending(o => o.RecObjectName)
                        : filter.Order == "UserSAP" ? IQuery.OrderByDescending(o => o.UserSAP)
                        : filter.Order == "Version" ? IQuery.OrderByDescending(o => o.WorkCenterFileVersion)
                        : filter.Order == "Status" ? IQuery.OrderByDescending(o => o.WorkCenterFileStatus)
                        : filter.Order == "CreatedBy" ? IQuery.OrderByDescending(o => o.CreatedBy)
                        : filter.Order == "CreatedDate" ? IQuery.OrderByDescending(o => o.CreatedDate)
                        : IQuery.OrderByDescending(o => o.CreatedDate);

                    lastVersion = IQuery.OrderByDescending(o => o.WorkCenterFileVersion).FirstOrDefault().WorkCenterFileVersion.GetValueOrDefault();

                    List<USR_TMMA_WORK_CENTER_FILE> list = filter.Pagination.IsPaging ? lists.Skip(filter.Pagination.Skip).Take(filter.Pagination.Take).ToList() : lists.ToList();

                    List<WorkCenterFileModel> mList = Mapping(list);

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

        public static ResponseModel AddWorkCenterFile(WorkCenterFileModel m, ref int _newID)
        {
            string action = "AddWorkCenterFile(WorkCenterFileModel, out _newID)";
            _newID = 0;
            try
            {
                if (m != null)
                {
                    using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                    {
                        USR_TMMA_WORK_CENTER_FILE _obj = Mapping(m);

                        context.USR_TMMA_WORK_CENTER_FILE.Add(_obj);

                        if (context.SaveChanges() > 0)
                        {
                            _newID = _obj.WorkCenterFileID;

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

        public static WorkCenterFileModel GetWorkCenterFileLastVersion()
        {
            ACTION = "GetWorkCenteFileLastVersion()";
            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    var list = context.USR_TMMA_WORK_CENTER_FILE.ToList();
                    var o = list.Last();
                    WorkCenterFileModel m = Mapping(o);

                    return m;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ResponseModel UpdateStatusWorkCenterFile(int WorkCenterFileID, int WorkCenterFileStatus)
        {
            string action = "UpdateStatusWorkCenterFile(WorkCenterFileID, WorkCenterFileStatus)";
            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    USR_TMMA_WORK_CENTER_FILE _obj = context.USR_TMMA_WORK_CENTER_FILE.FirstOrDefault(o => o.WorkCenterFileID == WorkCenterFileID);


                    if (_obj != null)
                    {
                        _obj.WorkCenterFileStatus = WorkCenterFileStatus;
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