using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TMMAConversions.DAL.Models;
using System.Web.Mvc;
using System.Web.Http;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TMMAConversions.UI.Controllers
{
    public class AuthenController : Controller
    {
        private string _ApiMDMUri;
        private string _ApiAuthenUri;
        private string _ApplicationId;
        private string _SecretKey;
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected TR HttpPost<T, TR>(string requestPath, T requestData)
        {
            using (var client = new HttpClient())
            {
                // setup client
                client.BaseAddress = new Uri(this._ApiMDMUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _Token);
                // make request
                var json = JsonConvert.SerializeObject(requestData);
                HttpContent content = new StringContent(json);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                var response = client.PostAsync(requestPath, content).Result;
                var returnObject = response.Content.ReadAsAsync<TR>().Result;

                return returnObject;
            }
        }
        private TR HttpPost<TR>()
        {
            log.Info("========== GetAccessToken =========");
            Initialize();
            var pairs = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>( "grant_type", "password" )
                    };
            var content = new FormUrlEncodedContent(pairs);
            System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this._ApiAuthenUri);
                client.DefaultRequestHeaders.Add("ApplicationId", _ApplicationId);
                client.DefaultRequestHeaders.Add("SecretKey", _SecretKey);
                var response = client.PostAsync(this._ApiAuthenUri, content).Result;
                var returnObject = response.Content.ReadAsAsync<TR>().Result;
                return returnObject;
            }  

        }

        public void Initialize()
        {
            this._ApiMDMUri = "https://scgchem-mdmdev.scg.com";
            this._ApiAuthenUri = "https://scgchem-mdmdev.scg.com/oauth/api/token";
            this._ApplicationId = "AD05C246-B8AB-43D1-B235-1EF4B4C6CB0B";
            this._SecretKey = "0D76A68D-8CBF-40EC-8584-71B8A6CDD815";
            
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

        }

        public string _Token
        {
            get
            {
                string value = "";
                if (string.IsNullOrEmpty(_CacheToken))
                {
                    _CacheToken = value = this.GetToken();
                    return value;
                }
                else
                {
                    return _CacheToken;
                }
            }
            set
            {
                _CacheToken = value;
            }
        }

        public string _CacheToken
        {
            get;
            set;
        }

        protected string GetToken()
        {
            //Call Authen              
            var res = HttpPost<MDMToken>();
            var token = res.Access_token;
            return token;
        }

    }

}