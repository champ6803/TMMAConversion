using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMMAConversions.DAL.Models
{
    public class ProductsViewModel : ResponseModel
    {
        public List<ProductsModel> List { get; set; }
        public ProductsFilterModel Filter { get; set; }
    }
    public class ProductsModel
    {
        public int ProductsID { get; set; }
        public int? BOMFileID { get; set; }
        public int? RoutingFileID { get; set; }
        public int ProductsTypeID { get; set; }
        public decimal? Version { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public ProductsTypeModel ProductsType { get; set; }
        public BOMFileModel BOMFile { get; set; }
        public RoutingFileModel RoutingFile { get; set;}
    }

    public class ProductsFilterModel
    {
        public string Keywords { get; set; }
        public int? ProductsID { get; set; }
        public int[] ProductsIDs { get; set; }
        public int? BOMFileID { get; set; }
        public int? RoutingFileID { get; set; }
        public int? ProductsTypeID { get; set; }
        public decimal? Version { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public ProductsTypeModel ProductsType { get; set; }
        public BOMFileModel BOMFile { get; set; }
        public RoutingFileModel RoutingFile { get; set; }
        public PaginationModel Pagination { get; set; }

        public ProductsFilterModel()
        {
            Pagination = new PaginationModel();
        }
    }
}