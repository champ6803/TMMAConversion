using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMMAConversions.DAL.DAL;
using TMMAConversions.DAL.Models;

namespace TMMAConversions.BLL.BLL
{
    public class ProductsBLL
    {
        static string SOURCE = "ProductsBLL";
        static string ACTION = "";

        public static ProductsViewModel GetProductsView(ProductsFilterModel filter)
        {
            ACTION = "GetProductsView(ProductsFilterModel)";
            try
            {
                ResponseModel response = new ResponseModel();
                int totalRecord = 0;

                var model = ProductsDAL.GetProductsList(filter, ref totalRecord, ref response);
                filter.Pagination.TotalRecord = totalRecord;

                PaginationModel pagination = new PaginationModel(totalRecord, filter.Pagination.Page, filter.Pagination.Take);

                filter.Pagination = pagination;

                return new ProductsViewModel()
                {
                    Action = ACTION,
                    Source = SOURCE,
                    Status = response.Status,
                    Message = response.Message,
                    List = model,
                    Filter = filter
                };
            }
            catch (Exception ex)
            {
                return new ProductsViewModel()
                {
                    Action = ACTION,
                    Source = SOURCE,
                    Status = false,
                    Message = ex.Message,
                    List = null,
                    Filter = filter
                };
            }
        }
    }
}
