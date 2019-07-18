using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using TMMAConversions.DAL.Entities;
using TMMAConversions.DAL.Models;

namespace TMMAConversions.DAL.DAL
{
    public class RoutingWithoutMaterialFileDAL
    {
        private static string SOURCE = "RoutingWithoutMaterialFileDAL";
        private static string ACTION = "";

        internal static RoutingWithoutMaterialFileModel Mapping(USR_TMMA_ROUTING_WITHOUT_MATERIAL_FILE o)
        {
            try
            {
                if (o != null)
                {
                    return new RoutingWithoutMaterialFileModel()
                    {
                        RoutingWithoutMaterialFileID = o.RoutingWithoutMaterialFileID,
                        RecObjectName = o.RecObjectName,
                        UserSAP = o.UserSAP,
                        RoutingWithoutMaterialFilePath = o.RoutingWithoutMaterialFilePath,
                        RoutingWithoutMaterialFileVersion = o.RoutingWithoutMaterialFileVersion,
                        RoutingWithoutMaterialFileStatus = o.RoutingWithoutMaterialFileStatus,
                        FileStatus = FileStatusDAL.GetFileStatus(o.RoutingWithoutMaterialFileStatus),
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
        internal static USR_TMMA_ROUTING_WITHOUT_MATERIAL_FILE Mapping(RoutingWithoutMaterialFileModel o)
        {
            try
            {
                if (o != null)
                {
                    return new USR_TMMA_ROUTING_WITHOUT_MATERIAL_FILE()
                    {
                        RoutingWithoutMaterialFileID = o.RoutingWithoutMaterialFileID,
                        RecObjectName = o.RecObjectName,
                        UserSAP = o.UserSAP,
                        RoutingWithoutMaterialFilePath = o.RoutingWithoutMaterialFilePath,
                        RoutingWithoutMaterialFileVersion = o.RoutingWithoutMaterialFileVersion,
                        RoutingWithoutMaterialFileStatus = o.RoutingWithoutMaterialFileStatus,
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
        internal static List<RoutingWithoutMaterialFileModel> Mapping(List<USR_TMMA_ROUTING_WITHOUT_MATERIAL_FILE> list)
        {
            ACTION = "Mapping(List<USR_TMMA_ROUTING_WITHOUT_MATERIAL_FILE>)";

            try
            {
                if (list != null && list.Count > 0)
                {
                    List<RoutingWithoutMaterialFileModel> mList = new List<RoutingWithoutMaterialFileModel>();

                    foreach (USR_TMMA_ROUTING_WITHOUT_MATERIAL_FILE o in list)
                    {
                        mList.Add(new RoutingWithoutMaterialFileModel()
                        {
                            RoutingWithoutMaterialFileID = o.RoutingWithoutMaterialFileID,
                            RecObjectName = o.RecObjectName,
                            UserSAP = o.UserSAP,
                            RoutingWithoutMaterialFilePath = o.RoutingWithoutMaterialFilePath,
                            RoutingWithoutMaterialFileVersion = o.RoutingWithoutMaterialFileVersion,
                            RoutingWithoutMaterialFileStatus = o.RoutingWithoutMaterialFileStatus,
                            FileStatus = FileStatusDAL.GetFileStatus(o.RoutingWithoutMaterialFileStatus),
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

        public static RoutingWithoutMaterialFileModel GetRoutingWithoutMaterialFile(int routingWithoutMaterialFileID)
        {
            ACTION = "GetRoutingWithoutMaterialFile(routingWithoutMaterialFileID)";

            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    USR_TMMA_ROUTING_WITHOUT_MATERIAL_FILE obj = context.USR_TMMA_ROUTING_WITHOUT_MATERIAL_FILE.Where(o => o.RoutingWithoutMaterialFileID == routingWithoutMaterialFileID).FirstOrDefault();
                    RoutingWithoutMaterialFileModel m = Mapping(obj);
                    return m;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<RoutingWithoutMaterialFileModel> GetRoutingWithoutMaterialFileList(RoutingWithoutMaterialFileFilterModel filter, ref int totalRecord, ref ResponseModel response)
        {
            ACTION = "GetRoutingWithoutMaterialFileList(RoutingWithoutMaterialFileFilterModel)";
            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    filter.RoutingWithoutMaterialFileIDs = filter.RoutingWithoutMaterialFileIDs != null ? filter.RoutingWithoutMaterialFileIDs : new int[] { };
                    var IQuery = context.USR_TMMA_ROUTING_WITHOUT_MATERIAL_FILE
                    .Where(o =>
                        (!string.IsNullOrEmpty(filter.Keywords) ?
                            (o.RecObjectName.Contains(filter.Keywords)) ||
                            (o.CreatedBy.Contains(filter.Keywords))
                        : true)
                        && (filter.RoutingWithoutMaterialFileID.HasValue ? o.RoutingWithoutMaterialFileID == filter.RoutingWithoutMaterialFileID.Value : true)
                        && (filter.RoutingWithoutMaterialFileIDs.Count() > 0 ? filter.RoutingWithoutMaterialFileIDs.Contains(o.RoutingWithoutMaterialFileID) : true)
                        && (!string.IsNullOrEmpty(filter.RecObjectName) ? filter.CreatedBy.Contains(o.RecObjectName) : true)
                        && (filter.ProductsTypeID.HasValue ? o.ProductsTypeID == filter.ProductsTypeID.Value : true)
                        && (filter.IsActive.HasValue ? o.IsActive == 1 : true)
                    );

                    totalRecord = IQuery.Count();

                    List<USR_TMMA_ROUTING_WITHOUT_MATERIAL_FILE> list = filter.Pagination.IsPaging ? IQuery.OrderBy(o => o.CreatedDate).Skip(filter.Pagination.Skip).Take(filter.Pagination.Take).ToList() : IQuery.ToList();

                    List<RoutingWithoutMaterialFileModel> mList = Mapping(list);

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

        public static ResponseModel AddRoutingWithoutMaterialFile(RoutingWithoutMaterialFileModel m, ref int _newID)
        {
            string action = "AddRoutingWithoutMaterialFile(RoutingWithoutMaterialFileModel, out _newID)";
            _newID = 0;
            try
            {
                if (m != null)
                {
                    using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                    {
                        USR_TMMA_ROUTING_WITHOUT_MATERIAL_FILE _obj = Mapping(m);

                        context.USR_TMMA_ROUTING_WITHOUT_MATERIAL_FILE.Add(_obj);

                        if (context.SaveChanges() > 0)
                        {
                            _newID = _obj.RoutingWithoutMaterialFileID;

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

        public static ResponseModel UpdateStatusRoutingWithoutMaterialFile(int RoutingWithoutMaterialFileID, int RoutingWithoutMaterialFileStatus)
        {
            string action = "UpdateStatusRoutingWithoutMaterialFile(RoutingWithoutMaterialFileModel)";
            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    USR_TMMA_ROUTING_WITHOUT_MATERIAL_FILE _obj = context.USR_TMMA_ROUTING_WITHOUT_MATERIAL_FILE.FirstOrDefault(o => o.RoutingWithoutMaterialFileID == RoutingWithoutMaterialFileID);


                    if (_obj != null)
                    {
                        _obj.RoutingWithoutMaterialFileStatus = RoutingWithoutMaterialFileStatus;
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
                        Message = "RoutingWithoutMaterialFileID Not Exist"
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

        public static RoutingWithoutMaterialFileModel GetRoutingWithoutMaterialFileLastVersion(int ProductsTypeID)
        {
            ACTION = "GetRoutingWithoutMaterialFileLastVersion()";
            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    var list = context.USR_TMMA_ROUTING_WITHOUT_MATERIAL_FILE.Where(a => a.ProductsTypeID == ProductsTypeID).ToList();
                    var o = list.Last();
                    RoutingWithoutMaterialFileModel m = Mapping(o);

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