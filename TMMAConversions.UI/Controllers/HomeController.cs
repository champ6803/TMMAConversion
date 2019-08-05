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
            return View();
        }
    }
}