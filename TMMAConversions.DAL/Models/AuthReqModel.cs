namespace TMMAConversions.DAL.Models
{
    public class CheckIPNetworkRequest : BaseReferenceToken
    {
        public string ip { get; set; }
    }
    public class ChemVerifyADAccount : BaseReferenceToken
    {
        public string adAccount { get; set; }
        public string password { get; set; }
    }
    public class BaseRequest : BaseReferenceToken
    {
        public bool IsNoCahce { get; set; }
        public int Limit { get; set; }
    }

    public class PublicHolidayRequest : BaseReferenceToken
    {

    }

    public class SSOVerifyReQuest
    {
        public string ssoAccount { get; set; }
        public string ssoPassword { get; set; }
        public string loginIpAddress { get; set; }
        public string loginDevice { get; set; }
        public string loginLocation { get; set; }
        public int ssoAccountType { get; set; }
        public string referenceToken { get; set; }
    }
}