using AutoMapper;
using bloodbank.Data.Models;
using bloodbank.Logic.Logics.Hospitals;
using donor.Data.Models;
using donor.Data.Models.dto.Branch.dto;
using donor.Logic.Logics.Brances;
using location.Data.Models;
using location.Data.Repositories.Cities;
using location.logic.Logics.Cities;
using location.logic.Logics.Towns;
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
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiVersion("1")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserLogic _userLogic;
        private readonly IJwtService _jwtService;
        private readonly ICipherService _cipherService;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IBranchLogic _branchLogic;
        private readonly IHospitalLogic _hospitalLogic;
        private readonly ICityLogic _cityLogic;
        private readonly ITownLogic _townLogic;

        public UserController(IUserLogic userLogic, IMapper mapper, ICipherService cipherService, IJwtService jwtService, IConfiguration configuration,
            IUserService userService, IBranchLogic branchLogic, IHospitalLogic hospitalLogic, ICityLogic cityLogic, ITownLogic townLogic)
        {
            _userLogic = userLogic;
            _mapper = mapper;
            _jwtService = jwtService;
            _cipherService = cipherService;
            _configuration = configuration;
            _userService = userService;
            _branchLogic = branchLogic;
            _hospitalLogic = hospitalLogic;
            _cityLogic = cityLogic;
            _townLogic = townLogic;
        }
        [HttpPost]
        public async Task<ActionResult<Response<UserAndBranchDto>>> RegisterToBranch([FromBody] BranchRegisterDto branchRegisterDto)
        {
            try
            {
                User user = _mapper.Map<User>(branchRegisterDto);
                // Email check to if user exist
                bool isUserExist = _userService.CheckUserExistByEmail(user.Email);
                if (isUserExist)
                {
                    return Ok(new Response<UserAndBranchDto> { Message = Error.USER_ALREADY_EXIST, Data = new UserAndBranchDto(), Progress = false });
                }
                //Find Branch
                City? city = _cityLogic.GetSingleByMethod(branchRegisterDto.City);
                if (city == null)
                {
                    return Ok(new Response<UserAndBranchDto> { Message = Error.CITY_NOT_FOUND, Data = new UserAndBranchDto(), Progress = false });
                }
                Town? town = _townLogic.GetSingleByMethod(branchRegisterDto.Town);
                if (town == null)
                {
                    return Ok(new Response<UserAndBranchDto> { Message = Error.TOWN_NOT_FOUND, Data = new UserAndBranchDto(), Progress = false });
                }
                Branch? branch = _branchLogic.GetSingleByMethods(city.Id, town.Id);
                if (branch == null)
                {
                    return Ok(new Response<UserAndBranchDto> { Message = Error.BRANCH_NOT_FOUND, Data = new UserAndBranchDto(), Progress = false });
                }
                user.HealthCenterId = branch.Id;

                //Assing password, hashing and salting
                _cipherService.CreatePasswordHash(branchRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                //Adding to database
                int userId = _userLogic.AddAndGetId(user);

                //Assing token according to branch user or hospital user
                user.Token = _jwtService.CreateToken(userId, user.Name, Role.BranchEmployee, _configuration);

                //Result
                return Ok(await _userService.UpdateUserAndCompleteRegisterOrDeleteIfFailedToBranch(userId, user, branch));              
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<Response<UserAndHospitalDto>>> RegisterToHospital([FromBody] HospitalRegisterDto hospitalRegisterDto)
        {
            try
            {
                User user = _mapper.Map<User>(hospitalRegisterDto);
                // Email check to if user exist
                bool isUserExist = _userService.CheckUserExistByEmail(user.Email);
                if (isUserExist)
                {
                    return Ok(new Response<UserAndHospitalDto> { Message = Error.USER_ALREADY_EXIST, Data = new UserAndHospitalDto(), Progress = false });
                }
                Hospital? hospital = _hospitalLogic.GetSingleByMethod(hospitalRegisterDto.HospitalName);
                if (hospital == null)
                {
                    return Ok(new Response<UserAndHospitalDto> { Message = Error.HOSPITAL_NOT_FOUND, Data = new UserAndHospitalDto(), Progress = false });
                }
                user.HealthCenterId = hospital.Id;
                //Assing password, hashing and salting
                _cipherService.CreatePasswordHash(hospitalRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                //Adding to database
                int userId = _userLogic.AddAndGetId(user);

                //Assing token according to branch user or hospital user
                user.Token = _jwtService.CreateToken(userId, user.Name, Role.HospitalEmployee, _configuration);

                //Result
                return Ok(await _userService.UpdateUserAndCompleteRegisterOrDeleteIfFailedToHospital(userId, user, hospital));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult<Response<UserAndBranchLocDto>> LoginToBranch([FromBody] UserLoginDto userLoginDto)
        {
            try
            {
                //Check email
                User? user = _userLogic.GetSingleByMethod(userLoginDto.Email);
                if (user == null)
                {
                    return Ok(new Response<UserAndBranchLocDto> { Message = Error.USER_NOT_FOUND, Data = new UserAndBranchLocDto(), Progress = false });
                }

                //Verify password
                bool isVerified = _cipherService.VerifyPasswordHash(user.PasswordHash, user.PasswordSalt, userLoginDto.Password);
                if (!isVerified)
                {
                    return Ok(new Response<UserAndBranchLocDto> { Message = Error.USER_NOT_FOUND, Data = new UserAndBranchLocDto(), Progress = false });
                }

                Branch? branch = _branchLogic.GetSingle(user.HealthCenterId);
                if(branch == null)
                {
                    return Ok(new Response<UserAndBranchLocDto> { Message = Error.BRANCH_NOT_FOUND, Data = new UserAndBranchLocDto(), Progress = false });
                }

                BranchDto branchDto = new BranchDto()
                {
                    Id = branch.Id,
                    City = _cityLogic.GetSingle(branch.City).Name,
                    Town = _townLogic.GetSingle(branch.Town).Name,
                    AbMinusBloodUnit = branch.AbMinusBloodUnit,
                    AbPlusBloodUnit = branch.AbPlusBloodUnit,
                    APlusBloodUnit = branch.APlusBloodUnit,
                    AMinusBloodUnit = branch.AMinusBloodUnit,
                    BMinusBloodUnit= branch.BMinusBloodUnit,
                    BPlusBloodUnit= branch.BPlusBloodUnit,
                    ZeroMinusBloodUnit = branch.ZeroMinusBloodUnit,
                    ZeroPlusBloodUnit = branch.ZeroPlusBloodUnit,
                    GeopointId = branch.GeopointId,
                };
                //Result
                return Ok(new Response<UserAndBranchLocDto> { Message = Success.USER_LOGIN_SUCCESSFULLY, Data = new UserAndBranchLocDto(){ User =user, Branch = branchDto }, Progress = true });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult<Response<UserAndHospitalDto>> LoginToHospital([FromBody] UserLoginDto userLoginDto)
        {
            try
            {
                //Check email
                User? user = _userLogic.GetSingleByMethod(userLoginDto.Email);
                if (user == null)
                {
                    return Ok(new Response<UserAndHospitalDto> { Message = Error.USER_NOT_FOUND, Data = new UserAndHospitalDto(), Progress = false });
                }

                //Verify password
                bool isVerified = _cipherService.VerifyPasswordHash(user.PasswordHash, user.PasswordSalt, userLoginDto.Password);
                if (!isVerified)
                {
                    return Ok(new Response<UserAndHospitalDto> { Message = Error.USER_NOT_FOUND, Data = new UserAndHospitalDto(), Progress = false });
                }

                Hospital? hospital = _hospitalLogic.GetSingle(user.HealthCenterId);
                if (hospital == null)
                {
                    return Ok(new Response<UserAndHospitalDto> { Message = Error.HOSPITAL_NOT_FOUND, Data = new UserAndHospitalDto(), Progress = false });
                }


                //Result
                return Ok(new Response<UserAndHospitalDto> { Message = Success.USER_LOGIN_SUCCESSFULLY, Data = new UserAndHospitalDto() { User = user, Hospital = hospital }, Progress = true });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}