using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMMAConversions.DAL.Models;
using TMMAConversions.BLL;
using TMMAConversions.BLL.Utilities;
using System.Security.Cryptography;

namespace TMMAConversions.UI.Controllers
{
    public class LoginController : Controller
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected Core core = new Core();

        // GET: Login
        [AllowAnonymous]
        public ActionResult Index()
        {
            UserModel UserModel = new UserModel();
            ViewData["UserModel"] = UserModel;
            
            return View();
        }

        [HttpPost]
        public ActionResult OnLogin(string username, string password)
        {
            try
            {
                UserModel record = core.GetUserByUsername(username);
                if (record == default(UserModel))
                {
                    throw new ApplicationException("invalid username and password");
                }

                string salt = record.Salt;
                byte[] passwordAndSaltBytes = System.Text.Encoding.UTF8.GetBytes(password + salt);
                byte[] hashBytes = new System.Security.Cryptography.SHA256Managed().ComputeHash(passwordAndSaltBytes);
                string hashString = Convert.ToBase64String(hashBytes);
                if (hashString == record.PasswordHash)
                {
                    Session["Username"] = record.Username;

                    log.Info("========== user : " + record.Username + "=========");

                    return Json(new ResponseModel()
                    {
                        Status = true,
                        Message = "Success"
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    throw new ApplicationException("invalid username and password");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);

                return Json(new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult OnLogout()
        {
            log.Info("========== Logout =========");
            Session.RemoveAll();
            return Redirect("/");
        }

    }
}