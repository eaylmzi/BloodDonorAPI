using System.Security.Cryptography;

namespace DonorAPI.Services.Cipher
{
    public class CipherService : ICipherService
    {
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }
        public bool VerifyPasswordHash(byte[] passwordHash, byte[] passwordSalt, string password)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                bool isMatched = computedHash.SequenceEqual(passwordHash);
                return isMatched;
            }
        }
    }
}
