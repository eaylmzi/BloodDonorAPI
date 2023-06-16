using AutoMapper;
using donor.Data.Resources.Roles;
using donor.Logic.Logics.Donors;
using DonorAPI.Services.Jwt;
using user.Data.Models;
using user.Logic.Logics.Users;

namespace DonorAPI.Services.Security
{
    public class SecurityService :ISecurityService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IJwtService _jwtService;
        private readonly IUserLogic _userLogic;


        public SecurityService(IMapper mapper, IConfiguration configuration, IJwtService jwtService, IUserLogic userLogic)

        {
            _mapper = mapper;
            _configuration = configuration;
            _jwtService = jwtService;
            _userLogic = userLogic;


        }
        public bool Verify(IHeaderDictionary headers)
        {
            string role = _jwtService.GetUserRoleFromToken(headers);
            if (role == Role.BranchEmployee)
            {
                User? user = _userLogic.GetSingle(_jwtService.GetUserIdFromToken(headers));
                if (user != null)
                {
                    if (user.Token == _jwtService.GetUserTokenFromToken(headers))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
