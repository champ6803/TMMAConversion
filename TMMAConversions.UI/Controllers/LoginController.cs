using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMMAConversions.DAL.Models;
using TMMAConversions.BLL;
using TMMAConversions.BLL.Utilities;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace TMMAConversions.UI.Controllers
{
    public class LoginController : AuthenController
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected Core core = new Core();
        private string _RequestPath;

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
                _RequestPath = "/v1.1/Api/SSO/SSOVerify";
                var req = new SSOVerifyReQuest
                {
                    ssoAccount = username,
                    ssoPassword = password,
                    referenceToken = _Token
                };
                var res = HttpPost<SSOVerifyReQuest, Response<object>>(_RequestPath, req);                
                var result = res.ResponseBase.MessageType;
                if (result != 0)
                {
                    throw new ApplicationException("Can't connecnt (" + res.ResponseBase.MessageTypeName + ")");
                    log.Info("========== response : " + res.ResponseBase.MessageTypeName + "=========");
                }
                else
                {
                    SSOVerifyRes ssores = JsonConvert.DeserializeObject<SSOVerifyRes>(res.ResponseData.ToString());
                    _Token = ssores.ssoAccountToken;
                    if (ssores.ssoAccountType == 0)
                    {
                        log.Info("========== user : " + username + "=========");
                        UserModel record = core.GetUserByUsername(username);

                        if (record == default(UserModel))
                        {
                            
                            SaveUser(username, password, "AD");
                            record = core.GetUserByUsername(username);
                        }

                        string salt = record.Salt;
                        string hashString = getHashString(password, salt);

                        if (hashString != record.PasswordHash)
                        {
                            SaveUser(username, password, "AD");
                            record = core.GetUserByUsername(username);                            
                        }

                        Session["Username"] = record.Username;
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
            }
            catch (Exception ex)
            {
                log.Error("========== " + ex.Message + " =========");

                return Json(new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }

            #region old login
            //try
            //{
            //    UserModel record = core.GetUserByUsername(username);
            //    if (record == default(UserModel))
            //    {
            //        throw new ApplicationException("invalid username and password");
            //    }

            //    string salt = record.Salt;
            //    byte[] passwordAndSaltBytes = System.Text.Encoding.UTF8.GetBytes(password + salt);
            //    byte[] hashBytes = new System.Security.Cryptography.SHA256Managed().ComputeHash(passwordAndSaltBytes);
            //    string hashString = Convert.ToBase64String(hashBytes);
            //    if (hashString == record.PasswordHash)
            //    {
            //        Session["Username"] = record.Username;

            //        log.Info("========== user : " + record.Username + "=========");

            //        return Json(new ResponseModel()
            //        {
            //            Status = true,
            //            Message = "Success"
            //        }, JsonRequestBehavior.AllowGet);
            //    }
            //    else
            //    {
            //        throw new ApplicationException("invalid username and password");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    log.Error("========== " + ex.Message + " =========");

            //    return Json(new ResponseModel()
            //    {
            //        Status = false,
            //        Message = ex.Message
            //    }, JsonRequestBehavior.AllowGet);
            //}
            #endregion
        }

        [HttpGet]
        public ActionResult OnLogout()
        {
            log.Info("========== Logout =========");
            Session.RemoveAll();
            return Redirect("/");
        }

        private static string getSalt()
        {
            byte[] bytes = new byte[128 / 8];
            using (var keyGenerator = RandomNumberGenerator.Create())
            {
                keyGenerator.GetBytes(bytes);
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        private static string getHashString(string password, string salt)
        {
            byte[] passwordAndSaltBytes = System.Text.Encoding.UTF8.GetBytes(password + salt);
            byte[] hashBytes = new System.Security.Cryptography.SHA256Managed().ComputeHash(passwordAndSaltBytes);
            string hashString = Convert.ToBase64String(hashBytes);

            return hashString;
        }

        public void SaveUser(string username, string password, string createby)
        {
            try
            {
                string newSalt = getSalt();
                string _hashString = getHashString(password, newSalt);
                //Add User to DB
                UserModel user = new UserModel();
                user.RoleID = 1;
                user.Username = username;
                user.PasswordHash = _hashString;
                user.Salt = newSalt;
                user.IsActive = true;
                user.CreatedBy = createby;
                user.CreatedDate = DateTime.Now;

                ResponseModel res = core.AddUser(user);

                if (!res.Status)
                {
                    throw new Exception(res.Message);
                }
                else
                {
                    log.Info("========== Save to Database Success. =========");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}