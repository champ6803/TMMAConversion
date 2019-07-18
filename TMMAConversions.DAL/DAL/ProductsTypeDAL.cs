using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMMAConversions.DAL.Models;
using TMMAConversions.DAL.Entities;

namespace TMMAConversions.DAL.DAL
{
    public class ProductsTypeDAL
    {
        static string SOURCE = "ProductsTypeDAL";
        static string ACTION = "";

        internal static ProductsTypeModel Mapping(USR_TMMA_PRODUCTS_TYPE o)
        {
            try
            {
                if (o != null)
                {
                    return new ProductsTypeModel()
                    {
                        ProductsTypeID = o.ProductsTypeID,
                        ProductsTypeName = o.ProductsTypeName,
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

        public static ProductsTypeModel GetProductsType(int productsTypeID)
        {
            ACTION = "GetProductsType(productsTypeID)";

            try
            {
                using (UTMMABCDBEntities context = new UTMMABCDBEntities())
                {
                    USR_TMMA_PRODUCTS_TYPE obj = context.USR_TMMA_PRODUCTS_TYPE.Where(o => o.ProductsTypeID == productsTypeID).FirstOrDefault();
                    ProductsTypeModel m = Mapping(obj);

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
