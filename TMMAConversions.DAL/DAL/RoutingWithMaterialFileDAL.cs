using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using TMMAConversions.DAL.Entities;
using TMMAConversions.DAL.Models;

namespace TMMAConversions.DAL.DAL
{
    public class RoutingWithMaterialFileDAL
    {
        private static string SOURCE = "RoutingWithMaterialFileDAL";
        private static string ACTION = "";

        internal static RoutingWithMaterialFileModel Mapping(USR_TMMA_ROUTING_WITH_MATERIAL_FILE o)
        {
            try
            {
                if (o != null)
                {
                    return new RoutingWithMaterialFileModel()
                    {
                        RoutingWithMaterialFileID = o.RoutingWithMaterialFileID,
                        RecObjectName = o.RecObjectName,
                        UserSAP = o.UserSAP,
                        RoutingWithMaterialFilePath = o.RoutingWithMaterialFilePath,
                        RoutingWithMaterialFileVersion = o.RoutingWithMaterialFileVersion,
                        RoutingWithMaterialFileStatus = o.RoutingWithMaterialFileStatus,
                        FileStatus = FileStatusDAL.GetFileStatus(o.RoutingWithMaterialFileStatus),
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
        internal static USR_TMMA_ROUTING_WITH_MATERIAL_FILE Mapping(RoutingWithMaterialFileModel o)
        {
            try
            {
                if (o != null)
                {
                    return new USR_TMMA_ROUTING_WITH_MATERIAL_FILE()
                    {
                        RoutingWithMaterialFileID = o.RoutingWithMaterialFileID,
                        RecObjectName = o.RecObjectName,
                        UserSAP = o.UserSAP,
                        RoutingWithMaterialFilePath = o.RoutingWithMaterialFilePath,
                        RoutingWithMaterialFileVersion = o.RoutingWithMaterialFileVersion,
                        RoutingWithMaterialFileStatus = o.RoutingWithMaterialFileStatus,
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
        internal static List<RoutingWithMaterialFileModel> Mapping(List<USR_TMMA_ROUTING_WITH_MATERIAL_FILE> list)
        {
            ACTION = "Mapping(List<USR_TMMA_ROUTING_WITH_MATERIAL_FILE>)";

            try
            {
                if (list != null && list.Count > 0)
                {
                    List<RoutingWithMaterialFileModel> mList = new List<RoutingWithMaterialFileModel>();

                    foreach (USR_TMMA_ROUTING_WITH_MATERIAL_FILE o in list)
                    {
                        mList.Add(new RoutingWithMaterialFileModel()
                        {
                            RoutingWithMaterialFileID = o.RoutingWithMaterialFileID,
                            RecObjectName = o.RecObjectName,
                            UserSAP = o.UserSAP,
                            RoutingWithMaterialFilePath = o.RoutingWithMaterialFilePath,
                            RoutingWithMaterialFileVersion = o.RoutingWithMaterialFileVersion,
                            RoutingWithMaterialFileStatus = o.RoutingWithMaterialFileStatus,
                            FileStatus = FileStatusDAL.GetFileStatus(o.RoutingWithMaterialFileStatus),
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

        public static RoutingWithMaterialFileModel GetRoutingWithMaterialFile(int routingWithMaterialFileID)
        {
            ACTION = "GetRoutingWithMaterialFile(routingWithMaterialFileID)";

            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    USR_TMMA_ROUTING_WITH_MATERIAL_FILE obj = context.USR_TMMA_ROUTING_WITH_MATERIAL_FILE.Where(o => o.RoutingWithMaterialFileID == routingWithMaterialFileID).FirstOrDefault();
                    RoutingWithMaterialFileModel m = Mapping(obj);
                    return m;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<RoutingWithMaterialFileModel> GetRoutingWithMaterialFileList(RoutingWithMaterialFileFilterModel filter, ref int totalRecord, ref ResponseModel response)
        {
            ACTION = "GetRoutingWithMaterialFileList(RoutingWithMaterialFileFilterModel)";
            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    filter.RoutingWithMaterialFileIDs = filter.RoutingWithMaterialFileIDs != null ? filter.RoutingWithMaterialFileIDs : new int[] { };
                    var IQuery = context.USR_TMMA_ROUTING_WITH_MATERIAL_FILE
                    .Where(o =>
                        (!string.IsNullOrEmpty(filter.Keywords) ?
                            (o.RecObjectName.Contains(filter.Keywords)) ||
                            (o.CreatedBy.Contains(filter.Keywords))
                        : true)
                        && (filter.RoutingWithMaterialFileID.HasValue ? o.RoutingWithMaterialFileID == filter.RoutingWithMaterialFileID.Value : true)
                        && (filter.RoutingWithMaterialFileIDs.Count() > 0 ? filter.RoutingWithMaterialFileIDs.Contains(o.RoutingWithMaterialFileID) : true)
                        && (!string.IsNullOrEmpty(filter.RecObjectName) ? filter.CreatedBy.Contains(o.RecObjectName) : true)
                        && (filter.ProductsTypeID.HasValue ? o.ProductsTypeID == filter.ProductsTypeID.Value : true)
                        && (filter.IsActive.HasValue ? o.IsActive == 1 : true)
                    );

                    totalRecord = IQuery.Count();

                    List<USR_TMMA_ROUTING_WITH_MATERIAL_FILE> list = filter.Pagination.IsPaging ? IQuery.OrderBy(o => o.CreatedDate).Skip(filter.Pagination.Skip).Take(filter.Pagination.Take).ToList() : IQuery.ToList();

                    List<RoutingWithMaterialFileModel> mList = Mapping(list);

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

        public static ResponseModel AddRoutingWithMaterialFile(RoutingWithMaterialFileModel m, ref int _newID)
        {
            string action = "AddRoutingWithMaterialFile(RoutingWithMaterialFileModel, out _newID)";
            _newID = 0;
            try
            {
                if (m != null)
                {
                    using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                    {
                        USR_TMMA_ROUTING_WITH_MATERIAL_FILE _obj = Mapping(m);

                        context.USR_TMMA_ROUTING_WITH_MATERIAL_FILE.Add(_obj);

                        if (context.SaveChanges() > 0)
                        {
                            _newID = _obj.RoutingWithMaterialFileID;

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

        public static ResponseModel UpdateStatusRoutingWithMaterialFile(int RoutingWithMaterialFileID, int RoutingWithMaterialFileStatus)
        {
            string action = "UpdateStatusRoutingWithMaterialFile(RoutingWithMaterialFileModel)";
            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    USR_TMMA_ROUTING_WITH_MATERIAL_FILE _obj = context.USR_TMMA_ROUTING_WITH_MATERIAL_FILE.FirstOrDefault(o => o.RoutingWithMaterialFileID == RoutingWithMaterialFileID);


                    if (_obj != null)
                    {
                        _obj.RoutingWithMaterialFileStatus = RoutingWithMaterialFileStatus;
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
                        Message = "RoutingWithMaterialFileID Not Exist"
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

        public static RoutingWithMaterialFileModel GetRoutingWithMaterialFileLastVersion(int ProductsTypeID)
        {
            ACTION = "GetRoutingWithMaterialFileLastVersion()";
            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    var list = context.USR_TMMA_ROUTING_WITH_MATERIAL_FILE.Where(a => a.ProductsTypeID == ProductsTypeID).ToList();
                    var o = list.Last();
                    RoutingWithMaterialFileModel m = Mapping(o);

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