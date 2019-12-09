﻿using System;
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
                //_RequestPath = "/v1.1/Api/Network/ChemVerifyADAccount";
                //var req = new ChemVerifyADAccount
                //{
                //    adAccount = username,
                //    password = password,
                //    ReferenceToken = _Token
                //};
                //var res = HttpPost<ChemVerifyADAccount, Response<string>>(_RequestPath, req);
                //_Token = res.ResponseBase.ReferenceToken;
                //var result = res.ResponseBase.MessageType;
                //if (result != 0)
                //{
                //    throw new ApplicationException("invalid username and password (" + res.ResponseBase.MessageTypeName + ")");
                //    log.Info("========== response : " + res.ResponseBase.MessageTypeName + "=========");
                //}
                //else
                //{
                    log.Info("========== user : " + username + "=========");
                    UserModel record = core.GetUserByUsername(username);

                    if (record == default(UserModel))
                    {
                        string newSalt = getSalt();
                        string _hashString = getHashString(password, newSalt);
                        SaveUser(username, _hashString, newSalt, "MDM");
                        record = core.GetUserByUsername(username);
                    }

                    string salt = record.Salt;
                    string hashString = getHashString(password, salt);

                    if (hashString == record.PasswordHash)
                    {
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

                //}
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

        public void SaveUser(string username, string passwordhash, string salt, string createby)
        {
            try
            {
                //Add User to DB
                UserModel user = new UserModel();
                user.RoleID = 1;
                user.Username = username;
                user.PasswordHash = passwordhash;
                user.Salt = salt;
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