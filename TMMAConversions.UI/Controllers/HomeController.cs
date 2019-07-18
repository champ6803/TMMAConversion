using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMMAConversions.BLL;
using TMMAConversions.DAL.Models;
using TMMAConversions.BLL.Utilities;
using System.Data.SqlClient;

namespace TMMAConversions.UI.Controllers
{
    public class HomeController : Controller
    {
        protected Core core = new Core();
         
        [CustomAuthorize]
        public ActionResult Index()
        {
            ProductsFilterModel filter = new ProductsFilterModel();
            filter.Pagination.Page = 1;
            ProductsViewModel model = core.GetProductsView(filter);

            var name = Environment.UserName;

            ViewData["ProductsViewModel"] = model;

            return View();
        }
    }
}