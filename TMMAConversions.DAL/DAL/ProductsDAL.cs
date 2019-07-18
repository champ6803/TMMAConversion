using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMMAConversions.DAL.Models;
using TMMAConversions.DAL.Entities;

namespace TMMAConversions.DAL.DAL
{
    public class ProductsDAL
    {
        static string SOURCE = "ProductsDAL";
        static string ACTION = "";

        internal static ProductsModel Mapping(USR_TMMA_PRODUCTS o)
        {
            try
            {
                if (o != null)
                {
                    return new ProductsModel()
                    {
                        ProductsID = o.ProductsID,
                        BOMFileID = o.BOMFileID,
                        RoutingFileID = o.RoutingFileID,
                        ProductsTypeID = o.ProductsTypeID,
                        Version = o.Version,
                        IsActive = o.IsActive == 1,
                        CreatedBy = o.CreatedBy,
                        CreatedDate = o.CreatedDate,
                        UpdatedBy = o.UpdatedBy,
                        UpdatedDate = o.UpdatedDate,
                        ProductsType = ProductsTypeDAL.GetProductsType(o.ProductsTypeID),
                        BOMFile = BOMFileDAL.GetBOMFile(o.BOMFileID.GetValueOrDefault()),
                        RoutingFile = RoutingFileDAL.GetRoutingFile(o.RoutingFileID.GetValueOrDefault())
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal static USR_TMMA_PRODUCTS Mapping(ProductsModel o)
        {
            try
            {
                if (o != null)
                {
                    return new USR_TMMA_PRODUCTS()
                    {
                        ProductsID = o.ProductsID,
                        BOMFileID = o.BOMFileID,
                        RoutingFileID = o.RoutingFileID,
                        ProductsTypeID = o.ProductsTypeID,
                        Version = o.Version,
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
        internal static List<ProductsModel> Mapping(List<USR_TMMA_PRODUCTS> list)
        {
            ACTION = "Mapping(List<USR_TMMA_PRODUCTS>)";

            try
            {
                if (list != null && list.Count > 0)
                {
                    List<ProductsModel> mList = new List<ProductsModel>();

                    foreach (USR_TMMA_PRODUCTS o in list)
                    {
                        mList.Add(new ProductsModel()
                        {
                            ProductsID = o.ProductsID,
                            BOMFileID = o.BOMFileID,
                            RoutingFileID = o.RoutingFileID,
                            ProductsTypeID = o.ProductsTypeID,
                            Version = o.Version,
                            IsActive = o.IsActive == 1,
                            CreatedBy = o.CreatedBy,
                            CreatedDate = o.CreatedDate,
                            UpdatedBy = o.UpdatedBy,
                            UpdatedDate = o.UpdatedDate,
                            ProductsType = ProductsTypeDAL.GetProductsType(o.ProductsTypeID),
                            BOMFile = BOMFileDAL.GetBOMFile(o.BOMFileID.GetValueOrDefault()),
                            RoutingFile = RoutingFileDAL.GetRoutingFile(o.RoutingFileID.GetValueOrDefault())
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

        public static List<ProductsModel> GetProductsList(ProductsFilterModel filter, ref int totalRecord, ref ResponseModel response)
        {
            ACTION = "GetProductsList(ProductsFilterModel)";
            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    filter.ProductsIDs = filter.ProductsIDs != null ? filter.ProductsIDs : new int[] { };
                    var IQuery = context.USR_TMMA_PRODUCTS
                    .Where(o =>
                        (!string.IsNullOrEmpty(filter.Keywords) ?
                            (o.CreatedBy.Contains(filter.Keywords))
                        : true)
                        && (filter.ProductsID.HasValue ? o.ProductsID == filter.ProductsID.Value : true)
                        && (filter.ProductsIDs.Count() > 0 ? filter.ProductsIDs.Contains(o.ProductsID) : true)
                        && (!string.IsNullOrEmpty(filter.CreatedBy) ? filter.CreatedBy.Contains(o.CreatedBy) : true)
                        && (filter.IsActive.HasValue ? o.IsActive == 1 : true)
                    );

                    totalRecord = IQuery.Count();

                    List<USR_TMMA_PRODUCTS> list = filter.Pagination.IsPaging ? IQuery.OrderBy(o => o.CreatedDate).Skip(filter.Pagination.Skip).Take(filter.Pagination.Take).ToList() : IQuery.ToList();

                    List<ProductsModel> mList = Mapping(list);

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

    }
}