using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using TMMAConversions.DAL.Entities;
using TMMAConversions.DAL.Models;

namespace TMMAConversions.DAL.DAL
{
    public class AssignMaterialToRoutingFileDAL
    {
        private static string SOURCE = "AssignMaterialToRoutingFileDAL";
        private static string ACTION = "";

        internal static AssignMaterialToRoutingFileModel Mapping(USR_TMMA_ASSIGN_MATERIAL_TO_ROUTING_FILE o)
        {
            try
            {
                if (o != null)
                {
                    return new AssignMaterialToRoutingFileModel()
                    {
                        AssignMaterialToRoutingFileID = o.AssignMaterialToRoutingFileID,
                        RecObjectName = o.RecObjectName,
                        UserSAP = o.UserSAP,
                        AssignMaterialToRoutingFilePath = o.AssignMaterialToRoutingFilePath,
                        AssignMaterialToRoutingFileVersion = o.AssignMaterialToRoutingFileVersion,
                        AssignMaterialToRoutingFileStatus = o.AssignMaterialToRoutingFileStatus,
                        FileStatus = FileStatusDAL.GetFileStatus(o.AssignMaterialToRoutingFileStatus),
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
        internal static USR_TMMA_ASSIGN_MATERIAL_TO_ROUTING_FILE Mapping(AssignMaterialToRoutingFileModel o)
        {
            try
            {
                if (o != null)
                {
                    return new USR_TMMA_ASSIGN_MATERIAL_TO_ROUTING_FILE()
                    {
                        AssignMaterialToRoutingFileID = o.AssignMaterialToRoutingFileID,
                        RecObjectName = o.RecObjectName,
                        UserSAP = o.UserSAP,
                        AssignMaterialToRoutingFilePath = o.AssignMaterialToRoutingFilePath,
                        AssignMaterialToRoutingFileVersion = o.AssignMaterialToRoutingFileVersion,
                        AssignMaterialToRoutingFileStatus = o.AssignMaterialToRoutingFileStatus,
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
        internal static List<AssignMaterialToRoutingFileModel> Mapping(List<USR_TMMA_ASSIGN_MATERIAL_TO_ROUTING_FILE> list)
        {
            ACTION = "Mapping(List<USR_TMMA_ASSIGN_MATERIAL_TO_ROUTING_FILE>)";

            try
            {
                if (list != null && list.Count > 0)
                {
                    List<AssignMaterialToRoutingFileModel> mList = new List<AssignMaterialToRoutingFileModel>();

                    foreach (USR_TMMA_ASSIGN_MATERIAL_TO_ROUTING_FILE o in list)
                    {
                        mList.Add(new AssignMaterialToRoutingFileModel()
                        {
                            AssignMaterialToRoutingFileID = o.AssignMaterialToRoutingFileID,
                            RecObjectName = o.RecObjectName,
                            UserSAP = o.UserSAP,
                            AssignMaterialToRoutingFilePath = o.AssignMaterialToRoutingFilePath,
                            AssignMaterialToRoutingFileVersion = o.AssignMaterialToRoutingFileVersion,
                            AssignMaterialToRoutingFileStatus = o.AssignMaterialToRoutingFileStatus,
                            FileStatus = FileStatusDAL.GetFileStatus(o.AssignMaterialToRoutingFileStatus),
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

        public static AssignMaterialToRoutingFileModel GetAssignMaterialToRoutingFile(int assignMaterialToRoutingFileID)
        {
            ACTION = "GetAssignMaterialToRoutingFile(assignMaterialToRoutingFileID)";

            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    USR_TMMA_ASSIGN_MATERIAL_TO_ROUTING_FILE obj = context.USR_TMMA_ASSIGN_MATERIAL_TO_ROUTING_FILE.Where(o => o.AssignMaterialToRoutingFileID == assignMaterialToRoutingFileID).FirstOrDefault();
                    AssignMaterialToRoutingFileModel m = Mapping(obj);
                    return m;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<AssignMaterialToRoutingFileModel> GetAssignMaterialToRoutingFileList(AssignMaterialToRoutingFileFilterModel filter, ref int totalRecord, ref ResponseModel response)
        {
            ACTION = "GetAssignMaterialToRoutingFileList(AssignMaterialToRoutingFileFilterModel)";
            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    filter.AssignMaterialToRoutingFileIDs = filter.AssignMaterialToRoutingFileIDs != null ? filter.AssignMaterialToRoutingFileIDs : new int[] { };
                    var IQuery = context.USR_TMMA_ASSIGN_MATERIAL_TO_ROUTING_FILE
                    .Where(o =>
                        (!string.IsNullOrEmpty(filter.Keywords) ?
                            (o.RecObjectName.Contains(filter.Keywords)) ||
                            (o.CreatedBy.Contains(filter.Keywords))
                        : true)
                        && (filter.AssignMaterialToRoutingFileID.HasValue ? o.AssignMaterialToRoutingFileID == filter.AssignMaterialToRoutingFileID.Value : true)
                        && (filter.AssignMaterialToRoutingFileIDs.Count() > 0 ? filter.AssignMaterialToRoutingFileIDs.Contains(o.AssignMaterialToRoutingFileID) : true)
                        && (!string.IsNullOrEmpty(filter.RecObjectName) ? filter.CreatedBy.Contains(o.RecObjectName) : true)
                        && (filter.ProductsTypeID.HasValue ? o.ProductsTypeID == filter.ProductsTypeID.Value : true)
                        && (filter.IsActive.HasValue ? o.IsActive == 1 : true)
                    );

                    totalRecord = IQuery.Count();

                    List<USR_TMMA_ASSIGN_MATERIAL_TO_ROUTING_FILE> list = filter.Pagination.IsPaging ? IQuery.OrderBy(o => o.CreatedDate).Skip(filter.Pagination.Skip).Take(filter.Pagination.Take).ToList() : IQuery.ToList();

                    List<AssignMaterialToRoutingFileModel> mList = Mapping(list);

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

        public static ResponseModel AddAssignMaterialToRoutingFile(AssignMaterialToRoutingFileModel m, ref int _newID)
        {
            string action = "AddAssignMaterialToRoutingFile(AssignMaterialToRoutingFileModel, out _newID)";
            _newID = 0;
            try
            {
                if (m != null)
                {
                    using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                    {
                        USR_TMMA_ASSIGN_MATERIAL_TO_ROUTING_FILE _obj = Mapping(m);

                        context.USR_TMMA_ASSIGN_MATERIAL_TO_ROUTING_FILE.Add(_obj);

                        if (context.SaveChanges() > 0)
                        {
                            _newID = _obj.AssignMaterialToRoutingFileID;

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

        public static ResponseModel UpdateStatusAssignMaterialToRoutingFile(int AssignMaterialToRoutingFileID, int AssignMaterialToRoutingFileStatus)
        {
            string action = "UpdateStatusAssignMaterialToRoutingFile(AssignMaterialToRoutingFileID, AssignMaterialToRoutingFileStatus)";
            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    USR_TMMA_ASSIGN_MATERIAL_TO_ROUTING_FILE _obj = context.USR_TMMA_ASSIGN_MATERIAL_TO_ROUTING_FILE.FirstOrDefault(o => o.AssignMaterialToRoutingFileID == AssignMaterialToRoutingFileID);


                    if (_obj != null)
                    {
                        _obj.AssignMaterialToRoutingFileStatus = AssignMaterialToRoutingFileStatus;
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
                        Message = "AssignMaterialToRoutingFileID Not Exist"
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

        public static AssignMaterialToRoutingFileModel GetAssignMaterialToRoutingFileLastVersion(int ProductsTypeID)
        {
            ACTION = "GetAssignMaterialToRoutingFileLastVersion()";
            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    var list = context.USR_TMMA_ASSIGN_MATERIAL_TO_ROUTING_FILE.Where(a => a.ProductsTypeID == ProductsTypeID).ToList();
                    var o = list.Last();
                    AssignMaterialToRoutingFileModel m = Mapping(o);

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