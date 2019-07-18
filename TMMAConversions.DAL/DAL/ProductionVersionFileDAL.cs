using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using TMMAConversions.DAL.Entities;
using TMMAConversions.DAL.Models;

namespace TMMAConversions.DAL.DAL
{
    public class ProductionVersionFileDAL
    {
        private static string SOURCE = "ProductionVersionFileDAL";
        private static string ACTION = "";

        internal static ProductionVersionFileModel Mapping(USR_TMMA_PRODUCTION_VERSION_FILE o)
        {
            try
            {
                if (o != null)
                {
                    return new ProductionVersionFileModel()
                    {
                        ProductionVersionFileID = o.ProductionVersionFileID,
                        RecObjectName = o.RecObjectName,
                        UserSAP = o.UserSAP,
                        ProductionVersionFilePath = o.ProductionVersionFilePath,
                        ProductionVersionFileVersion = o.ProductionVersionFileVersion,
                        ProductionVersionFileStatus = o.ProductionVersionFileStatus,
                        FileStatus = FileStatusDAL.GetFileStatus(o.ProductionVersionFileStatus),
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

        internal static USR_TMMA_PRODUCTION_VERSION_FILE Mapping(ProductionVersionFileModel o)
        {
            try
            {
                if (o != null)
                {
                    return new USR_TMMA_PRODUCTION_VERSION_FILE()
                    {
                        ProductionVersionFileID = o.ProductionVersionFileID,
                        RecObjectName = o.RecObjectName,
                        UserSAP = o.UserSAP,
                        ProductionVersionFilePath = o.ProductionVersionFilePath,
                        ProductionVersionFileVersion = o.ProductionVersionFileVersion,
                        ProductionVersionFileStatus = o.ProductionVersionFileStatus,
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

        internal static List<ProductionVersionFileModel> Mapping(List<USR_TMMA_PRODUCTION_VERSION_FILE> list)
        {
            ACTION = "Mapping(List<ProductionVersionFileModel>)";

            try
            {
                if (list != null && list.Count > 0)
                {
                    List<ProductionVersionFileModel> mList = new List<ProductionVersionFileModel>();

                    foreach (USR_TMMA_PRODUCTION_VERSION_FILE o in list)
                    {
                        mList.Add(new ProductionVersionFileModel()
                        {
                            ProductionVersionFileID = o.ProductionVersionFileID,
                            RecObjectName = o.RecObjectName,
                            UserSAP = o.UserSAP,
                            ProductionVersionFilePath = o.ProductionVersionFilePath,
                            ProductionVersionFileVersion = o.ProductionVersionFileVersion,
                            ProductionVersionFileStatus = o.ProductionVersionFileStatus,
                            FileStatus = FileStatusDAL.GetFileStatus(o.ProductionVersionFileStatus),
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

        public static ProductionVersionFileModel GetProductionVersionFile(int productionVersionFileID)
        {
            ACTION = "GetProductionVersionFile(productionVersionFileID)";

            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    USR_TMMA_PRODUCTION_VERSION_FILE obj = context.USR_TMMA_PRODUCTION_VERSION_FILE.Where(o => o.ProductionVersionFileID == productionVersionFileID).FirstOrDefault();
                    ProductionVersionFileModel m = Mapping(obj);
                    return m;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<ProductionVersionFileModel> GetProductionVersionFileList(ProductionVersionFileFilterModel filter, ref int totalRecord, ref ResponseModel response, ref decimal lastVersion)
        {
            ACTION = "GetProductionVersionFileList(ProductionVersionFileFilterModel)";
            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    filter.ProductionVersionFileIDs = filter.ProductionVersionFileIDs != null ? filter.ProductionVersionFileIDs : new int[] { };
                    var IQuery = context.USR_TMMA_PRODUCTION_VERSION_FILE
                    .Where(o =>
                        (!string.IsNullOrEmpty(filter.Keywords) ?
                            (o.RecObjectName.Contains(filter.Keywords)) ||
                            (o.CreatedBy.Contains(filter.Keywords))
                        : true)
                        && (filter.ProductionVersionFileID.HasValue ? o.ProductionVersionFileID == filter.ProductionVersionFileID.Value : true)
                        && (filter.ProductionVersionFileIDs.Count() > 0 ? filter.ProductionVersionFileIDs.Contains(o.ProductionVersionFileID) : true)
                        && (!string.IsNullOrEmpty(filter.RecObjectName) ? filter.CreatedBy.Contains(o.RecObjectName) : true)
                        && (filter.ProductsTypeID.HasValue ? o.ProductsTypeID == filter.ProductsTypeID.Value : true)
                        && (filter.IsActive.HasValue ? o.IsActive == 1 : true)
                    );

                    totalRecord = IQuery.Count();

                    var lists = filter.Sort == "asc"
                        ? filter.Order == "RecObjectName" ? IQuery.OrderBy(o => o.RecObjectName)
                        : filter.Order == "UserSAP" ? IQuery.OrderBy(o => o.UserSAP)
                        : filter.Order == "Version" ? IQuery.OrderBy(o => o.ProductionVersionFileVersion)
                        : filter.Order == "Status" ? IQuery.OrderBy(o => o.ProductionVersionFileStatus)
                        : filter.Order == "CreatedBy" ? IQuery.OrderBy(o => o.CreatedBy)
                        : filter.Order == "CreatedDate" ? IQuery.OrderBy(o => o.CreatedDate)
                        : IQuery.OrderBy(o => o.CreatedDate)
                        : filter.Order == "RecObjectName" ? IQuery.OrderByDescending(o => o.RecObjectName)
                        : filter.Order == "UserSAP" ? IQuery.OrderByDescending(o => o.UserSAP)
                        : filter.Order == "Version" ? IQuery.OrderByDescending(o => o.ProductionVersionFileVersion)
                        : filter.Order == "Status" ? IQuery.OrderByDescending(o => o.ProductionVersionFileStatus)
                        : filter.Order == "CreatedBy" ? IQuery.OrderByDescending(o => o.CreatedBy)
                        : filter.Order == "CreatedDate" ? IQuery.OrderByDescending(o => o.CreatedDate)
                        : IQuery.OrderByDescending(o => o.CreatedDate);

                    lastVersion = IQuery.OrderByDescending(o => o.ProductionVersionFileVersion).FirstOrDefault().ProductionVersionFileVersion.GetValueOrDefault();

                    List<USR_TMMA_PRODUCTION_VERSION_FILE> list = filter.Pagination.IsPaging ? IQuery.Skip(filter.Pagination.Skip).Take(filter.Pagination.Take).ToList() : IQuery.ToList();

                    List<ProductionVersionFileModel> mList = Mapping(list);

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

        public static ResponseModel AddProductionVersionFile(ProductionVersionFileModel m, ref int _newID)
        {
            string action = "AddProductionVersionFile(ProductionVersionFileModel, out _newID)";
            _newID = 0;
            try
            {
                if (m != null)
                {
                    using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                    {
                        USR_TMMA_PRODUCTION_VERSION_FILE _obj = Mapping(m);

                        context.USR_TMMA_PRODUCTION_VERSION_FILE.Add(_obj);

                        if (context.SaveChanges() > 0)
                        {
                            _newID = _obj.ProductionVersionFileID;

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

        public static ProductionVersionFileModel GetProductionVersionFileLastVersion()
        {
            ACTION = "GetProductionVersionFileLastVersion()";
            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    var list = context.USR_TMMA_PRODUCTION_VERSION_FILE.ToList();
                    var o = list.Last();
                    ProductionVersionFileModel m = Mapping(o);

                    return m;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ResponseModel UpdateStatusProductionVersionFile(int ProductionVersionFileID, int ProductionVersionFileStatus)
        {
            string action = "UpdateStatusProductionVersionFile(ProductionVersionFileID, ProductionVersionFileStatus)";
            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    USR_TMMA_PRODUCTION_VERSION_FILE _obj = context.USR_TMMA_PRODUCTION_VERSION_FILE.FirstOrDefault(o => o.ProductionVersionFileID == ProductionVersionFileID);


                    if (_obj != null)
                    {
                        _obj.ProductionVersionFileStatus = ProductionVersionFileStatus;
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
                        Message = "ProductionVersionFileID Not Exist"
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