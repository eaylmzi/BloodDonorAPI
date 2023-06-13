using user.Data.Models;
using user.Data;
using donor.Data.Models;
using user.Data.Models.dto;
using bloodbank.Data.Models;

namespace UserAPI.Services.Users
{
    public interface IUserService
    {
        public string CreateTokenByUserRole(int userId, string username, string? hospitalName);
        public Task<Response<UserAndBranchDto>> UpdateUserAndCompleteRegisterOrDeleteIfFailedToBranch(int userId, User user, Branch branch);
        public Task<Response<UserAndHospitalDto>> UpdateUserAndCompleteRegisterOrDeleteIfFailedToHospital(int userId, User user, Hospital hospital);
        public bool CheckUserExistByEmail(string email);
    }
}
