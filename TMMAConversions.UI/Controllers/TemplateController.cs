using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TMMAConversions.UI.Controllers
{
    public class TemplateController : Controller
    {
        // GET: Template
        [CustomAuthorize]
        public ActionResult Index()
        {
            return View();
        }

    }
}