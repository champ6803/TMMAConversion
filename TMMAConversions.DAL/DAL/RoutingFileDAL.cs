using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using TMMAConversions.DAL.Entities;
using TMMAConversions.DAL.Models;

namespace TMMAConversions.DAL.DAL
{
    public class RoutingFileDAL
    {
        static string SOURCE = "RoutingFileDAL";
        static string ACTION = "";

        internal static RoutingFileModel Mapping(USR_TMMA_ROUTING_FILE o)
        {
            try
            {
                if (o != null)
                {
                    return new RoutingFileModel()
                    {
                        RoutingFileID = o.RoutingFileID,
                        RecObjectName = o.RecObjectName,
                        UserSAP = o.UserSAP,
                        RoutingFilePath = o.RoutingFilePath,
                        RoutingFileVersion = o.RoutingFileVersion,
                        RoutingFileStatus = o.RoutingFileStatus,
                        ProductsTypeID = o.ProductsTypeID,
                        FileStatus = FileStatusDAL.GetFileStatus(o.RoutingFileStatus),
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

        internal static USR_TMMA_ROUTING_FILE Mapping(RoutingFileModel o)
        {
            try
            {
                if (o != null)
                {
                    return new USR_TMMA_ROUTING_FILE()
                    {
                        RoutingFileID = o.RoutingFileID,
                        RecObjectName = o.RecObjectName,
                        UserSAP = o.UserSAP,
                        RoutingFilePath = o.RoutingFilePath,
                        RoutingFileVersion = o.RoutingFileVersion,
                        RoutingFileStatus = o.RoutingFileStatus,
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

        internal static List<RoutingFileModel> Mapping(List<USR_TMMA_ROUTING_FILE> list)
        {
            ACTION = "Mapping(List<USR_TMMA_ROUTING_FILE>)";

            try
            {
                if (list != null && list.Count > 0)
                {
                    List<RoutingFileModel> mList = new List<RoutingFileModel>();

                    foreach (USR_TMMA_ROUTING_FILE o in list)
                    {
                        mList.Add(new RoutingFileModel()
                        {
                            RoutingFileID = o.RoutingFileID,
                            RecObjectName = o.RecObjectName,
                            UserSAP = o.UserSAP,
                            RoutingFilePath = o.RoutingFilePath,
                            RoutingFileVersion = o.RoutingFileVersion,
                            RoutingFileStatus = o.RoutingFileStatus,
                            FileStatus = FileStatusDAL.GetFileStatus(o.RoutingFileStatus),
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

        public static RoutingFileModel GetRoutingFile(int routingFileID)
        {
            ACTION = "GetRoutingFile(routingFileID)";

            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    USR_TMMA_ROUTING_FILE obj = context.USR_TMMA_ROUTING_FILE.Where(o => o.RoutingFileID == routingFileID).FirstOrDefault();
                    RoutingFileModel m = Mapping(obj);
                    return m;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<RoutingFileModel> GetRoutingFileList(RoutingFileFilterModel filter, ref int totalRecord, ref ResponseModel response, ref decimal lastVersion)
        {
            ACTION = "GetRoutingFileList(RoutingFileFilterModel)";
            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    filter.RoutingFileIDs = filter.RoutingFileIDs != null ? filter.RoutingFileIDs : new int[] { };
                    var IQuery = context.USR_TMMA_ROUTING_FILE
                    .Where(o =>
                        (!string.IsNullOrEmpty(filter.Keywords) ?
                            (o.RecObjectName.Contains(filter.Keywords)) ||
                            (o.CreatedBy.Contains(filter.Keywords))
                        : true)
                        && (filter.RoutingFileID.HasValue ? o.RoutingFileID == filter.RoutingFileID.Value : true)
                        && (filter.RoutingFileIDs.Count() > 0 ? filter.RoutingFileIDs.Contains(o.RoutingFileID) : true)
                        && (!string.IsNullOrEmpty(filter.RecObjectName) ? filter.RecObjectName.Contains(o.RecObjectName) : true)
                        && (filter.ProductsTypeID.HasValue ? o.ProductsTypeID == filter.ProductsTypeID.Value : true)
                        && (filter.IsActive.HasValue ? o.IsActive == 1 : true)
                    );

                    totalRecord = IQuery.Count();

                    var lists = filter.Sort == "asc"
                        ? filter.Order == "RecObjectName" ? IQuery.OrderBy(o => o.RecObjectName)
                        : filter.Order == "UserSAP" ? IQuery.OrderBy(o => o.UserSAP)
                        : filter.Order == "Version" ? IQuery.OrderBy(o => o.RoutingFileVersion)
                        : filter.Order == "Status" ? IQuery.OrderBy(o => o.RoutingFileStatus)
                        : filter.Order == "ValidDate" ? IQuery.OrderBy(o => o.ValidDate)
                        : filter.Order == "CreatedBy" ? IQuery.OrderBy(o => o.CreatedBy)
                        : filter.Order == "CreatedDate" ? IQuery.OrderBy(o => o.CreatedDate)
                        : IQuery.OrderBy(o => o.CreatedDate)
                        : filter.Order == "RecObjectName" ? IQuery.OrderByDescending(o => o.RecObjectName)
                        : filter.Order == "UserSAP" ? IQuery.OrderByDescending(o => o.UserSAP)
                        : filter.Order == "Version" ? IQuery.OrderByDescending(o => o.RoutingFileVersion)
                        : filter.Order == "Status" ? IQuery.OrderByDescending(o => o.RoutingFileStatus)
                        : filter.Order == "ValidDate" ? IQuery.OrderByDescending(o => o.ValidDate)
                        : filter.Order == "CreatedBy" ? IQuery.OrderByDescending(o => o.CreatedBy)
                        : filter.Order == "CreatedDate" ? IQuery.OrderByDescending(o => o.CreatedDate)
                        : IQuery.OrderByDescending(o => o.CreatedDate);

                    lastVersion = IQuery.OrderByDescending(o => o.RoutingFileVersion).FirstOrDefault().RoutingFileVersion.GetValueOrDefault();

                    List<USR_TMMA_ROUTING_FILE> list = filter.Pagination.IsPaging ? lists.Skip(filter.Pagination.Skip).Take(filter.Pagination.Take).ToList() : lists.ToList();

                    List<RoutingFileModel> mList = Mapping(list);

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

        public static ResponseModel AddRoutingFile(RoutingFileModel m, ref int _newID)
        {
            ACTION = "AddRoutingFile(RoutingFileModel, out _newID)";
            _newID = 0;
            try
            {
                if (m != null)
                {
                    using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                    {
                        USR_TMMA_ROUTING_FILE _obj = Mapping(m);

                        context.USR_TMMA_ROUTING_FILE.Add(_obj);

                        if (context.SaveChanges() > 0)
                        {
                            _newID = _obj.RoutingFileID;

                            return new ResponseModel()
                            {
                                Source = SOURCE,
                                Action = ACTION,
                                Status = true,
                                Message = "Success"
                            };
                        }

                        return new ResponseModel()
                        {
                            Source = SOURCE,
                            Action = ACTION,
                            Status = false,
                            Message = "Fail"
                        };
                    }
                }

                return new ResponseModel()
                {
                    Source = SOURCE,
                    Action = ACTION,
                    Status = false,
                    Message = "Null"
                };
            }
            catch (DbEntityValidationException ex)
            {
                return new ResponseModel()
                {
                    Source = SOURCE,
                    Action = ACTION,
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
                    Action = ACTION,
                    Status = false,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                };
            }
        }

        public static ResponseModel UpdateStatusRoutingFile(int RoutingFileID, int RoutingFileStatus)
        {
            string action = "UpdateStatusRoutingFile(RoutingFileID, RoutingFileStatus)";
            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    USR_TMMA_ROUTING_FILE _obj = context.USR_TMMA_ROUTING_FILE.FirstOrDefault(o => o.RoutingFileID == RoutingFileID);


                    if (_obj != null)
                    {
                        _obj.RoutingFileStatus = RoutingFileStatus;
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
                        Message = "BOMFileID Not Exist"
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

        public static RoutingFileModel GetRoutingFileLastVersion(int ProductsTypeID)
        {
            ACTION = "GetRoutingFileLastVersion(ProductsTypeID)";
            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    var list = context.USR_TMMA_ROUTING_FILE.Where(a => a.ProductsTypeID == ProductsTypeID).ToList();
                    var o = list.Last();
                    RoutingFileModel m = Mapping(o);

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