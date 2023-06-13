using AutoMapper;
using bloodbank.Data.Models;
using donor.Data.Models;
using location.Data.Models;
using location.logic.Logics.Cities;
using location.logic.Logics.Towns;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using user.Data;
using user.Data.Models;
using user.Data.Models.dto;
using user.Data.Resources.Roles;
using user.Data.Resources.String.Message;
using user.Logic.Logics.Users;
using UserAPI.Services.Jwt;

namespace UserAPI.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserLogic _userLogic;
        private readonly IJwtService _jwtService;
        private readonly IConfiguration _configuration;
        private readonly ICityLogic _cityLogic;
        private readonly ITownLogic _townLogic;

        public UserService(IUserLogic userLogic, IMapper mapper, IJwtService jwtService, IConfiguration configuration, ICityLogic cityLogic, ITownLogic townLogic)
        {
            _userLogic = userLogic;
            _mapper = mapper;
            _jwtService = jwtService;
            _configuration = configuration;
            _cityLogic = cityLogic;
            _townLogic = townLogic;
        }

        public string CreateTokenByUserRole(int userId , string username, string? hospitalName)
        {
            if (hospitalName.IsNullOrEmpty())
            {
                return _jwtService.CreateToken(userId,username,Role.BranchEmployee,_configuration);
            }
            return _jwtService.CreateToken(userId, username, Role.HospitalEmployee, _configuration);
        }

        public async Task<Response<UserAndBranchDto>> UpdateUserAndCompleteRegisterOrDeleteIfFailedToBranch(int userId, User user,Branch branch)
        {
            User? updatedUser = await _userLogic.UpdateAsync(userId, user);
            if (updatedUser == null)
            {
                bool isDeleted = _userLogic.Delete(userId);
                if (isDeleted)
                {
                    return new Response<UserAndBranchDto> { Message = Success.USER_DELETED_SUCCESSFULLY, Data = new UserAndBranchDto(), Progress = false };
                }
                return new Response<UserAndBranchDto> { Message = Error.USER_NOT_DELETED_SUCCESSFULLY_WHILE_REGISTERING, Data = new UserAndBranchDto(), Progress = false };
            }
            return new Response<UserAndBranchDto> { Message = Success.USER_ADDED_SUCCESSFULLY, Data = new UserAndBranchDto() { User = user, Branch = branch}, Progress = true };
        }
        public async Task<Response<UserAndHospitalDto>> UpdateUserAndCompleteRegisterOrDeleteIfFailedToHospital(int userId, User user, Hospital hospital)
        {
            User? updatedUser = await _userLogic.UpdateAsync(userId, user);
            if (updatedUser == null)
            {
                bool isDeleted = _userLogic.Delete(userId);
                if (isDeleted)
                {
                    return new Response<UserAndHospitalDto> { Message = Success.USER_DELETED_SUCCESSFULLY, Data = new UserAndHospitalDto(), Progress = false };
                }
                return new Response<UserAndHospitalDto> { Message = Error.USER_NOT_DELETED_SUCCESSFULLY_WHILE_REGISTERING, Data = new UserAndHospitalDto(), Progress = false };
            }
            return new Response<UserAndHospitalDto> { Message = Success.USER_ADDED_SUCCESSFULLY, Data = new UserAndHospitalDto() { User = user, Hospital = hospital }, Progress = true };
        }
        public bool CheckUserExistByEmail(string email)
        {
            User? user = _userLogic.GetSingleByMethod(email);
            if(user != null)
            {
                return true;
            }
            return false;
        }

    }
}
