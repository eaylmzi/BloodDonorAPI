namespace BloodBankAPI.Services.Security
{
    public interface ISecurityService
    {
        public bool Verify(IHeaderDictionary headers);
    }
}
