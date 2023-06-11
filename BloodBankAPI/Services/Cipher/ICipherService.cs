namespace BloodBankAPI.Services.Cipher
{
    public interface ICipherService
    {
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        public bool VerifyPasswordHash(byte[] passwordHash, byte[] passwordSalt, string password);
    }
}
