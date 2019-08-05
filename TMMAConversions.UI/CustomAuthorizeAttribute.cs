using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMMAConversions.DAL.Models;
using TMMAConversions.BLL;
using TMMAConversions.BLL.Utilities;

namespace TMMAConversions.UI
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected Core core = new Core();
        private readonly string[] allowedroles;

        public CustomAuthorizeAttribute(params string[] roles)
        {
            this.allowedroles = roles; // select roles from controller
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (HttpContext.Current.Session["Username"] == null)
            {
                filterContext.Result = new RedirectResult("~/Login");
            }
        }

    }
}