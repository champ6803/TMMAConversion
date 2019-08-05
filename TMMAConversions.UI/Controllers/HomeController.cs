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
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected Core core = new Core();
         
        //[CustomAuthorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}