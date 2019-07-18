using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using TMMAConversions.DAL.Entities;
using TMMAConversions.DAL.Models;

namespace TMMAConversions.DAL.DAL
{
    public class PackagingInstructionFileDAL
    {
        private static string SOURCE = "PackagingInstructionFileDAL";
        private static string ACTION = "";

        internal static PackagingInstructionFileModel Mapping(USR_TMMA_PACKAGING_INSTRUCTION_FILE o)
        {
            try
            {
                if (o != null)
                {
                    return new PackagingInstructionFileModel()
                    {
                        PackagingInstructionFileID = o.PackagingInstructionFileID,
                        RecObjectName = o.RecObjectName,
                        UserSAP = o.UserSAP,
                        PackagingInstructionFilePath = o.PackagingInstructionFilePath,
                        PackagingInstructionFileVersion = o.PackagingInstructionFileVersion,
                        PackagingInstructionFileStatus = o.PackagingInstructionFileStatus,
                        FileStatus = FileStatusDAL.GetFileStatus(o.PackagingInstructionFileStatus),
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
        internal static USR_TMMA_PACKAGING_INSTRUCTION_FILE Mapping(PackagingInstructionFileModel o)
        {
            try
            {
                if (o != null)
                {
                    return new USR_TMMA_PACKAGING_INSTRUCTION_FILE()
                    {
                        PackagingInstructionFileID = o.PackagingInstructionFileID,
                        RecObjectName = o.RecObjectName,
                        UserSAP = o.UserSAP,
                        PackagingInstructionFilePath = o.PackagingInstructionFilePath,
                        PackagingInstructionFileVersion = o.PackagingInstructionFileVersion,
                        PackagingInstructionFileStatus = o.PackagingInstructionFileStatus,
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
        internal static List<PackagingInstructionFileModel> Mapping(List<USR_TMMA_PACKAGING_INSTRUCTION_FILE> list)
        {
            ACTION = "Mapping(List<USR_TMMA_PACKAGING_INSTRUCTION_FILE>)";

            try
            {
                if (list != null && list.Count > 0)
                {
                    List<PackagingInstructionFileModel> mList = new List<PackagingInstructionFileModel>();

                    foreach (USR_TMMA_PACKAGING_INSTRUCTION_FILE o in list)
                    {
                        mList.Add(new PackagingInstructionFileModel()
                        {
                            PackagingInstructionFileID = o.PackagingInstructionFileID,
                            RecObjectName = o.RecObjectName,
                            UserSAP = o.UserSAP,
                            PackagingInstructionFilePath = o.PackagingInstructionFilePath,
                            PackagingInstructionFileVersion = o.PackagingInstructionFileVersion,
                            PackagingInstructionFileStatus = o.PackagingInstructionFileStatus,
                            FileStatus = FileStatusDAL.GetFileStatus(o.PackagingInstructionFileStatus),
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

        public static PackagingInstructionFileModel GetPackagingInstructionFile(int packagingInstructionFileID)
        {
            ACTION = "GetPackagingInstructionFile(packagingInstructionFileID)";

            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    USR_TMMA_PACKAGING_INSTRUCTION_FILE obj = context.USR_TMMA_PACKAGING_INSTRUCTION_FILE.Where(o => o.PackagingInstructionFileID == packagingInstructionFileID).FirstOrDefault();
                    PackagingInstructionFileModel m = Mapping(obj);
                    return m;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<PackagingInstructionFileModel> GetPackagingInstructionFileList(PackagingInstructionFileFilterModel filter, ref int totalRecord, ref ResponseModel response, ref decimal lastVersion)
        {
            ACTION = "GetPackagingInstructionFileList(PackagingInstructionFileFilterModel)";
            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    filter.PackagingInstructionFileIDs = filter.PackagingInstructionFileIDs != null ? filter.PackagingInstructionFileIDs : new int[] { };
                    var IQuery = context.USR_TMMA_PACKAGING_INSTRUCTION_FILE
                    .Where(o =>
                        (!string.IsNullOrEmpty(filter.Keywords) ?
                            (o.RecObjectName.Contains(filter.Keywords)) ||
                            (o.CreatedBy.Contains(filter.Keywords))
                        : true)
                        && (filter.PackagingInstructionFileID.HasValue ? o.PackagingInstructionFileID == filter.PackagingInstructionFileID.Value : true)
                        && (filter.PackagingInstructionFileIDs.Count() > 0 ? filter.PackagingInstructionFileIDs.Contains(o.PackagingInstructionFileID) : true)
                        && (!string.IsNullOrEmpty(filter.RecObjectName) ? filter.CreatedBy.Contains(o.RecObjectName) : true)
                        && (filter.ProductsTypeID.HasValue ? o.ProductsTypeID == filter.ProductsTypeID.Value : true)
                        && (filter.IsActive.HasValue ? o.IsActive == 1 : true)
                    );

                    totalRecord = IQuery.Count();

                    var lists = filter.Sort == "asc"
                        ? filter.Order == "RecObjectName" ? IQuery.OrderBy(o => o.RecObjectName)
                        : filter.Order == "UserSAP" ? IQuery.OrderBy(o => o.UserSAP)
                        : filter.Order == "Version" ? IQuery.OrderBy(o => o.PackagingInstructionFileVersion)
                        : filter.Order == "Status" ? IQuery.OrderBy(o => o.PackagingInstructionFileStatus)
                        : filter.Order == "CreatedBy" ? IQuery.OrderBy(o => o.CreatedBy)
                        : filter.Order == "CreatedDate" ? IQuery.OrderBy(o => o.CreatedDate)
                        : IQuery.OrderBy(o => o.CreatedDate)
                        : filter.Order == "RecObjectName" ? IQuery.OrderByDescending(o => o.RecObjectName)
                        : filter.Order == "UserSAP" ? IQuery.OrderByDescending(o => o.UserSAP)
                        : filter.Order == "Version" ? IQuery.OrderByDescending(o => o.PackagingInstructionFileVersion)
                        : filter.Order == "Status" ? IQuery.OrderByDescending(o => o.PackagingInstructionFileStatus)
                        : filter.Order == "CreatedBy" ? IQuery.OrderByDescending(o => o.CreatedBy)
                        : filter.Order == "CreatedDate" ? IQuery.OrderByDescending(o => o.CreatedDate)
                        : IQuery.OrderByDescending(o => o.CreatedDate);

                    lastVersion = IQuery.OrderByDescending(o => o.PackagingInstructionFileVersion).FirstOrDefault().PackagingInstructionFileVersion.GetValueOrDefault();

                    List<USR_TMMA_PACKAGING_INSTRUCTION_FILE> list = filter.Pagination.IsPaging ? lists.Skip(filter.Pagination.Skip).Take(filter.Pagination.Take).ToList() : lists.ToList();

                    List<PackagingInstructionFileModel> mList = Mapping(list);

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

        public static ResponseModel AddPackagingInstructionFile(PackagingInstructionFileModel m, ref int _newID)
        {
            string action = "AddPackagingInstructionFile(PackagingInstructionFileModel, out _newID)";
            _newID = 0;
            try
            {
                if (m != null)
                {
                    using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                    {
                        USR_TMMA_PACKAGING_INSTRUCTION_FILE _obj = Mapping(m);

                        context.USR_TMMA_PACKAGING_INSTRUCTION_FILE.Add(_obj);

                        if (context.SaveChanges() > 0)
                        {
                            _newID = _obj.PackagingInstructionFileID;

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

        public static ResponseModel UpdateStatusPackagingInstructionFile(int PackagingInstructionFileID, int PackagingInstructionFileStatus)
        {
            string action = "UpdateStatusPackagingInstructionFile(PackagingInstructionFileID, PackagingInstructionFileStatus)";
            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    USR_TMMA_PACKAGING_INSTRUCTION_FILE _obj = context.USR_TMMA_PACKAGING_INSTRUCTION_FILE.FirstOrDefault(o => o.PackagingInstructionFileID == PackagingInstructionFileID);


                    if (_obj != null)
                    {
                        _obj.PackagingInstructionFileStatus = PackagingInstructionFileStatus;
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
                        Message = "PackagingInstructionFileID Not Exist"
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

        public static PackagingInstructionFileModel GetPackagingInstructionFileLastVersion(int ProductsTypeID)
        {
            ACTION = "GetPackagingInstructionFileLastVersion(ProductsTypeID)";
            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    var list = context.USR_TMMA_PACKAGING_INSTRUCTION_FILE.Where(a => a.ProductsTypeID == ProductsTypeID).ToList();
                    var o = list.Last();
                    PackagingInstructionFileModel m = Mapping(o);

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