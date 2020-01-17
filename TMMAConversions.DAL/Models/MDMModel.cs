using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMMAConversions.DAL.Models
{
    public class BaseReferenceToken
    {
        public string ReferenceToken { get; set; }
    }

    public class Response<T>
    {
        public T ResponseData { get; set; }
        public BaseResponse ResponseBase { get; set; }
    }

    public class BaseResponse
    {
        public DateTime ReferenceTime { get; set; }
        public int MessageType { get; set; }
        public string MessageTypeName { get; set; }
        public Exception Exception { get; set; }
        public string ReferenceToken { get; set; }
    }

    public enum MessageType
    {
        Success = 0,
        Exception = 1,
        AccessExpired = 2,
        AccessDenied = 3,
        AccessOverLimit = 4,
        PermissionDenied = 5,
        BlockedResultError = 6
    }

    public class MDMAuthen
    {
        public string grant_type { get; set; }
    }

    public class MDMToken
    {
        public string Access_token { get; set; }
        public string Token_type { get; set; }
        public int Expires_in { get; set; }
    }

    public class ODataBaseResponse<T>
    {
        [JsonProperty("odata.metadata")]
        public string Metadata { get; set; }
        public T Value { get; set; }
    }

    public class SSOVerifyRes
    {
        public int ssoAccountType { get; set; }
        public string ssoAccountToken { get; set; }
        public int minuteExpired { get; set; }

    }
}