using user.Data.Models;
using user.Data;

namespace UserAPI.Services.Users
{
    public interface IUserService
    {
        public string CreateTokenByUserRole(int userId, string username, string? hospitalName);
        public Task<Response<User>> UpdateUserAndCompleteRegisterOrDeleteIfFailed(int userId, User user);
        public bool CheckUserExistByEmail(string email);
    }
}
