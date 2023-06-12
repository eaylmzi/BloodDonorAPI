using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using user.Data;
using user.Data.Models;
using user.Data.Models.dto;
using user.Data.Resources.Roles;
using user.Data.Resources.String.Message;
using user.Logic.Logics.Users;
using UserAPI.Services.Cipher;
using UserAPI.Services.Jwt;
using UserAPI.Services.Users;

namespace UserAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserLogic _userLogic;
        private readonly IJwtService _jwtService;
        private readonly ICipherService _cipherService;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public UserController(IUserLogic userLogic, IMapper mapper, ICipherService cipherService, IJwtService jwtService, IConfiguration configuration,
            IUserService userService)
        {
            _userLogic = userLogic;
            _mapper = mapper;
            _jwtService = jwtService;
            _cipherService = cipherService;
            _configuration = configuration;
            _userService = userService;


        }
        [HttpPost]
        public async Task<ActionResult<Response<User>>> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            try
            {
                User user = _mapper.Map<User>(userRegisterDto);
                // Email check to if user exist
                bool isUserExist = _userService.CheckUserExistByEmail(user.Email);
                if (isUserExist)
                {
                    return Ok(new Response<User> { Message = Error.USER_ALREADY_EXIST, Data = new User(), Progress = false });
                }

                //Assing password, hashing and salting
                _cipherService.CreatePasswordHash(userRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                //Adding to database
                int userId = _userLogic.AddAndGetId(user);

                //Assing token according to branch user or hospital user
                user.Token = _userService.CreateTokenByUserRole(userId, user.Name,userRegisterDto.HospitalName);

                //Result
                return Ok(await _userService.UpdateUserAndCompleteRegisterOrDeleteIfFailed(userId, user));              
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult<Response<User>> Login([FromBody] UserLoginDto userLoginDto)
        {
            try
            {
                //Check email
                User? user = _userLogic.GetSingleByMethod(userLoginDto.Email);
                if (user == null)
                {
                    return Ok(new Response<User> { Message = Error.USER_NOT_FOUND, Data = new User(), Progress = false });
                }

                //Verify password
                bool isVerified = _cipherService.VerifyPasswordHash(user.PasswordHash, user.PasswordSalt, userLoginDto.Password);
                if (!isVerified)
                {
                    return Ok(new Response<User> { Message = Error.USER_NOT_FOUND, Data = new User(), Progress = false });
                }

                //Result
                return Ok(new Response<User> { Message = Success.USER_LOGIN_SUCCESSFULLY, Data = user, Progress = true });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public ActionResult<int> Yey()
        {
            try
            {
                return Ok(5);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}