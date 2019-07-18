using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using TMMAConversions.DAL.Entities;
using TMMAConversions.DAL.Models;

namespace TMMAConversions.DAL.DAL
{
    public class InspectionPlanFileDAL
    {
        private static string SOURCE = "InspectionPlanFileDAL";
        private static string ACTION = "";

        internal static InspectionPlanFileModel Mapping(USR_TMMA_INSPECTION_PLAN_FILE o)
        {
            try
            {
                if (o != null)
                {
                    return new InspectionPlanFileModel()
                    {
                        InspectionPlanFileID = o.InspectionPlanFileID,
                        RecObjectName = o.RecObjectName,
                        UserSAP = o.UserSAP,
                        InspectionPlanFilePath = o.InspectionPlanFilePath,
                        InspectionPlanFileVersion = o.InspectionPlanFileVersion,
                        InspectionPlanFileStatus = o.InspectionPlanFileStatus,
                        FileStatus = FileStatusDAL.GetFileStatus(o.InspectionPlanFileStatus),
                        ProductsTypeID = o.ProductsTypeID,
                        ValidDate =  o.ValidDate,
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
        internal static USR_TMMA_INSPECTION_PLAN_FILE Mapping(InspectionPlanFileModel o)
        {
            try
            {
                if (o != null)
                {
                    return new USR_TMMA_INSPECTION_PLAN_FILE()
                    {
                        InspectionPlanFileID = o.InspectionPlanFileID,
                        RecObjectName = o.RecObjectName,
                        UserSAP = o.UserSAP,
                        InspectionPlanFilePath = o.InspectionPlanFilePath,
                        InspectionPlanFileVersion = o.InspectionPlanFileVersion,
                        InspectionPlanFileStatus = o.InspectionPlanFileStatus,
                        ProductsTypeID = o.ProductsTypeID,
                        ValidDate =  o.ValidDate,
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
        internal static List<InspectionPlanFileModel> Mapping(List<USR_TMMA_INSPECTION_PLAN_FILE> list)
        {
            ACTION = "Mapping(List<USR_TMMA_INSPECTION_PLAN_FILE>)";

            try
            {
                if (list != null && list.Count > 0)
                {
                    List<InspectionPlanFileModel> mList = new List<InspectionPlanFileModel>();

                    foreach (USR_TMMA_INSPECTION_PLAN_FILE o in list)
                    {
                        mList.Add(new InspectionPlanFileModel()
                        {
                            InspectionPlanFileID = o.InspectionPlanFileID,
                            RecObjectName = o.RecObjectName,
                            UserSAP = o.UserSAP,
                            InspectionPlanFilePath = o.InspectionPlanFilePath,
                            InspectionPlanFileVersion = o.InspectionPlanFileVersion,
                            InspectionPlanFileStatus = o.InspectionPlanFileStatus,
                            FileStatus = FileStatusDAL.GetFileStatus(o.InspectionPlanFileStatus),
                            ProductsTypeID = o.ProductsTypeID,
                            ValidDate = o.ValidDate,
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

        public static InspectionPlanFileModel GetInspectionPlanFile(int inspectionPlanFileID)
        {
            ACTION = "GetInspectionPlanFile(inspectionPlanFileID)";

            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    USR_TMMA_INSPECTION_PLAN_FILE obj = context.USR_TMMA_INSPECTION_PLAN_FILE.Where(o => o.InspectionPlanFileID == inspectionPlanFileID).FirstOrDefault();
                    InspectionPlanFileModel m = Mapping(obj);
                    return m;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<InspectionPlanFileModel> GetInspectionPlanFileList(InspectionPlanFileFilterModel filter, ref int totalRecord, ref ResponseModel response, ref decimal lastVersion)
        {
            ACTION = "GetInspectionPlanFileList(InspectionPlanFileFilterModel)";
            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    filter.InspectionPlanFileIDs = filter.InspectionPlanFileIDs != null ? filter.InspectionPlanFileIDs : new int[] { };
                    var IQuery = context.USR_TMMA_INSPECTION_PLAN_FILE
                    .Where(o =>
                        (!string.IsNullOrEmpty(filter.Keywords) ?
                            (o.RecObjectName.Contains(filter.Keywords)) ||
                            (o.CreatedBy.Contains(filter.Keywords))
                        : true)
                        && (filter.InspectionPlanFileID.HasValue ? o.InspectionPlanFileID == filter.InspectionPlanFileID.Value : true)
                        && (filter.InspectionPlanFileIDs.Count() > 0 ? filter.InspectionPlanFileIDs.Contains(o.InspectionPlanFileID) : true)
                        && (!string.IsNullOrEmpty(filter.RecObjectName) ? filter.CreatedBy.Contains(o.RecObjectName) : true)
                        && (filter.ProductsTypeID.HasValue ? o.ProductsTypeID == filter.ProductsTypeID.Value : true)
                        && (filter.IsActive.HasValue ? o.IsActive == 1 : true)
                    );

                    totalRecord = IQuery.Count();

                    var lists = filter.Sort == "asc"
                        ? filter.Order == "RecObjectName" ? IQuery.OrderBy(o => o.RecObjectName)
                        : filter.Order == "UserSAP" ? IQuery.OrderBy(o => o.UserSAP)
                        : filter.Order == "Version" ? IQuery.OrderBy(o => o.InspectionPlanFileVersion)
                        : filter.Order == "Status" ? IQuery.OrderBy(o => o.InspectionPlanFileStatus)
                        : filter.Order == "ValidDate" ? IQuery.OrderBy(o => o.ValidDate)
                        : filter.Order == "CreatedBy" ? IQuery.OrderBy(o => o.CreatedBy)
                        : filter.Order == "CreatedDate" ? IQuery.OrderBy(o => o.CreatedDate)
                        : IQuery.OrderBy(o => o.CreatedDate)
                        : filter.Order == "RecObjectName" ? IQuery.OrderByDescending(o => o.RecObjectName)
                        : filter.Order == "UserSAP" ? IQuery.OrderByDescending(o => o.UserSAP)
                        : filter.Order == "Version" ? IQuery.OrderByDescending(o => o.InspectionPlanFileVersion)
                        : filter.Order == "Status" ? IQuery.OrderByDescending(o => o.InspectionPlanFileStatus)
                        : filter.Order == "ValidDate" ? IQuery.OrderByDescending(o => o.ValidDate)
                        : filter.Order == "CreatedBy" ? IQuery.OrderByDescending(o => o.CreatedBy)
                        : filter.Order == "CreatedDate" ? IQuery.OrderByDescending(o => o.CreatedDate)
                        : IQuery.OrderByDescending(o => o.CreatedDate);

                    lastVersion = IQuery.OrderByDescending(o => o.InspectionPlanFileVersion).FirstOrDefault().InspectionPlanFileVersion.GetValueOrDefault();

                    List<USR_TMMA_INSPECTION_PLAN_FILE> list = filter.Pagination.IsPaging ? lists.Skip(filter.Pagination.Skip).Take(filter.Pagination.Take).ToList() : lists.ToList();

                    List<InspectionPlanFileModel> mList = Mapping(list);

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

        public static ResponseModel AddInspectionPlanFile(InspectionPlanFileModel m, ref int _newID)
        {
            string action = "AddInspectionPlanFile(InspectionPlanFileModel, out _newID)";
            _newID = 0;
            try
            {
                if (m != null)
                {
                    using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                    {
                        USR_TMMA_INSPECTION_PLAN_FILE _obj = Mapping(m);

                        context.USR_TMMA_INSPECTION_PLAN_FILE.Add(_obj);

                        if (context.SaveChanges() > 0)
                        {
                            _newID = _obj.InspectionPlanFileID;

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

        public static ResponseModel UpdateStatusInspectionPlanFile(int InspectionPlanFileID, int InspectionPlanFileStatus)
        {
            string action = "UpdateStatusInspectionPlanFile(InspectionPlanFileID, InspectionPlanFileStatus)";
            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    USR_TMMA_INSPECTION_PLAN_FILE _obj = context.USR_TMMA_INSPECTION_PLAN_FILE.FirstOrDefault(o => o.InspectionPlanFileID == InspectionPlanFileID);

                    if (_obj != null)
                    {
                        _obj.InspectionPlanFileStatus = InspectionPlanFileStatus;
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
                        Message = "InspectionPlanFileStatus Not Exist"
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

        public static InspectionPlanFileModel GetInspectionPlanFileLastVersion(int ProductsTypeID)
        {
            ACTION = "GetInspectionPlanFileLastVersion(ProductsTypeID)";
            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    var list = context.USR_TMMA_INSPECTION_PLAN_FILE.Where(a => a.ProductsTypeID == ProductsTypeID).ToList();
                    var o = list.Last();
                    InspectionPlanFileModel m = Mapping(o);

                    return m;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}