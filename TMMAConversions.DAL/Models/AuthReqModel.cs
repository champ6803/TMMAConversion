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
}