using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using user.Data;
using user.Data.Models;
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

        public UserService(IUserLogic userLogic, IMapper mapper, IJwtService jwtService, IConfiguration configuration)
        {
            _userLogic = userLogic;
            _mapper = mapper;
            _jwtService = jwtService;
            _configuration = configuration;
        }

        public string CreateTokenByUserRole(int userId , string username, string? hospitalName)
        {
            if (hospitalName.IsNullOrEmpty())
            {
                return _jwtService.CreateToken(userId,username,Role.BranchEmployee,_configuration);
            }
            return _jwtService.CreateToken(userId, username, Role.HospitalEmployee, _configuration);
        }

        public async Task<Response<User>> UpdateUserAndCompleteRegisterOrDeleteIfFailed(int userId, User user)
        {
            User? updatedUser = await _userLogic.UpdateAsync(userId, user);
            if (updatedUser == null)
            {
                bool isDeleted = _userLogic.Delete(userId);
                if (isDeleted)
                {
                    return new Response<User> { Message = Success.USER_DELETED_SUCCESSFULLY, Data = new User(), Progress = false };
                }
                return new Response<User> { Message = Error.USER_NOT_DELETED_SUCCESSFULLY_WHILE_REGISTERING, Data = new User(), Progress = false };
            }
            return new Response<User> { Message = Success.USER_ADDED_SUCCESSFULLY, Data = updatedUser, Progress = true };
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
