namespace DonorAPI.Services.Security
{
    public interface ISecurityService
    {
        public bool Verify(IHeaderDictionary headers);
    }
}
