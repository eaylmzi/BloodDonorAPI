using AutoMapper;
using donor.Logic.Logics.Donors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using donor.Data.Resources.Roles;
using donor.Data.Resources.String.Message;
using user.Data.Models;
using user.Data;
using donor.Data.Models;
using donor.Data.Models.dto.Donor.dto;
using donor.Logic.Logics.Brances;
using donor.Data.Models.dto.DonationHistory.dto;
using donor.Logic.Logics.DonationHistories;
using DonorAPI.Services.Donors;
using donor.Logic.Logics.JoinTable;
using DonorAPI.Services.Jwt;
using user.Logic.Logics.Users;
using location.Data.Models;
using user.Data.Models.dto;
using DonorAPI.Services.Security;
using DonorAPI.Services.Location;
using bloodbank.Logic;
using DonorAPI.Services.Blob;
using location.logic.Logics.Cities;
using location.logic.Logics.Towns;
using bloodbank.Data.Models;

namespace DonorAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiVersion("1")]
    
    public class DonorController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IDonorLogic _donorLogic;
        private readonly IBranchLogic _branchLogic;
        private readonly ISecurityService _securityService;
        private readonly IDonationHistoryLogic _donationHistoryLogic;
        private readonly ILocationService _locationService;
        private readonly IDonorService _donorService;
        private readonly IJoinTable _joinTable;
        private readonly IJwtService _jwtService;
        private readonly IUserLogic _userLogic;
        private readonly ICityLogic _cityLogic;
        private readonly ITownLogic _townLogic;


        public DonorController(IMapper mapper, IConfiguration configuration, IDonorLogic donorLogic, ISecurityService securityService, ILocationService locationService, IBranchLogic branchLogic, IDonationHistoryLogic donationHistoryLogic, IDonorService donorService, IJoinTable joinTable, IJwtService jwtService, IUserLogic userLogic, ICityLogic cityLogic, ITownLogic townLogic)

        {
            _mapper = mapper;
            _configuration = configuration;
            _donorLogic = donorLogic;
            _securityService = securityService;
            _locationService = locationService;
            _branchLogic = branchLogic;
            _donationHistoryLogic = donationHistoryLogic;
            _donorService = donorService;
            _joinTable = joinTable;
            _jwtService = jwtService;
            _userLogic = userLogic;
            _cityLogic = cityLogic;
            _townLogic = townLogic;
        }
        [HttpPost, Authorize(Roles = $"{Role.BranchEmployee}")]
        public async Task<ActionResult<Response<Donor>>> Add([FromForm] DonorAdditionDto donorAdditionDto)
        {
            try
            {
                if (_securityService.Verify(Request.Headers))
                {
                    //Check dependencies
                    bool isBranchExist = _branchLogic.CheckExistence(donorAdditionDto.BranchId);
                    if (!isBranchExist)
                    {
                        return Ok(new Response<Donor> { Message = Error.BRANCH_NOT_FOUND, Data = new Donor(), Progress = false });
                    }
                    int? townId = _locationService.GetTownIfNotExistCreateAndGet(donorAdditionDto.Town);
                    if (townId == null)
                    {
                        return Ok(new Response<Donor> { Message = Error.TOWN_NOT_ADDED, Data = new Donor(), Progress = false });
                    }
                    int? cityId = _locationService.GetCityIfNotExistCreateAndGet(donorAdditionDto.City);
                    if(cityId == null)
                    {
                        return Ok(new Response<Donor> { Message = Error.CITY_NOT_ADDED, Data = new Donor(), Progress = false });
                    }
                  

                    //Adding donor
                    Donor donor = new Donor
                    {
                        BranchId = donorAdditionDto.BranchId,
                        Name = donorAdditionDto.Name.ToLower(),
                        Surname = donorAdditionDto.Surname.ToLower(),
                        BloodType = donorAdditionDto.BloodType,
                        Phone = donorAdditionDto.Phone,
                        City = (int)cityId,
                        Town = (int)townId,

                    };
                    int isDonorAdded = _donorLogic.AddAndGetId(donor);
                    if (!(isDonorAdded > 0)) 
                    {
                        return Ok(new Response<Donor> { Message = Error.DONOR_NOT_ADDED, Data = new Donor(), Progress = false });
                    }
                    BlobService _blobService = new BlobService();
                    await _blobService.UploadPhotoAsync(donorAdditionDto.Photo, donorAdditionDto.Name, donorAdditionDto.Surname, isDonorAdded);
                  

                    return Ok(new Response<Donor> { Message = Success.DONOR_ADDED, Data = donor, Progress = true });
                }
                else
                {
                    return BadRequest(new Response<Donor> { Message = Error.USER_NOT_VERIFIED, Data = new Donor(), Progress = false });
                }
            }


            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost, Authorize(Roles = $"{Role.BranchEmployee}")]
        public ActionResult<Response<Donor>> Find([FromBody] DonorIdentificationDto donorIdentificationDto)
        {
            try
            {
                //Knak bunu böyle değil join table yapıp acording to branch id yapçan
                if (_securityService.Verify(Request.Headers))
                {
                    int userId = _jwtService.GetUserIdFromToken(Request.Headers);
                    User? user = _userLogic.GetSingleByMethod(userId);
                    if (user == null)
                    {
                        return Ok(new Response<Donor> { Message = Error.USER_NOT_FOUND, Data = new Donor(), Progress = false });
                    }
                    List<Donor>? donorList = _joinTable.FindDonorByJoinTable(user.HealthCenterId,donorIdentificationDto.Name.ToLower(), donorIdentificationDto.Surname.ToLower());
                    if(donorList.Count == 0)
                    {
                        return Ok(new Response<Donor> { Message = Error.DONOR_NOT_FOUND, Data = new Donor(), Progress = false });
                    }
                    return Ok(new Response<Donor> { Message = Success.DONOR_FOUND, Data = donorList[0], Progress = true });
                }
                else
                {
                    return BadRequest(new Response<Donor> { Message = Error.USER_NOT_VERIFIED, Data = new Donor(), Progress = false });
                }
            }


            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost, Authorize(Roles = $"{Role.BranchEmployee}")]
        public async Task<ActionResult<Response<DonationHistory>>> Donate([FromBody] DonationHistoryDto donationHistoryDto)
        {
            try
            {
                if (_securityService.Verify(Request.Headers))
                {
                    //Updating tuple count
                    Donor? donor = _donorLogic.GetSingleByMethod(donationHistoryDto.DonorId);
                    if (donor == null)
                    {
                        return Ok(new Response<DonationHistory> { Message = Error.DONOR_NOT_FOUND, Data = new DonationHistory(), Progress = false });
                    }
                    Branch? branch = _branchLogic.GetSingleByMethod(donor.BranchId);
                    if (branch == null)
                    {
                        return Ok(new Response<DonationHistory> { Message = Error.BRANCH_NOT_FOUND, Data = new DonationHistory(), Progress = false });
                    }
                    bool isBranchBloodCountUpdated = await _donorService.UpdateBranchBloodCount(branch,donor.BloodType,donationHistoryDto.TupleCount);
                    if (!isBranchBloodCountUpdated)
                    {
                        return Ok(new Response<DonationHistory> { Message = Error.BLOOD_COUNT_NOT_UPDATED, Data = new DonationHistory(), Progress = false });
                    }

                    //Adding donation history
                    DonationHistory donationHistory = new DonationHistory()
                    {
                        DonationTime = DateTime.Now,
                        DonorId = donationHistoryDto.DonorId,
                        TupleCount = donationHistoryDto.TupleCount,
                    };

                    bool isDonationHistoryAdded = _donationHistoryLogic.Add(donationHistory);
                    if (!isDonationHistoryAdded)
                    {
                        bool isUndoBranchBloodCountUpdated = await _donorService.UndoUpdateBranchBloodCount(branch, donor.BloodType, donationHistoryDto.TupleCount);
                        if (!isUndoBranchBloodCountUpdated)
                        {
                            return Ok(new Response<DonationHistory> { Message = Error.UNDO_BLOOD_COUNT_NOT_UPDATED, Data = new DonationHistory(), Progress = false });
                        }
                        return Ok(new Response<DonationHistory> { Message = Error.DONATION_HISTORY_NOT_CREATED, Data = new DonationHistory(), Progress = false });
                    }
                    return Ok(new Response<DonationHistory> { Message = Success.DONATE_SUCCESSFULLY, Data = donationHistory, Progress = true });
                }
                else
                {
                    return BadRequest(new Response<Donor> { Message = Error.USER_NOT_VERIFIED, Data = new Donor(), Progress = false });
                }
            }


            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost, Authorize(Roles = $"{Role.BranchEmployee}")]
        public ActionResult<Response<List<DonorListDto>>> GetList()
        {
            try
            {
                if (_securityService.Verify(Request.Headers))
                {
                    int userId = _jwtService.GetUserIdFromToken(Request.Headers);
                    User? user = _userLogic.GetSingle(userId);
                    if (user == null)
                    {
                        return Ok(new Response<Hospital> { Message = Error.USER_NOT_FOUND, Data = new Hospital(), Progress = false });
                    }
                    Branch? branch = _branchLogic.GetSingle(user.HealthCenterId);
                    if (branch == null)
                    {
                        return BadRequest(new Response<List<Donor>> { Message = Error.BRANCH_NOT_FOUND, Data = new List<Donor>(), Progress = false });
                    }
                    
                    var list = _joinTable.GetDonorListByJoinTable(user.HealthCenterId);
                    List<DonorListDto> donorList = new List<DonorListDto>();
                    if (list.Count != 0)
                    {
                       
                        foreach(var item in list)
                        {
                            DonorListDto donorListDto = new DonorListDto()
                            {
                                Id = item.Id,
                                Name = item.Name,
                                Surname = item.Surname,
                                BloodType = item.BloodType,
                                BranchId = item.BranchId,
                                Phone = item.Phone,
                                City = _cityLogic.GetSingle(item.City).Name,
                                Town = _townLogic.GetSingle(item.Town).Name,


                            };
                            donorList.Add(donorListDto);
                        }
                        return Ok(new Response<List<DonorListDto>> { Message = Success.DONOR_LIST, Data = donorList, Progress = true });
                        
                    }
                    return BadRequest(new Response<List<DonorListDto>> { Message = Error.DONOR_LIST_EMPTY, Data = new List<DonorListDto>(), Progress = false });
                }
                else
                {
                    return BadRequest(new Response<Donor> { Message = Error.USER_NOT_VERIFIED, Data = new Donor(), Progress = false });
                }
            }


            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
