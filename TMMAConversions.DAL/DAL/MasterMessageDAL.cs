using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using TMMAConversions.DAL.Entities;
using TMMAConversions.DAL.Models;

namespace TMMAConversions.DAL.DAL
{
    public class MasterMessageDAL
    {
        private static string SOURCE = "MasterMessageDAL";
        private static string ACTION = "";

        internal static BOMFileModel Mapping(USR_TMMA_BOM_FILE o)
        {
            try
            {
                if (o != null)
                {
                    return new BOMFileModel()
                    {
                        BOMFileID = o.BOMFileID,
                        RecObjectName = o.RecObjectName,
                        UserSAP = o.UserSAP,
                        BOMFilePath = o.BOMFilePath,
                        BOMFileVersion = o.BOMFileVersion,
                        BOMFileStatus = o.BOMFileStatus,
                        FileStatus = FileStatusDAL.GetFileStatus(o.BOMFileStatus),
                        ProductsTypeID = o.ProductsTypeID,
                        ValidDate = o.ValidDate,
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
        internal static USR_TMMA_BOM_FILE Mapping(BOMFileModel o)
        {
            try
            {
                if (o != null)
                {
                    return new USR_TMMA_BOM_FILE()
                    {
                        BOMFileID = o.BOMFileID,
                        RecObjectName = o.RecObjectName,
                        UserSAP = o.UserSAP,
                        BOMFilePath = o.BOMFilePath,
                        BOMFileVersion = o.BOMFileVersion,
                        BOMFileStatus = o.BOMFileStatus,
                        ProductsTypeID = o.ProductsTypeID,
                        ValidDate = o.ValidDate,
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
        internal static List<BOMFileModel> Mapping(List<USR_TMMA_BOM_FILE> list)
        {
            ACTION = "Mapping(List<USR_TMMA_BOM_FILE>)";

            try
            {
                if (list != null && list.Count > 0)
                {
                    List<BOMFileModel> mList = new List<BOMFileModel>();

                    foreach (USR_TMMA_BOM_FILE o in list)
                    {
                        mList.Add(new BOMFileModel()
                        {
                            BOMFileID = o.BOMFileID,
                            RecObjectName = o.RecObjectName,
                            UserSAP = o.UserSAP,
                            BOMFilePath = o.BOMFilePath,
                            BOMFileVersion = o.BOMFileVersion,
                            BOMFileStatus = o.BOMFileStatus,
                            FileStatus = FileStatusDAL.GetFileStatus(o.BOMFileStatus),
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

        public static BOMFileModel GetBOMFile(int bomFileID)
        {
            ACTION = "GetBOMFile(bomFileID)";

            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    USR_TMMA_BOM_FILE obj = context.USR_TMMA_BOM_FILE.Where(o => o.BOMFileID == bomFileID).FirstOrDefault();
                    BOMFileModel m = Mapping(obj);
                    return m;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<BOMFileModel> GetBOMFileList(BOMFileFilterModel filter, ref int totalRecord, ref ResponseModel response, ref decimal lastVersion)
        {
            ACTION = "GetBOMFileList(BOMFileFilterModel)";
            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    filter.BOMFileIDs = filter.BOMFileIDs != null ? filter.BOMFileIDs : new int[] { };
                    var IQuery = context.USR_TMMA_BOM_FILE
                    .Where(o =>
                        (!string.IsNullOrEmpty(filter.Keywords) ?
                            (o.RecObjectName.Contains(filter.Keywords)) ||
                            (o.CreatedBy.Contains(filter.Keywords))
                        : true)
                        && (filter.BOMFileID.HasValue ? o.BOMFileID == filter.BOMFileID.Value : true)
                        && (filter.BOMFileIDs.Count() > 0 ? filter.BOMFileIDs.Contains(o.BOMFileID) : true)
                        && (!string.IsNullOrEmpty(filter.RecObjectName) ? filter.CreatedBy.Contains(o.RecObjectName) : true)
                        && (filter.ProductsTypeID.HasValue ? o.ProductsTypeID == filter.ProductsTypeID.Value : true)
                        && (filter.IsActive.HasValue ? o.IsActive == 1 : true)
                    );

                    totalRecord = IQuery.Count();

                    var lists = filter.Sort == "asc"
                        ? filter.Order == "RecObjectName" ? IQuery.OrderBy(o => o.RecObjectName)
                        : filter.Order == "UserSAP" ? IQuery.OrderBy(o => o.UserSAP)
                        : filter.Order == "Version" ? IQuery.OrderBy(o => o.BOMFileVersion)
                        : filter.Order == "Status" ? IQuery.OrderBy(o => o.BOMFileStatus)
                        : filter.Order == "ValidDate" ? IQuery.OrderBy(o => o.ValidDate)
                        : filter.Order == "CreatedBy" ? IQuery.OrderBy(o => o.CreatedBy)
                        : filter.Order == "CreatedDate" ? IQuery.OrderBy(o => o.CreatedDate)
                        : IQuery.OrderBy(o => o.CreatedDate)
                        : filter.Order == "RecObjectName" ? IQuery.OrderByDescending(o => o.RecObjectName)
                        : filter.Order == "UserSAP" ? IQuery.OrderByDescending(o => o.UserSAP)
                        : filter.Order == "Version" ? IQuery.OrderByDescending(o => o.BOMFileVersion)
                        : filter.Order == "Status" ? IQuery.OrderByDescending(o => o.BOMFileStatus)
                        : filter.Order == "ValidDate" ? IQuery.OrderByDescending(o => o.ValidDate)
                        : filter.Order == "CreatedBy" ? IQuery.OrderByDescending(o => o.CreatedBy)
                        : filter.Order == "CreatedDate" ? IQuery.OrderByDescending(o => o.CreatedDate)
                        : IQuery.OrderByDescending(o => o.CreatedDate);

                    lastVersion = IQuery.OrderByDescending(o => o.BOMFileVersion).FirstOrDefault().BOMFileVersion.GetValueOrDefault();

                    List<USR_TMMA_BOM_FILE> list = filter.Pagination.IsPaging ? lists.Skip(filter.Pagination.Skip).Take(filter.Pagination.Take).ToList() : lists.ToList();

                    List<BOMFileModel> mList = Mapping(list);

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

        public static ResponseModel AddBOMFile(BOMFileModel m, ref int _newID)
        {
            string action = "AddBOMFile(BOMFileModel, out _newID)";
            _newID = 0;
            try
            {
                if (m != null)
                {
                    using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                    {
                        USR_TMMA_BOM_FILE _obj = Mapping(m);

                        context.USR_TMMA_BOM_FILE.Add(_obj);

                        if (context.SaveChanges() > 0)
                        {
                            _newID = _obj.BOMFileID;

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

        public static ResponseModel UpdateStatusBOMFile(int BOMFileID, int BOMFileStatus)
        {
            string action = "UpdateStatusBOMFile(BOMFileModel)";
            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    USR_TMMA_BOM_FILE _obj = context.USR_TMMA_BOM_FILE.FirstOrDefault(o => o.BOMFileID == BOMFileID);


                    if (_obj != null)
                    {
                        _obj.BOMFileStatus = BOMFileStatus;
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

        public static BOMFileModel GetBOMFileLastVersion(int ProductsTypeID)
        {
            ACTION = "GetBOMFileLastVersion()";
            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    var list = context.USR_TMMA_BOM_FILE.Where(a => a.ProductsTypeID == ProductsTypeID).ToList();
                    var o = list.Last();
                    BOMFileModel m = Mapping(o);

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