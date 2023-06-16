namespace BloodBankAPI.Services.Mail
{
    public interface IMailService
    {
        public bool SendMail(string emailAddress, string subject, string body, IConfiguration configuration);
    }
}
