using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMMAConversions.DAL.DAL;
using TMMAConversions.DAL.Models;

namespace TMMAConversions.BLL.BLL
{
    public class ProductionVersionFileBLL
    {
        private static string SOURCE = "ProductionVersionFileBLL";
        private static string ACTION = "";

        public static ProductionVersionFileViewModel GetProductionVersionFileView(ProductionVersionFileFilterModel filter)
        {
            ACTION = "GetProductionVersionFileView(ProductionVersionFileFilterModel)";
            try
            {
                ResponseModel response = new ResponseModel();
                int totalRecord = 0;
                decimal lastVersion = 0;

                var model = ProductionVersionFileDAL.GetProductionVersionFileList(filter, ref totalRecord, ref response, ref lastVersion);
                filter.Pagination.TotalRecord = totalRecord;
                filter.LastProductionVersionFileVersion = lastVersion;

                PaginationModel pagination = new PaginationModel(totalRecord, filter.Pagination.Page, filter.Pagination.Take);

                filter.Pagination = pagination;

                return new ProductionVersionFileViewModel()
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
                return new ProductionVersionFileViewModel()
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

        public static ResponseModel AddProductionVersionFile(ProductionVersionFileModel m, ref int _newID)
        {
            ACTION = "AddProductionVersionFile(ProductionVersionFileModel)";
            try
            {
                _newID = 0;
                ResponseModel res = ProductionVersionFileDAL.AddProductionVersionFile(m, ref _newID);
                return new ResponseModel()
                {
                    Action = ACTION,
                    Source = SOURCE,
                    Message = res.Message,
                    Status = res.Status
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel()
                {
                    Action = ACTION,
                    Source = SOURCE,
                    Message = ex.Message,
                    Status = false
                };
            }
        }

        public static ProductionVersionFileModel GetProductionVersionFileLastVersion()
        {
            ACTION = "GetProductionVersionFileLastVersion()";
            try
            {
                return ProductionVersionFileDAL.GetProductionVersionFileLastVersion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ResponseModel UpdateStatusProductionVersionFile(int ProductionVersionFileID, int ProductionVersionFileStatus)
        {
            ACTION = "UpdateStatusProductionVersionFile(ProductionVersionFileID, ProductionVersionFileStatus)";
            try
            {
                ResponseModel res = ProductionVersionFileDAL.UpdateStatusProductionVersionFile(ProductionVersionFileID, ProductionVersionFileStatus);
                return new ResponseModel()
                {
                    Action = ACTION,
                    Source = SOURCE,
                    Message = res.Message,
                    Status = res.Status
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel()
                {
                    Action = ACTION,
                    Source = SOURCE,
                    Message = ex.Message,
                    Status = false
                };
            }
        }
    }
}