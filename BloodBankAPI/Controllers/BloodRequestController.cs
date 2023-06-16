using bloodbank.Data.Models.dto.BloodRequest.dto;
using bloodbank.Data.Resources.Roles;
using BloodBankAPI.Services.Security;
using donor.Data.Models.dto.Donor.dto;
using donor.Data.Models;
using donor.Logic.Logics.Brances;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using bloodbank.Data;
using bloodbank.Data.Resources.String;
using location.Data.Models;
using user.Data.Models.dto;
using location.logic.Logics.Cities;
using location.logic.Logics.Towns;
using DonorAPI.Services.Donors;
using BloodBankAPI.Services.Jwt;
using user.Logic.Logics.Users;
using user.Data.Models;
using bloodbank.Data.Models;
using bloodbank.Logic.Logics.Hospitals;
using BloodBankAPI.Services.Mail;
using bloodbank.Logic.Logics.BloodRequests;
using BloodBankAPI.Services.Donors;
using BloodBankAPI.Services.Location;
using bloodbank.Logic.Logics.JoinTable;
using Microsoft.IdentityModel.Tokens;
using location.logic.Logics.Geopoints;
using bloodbank.Logic;
using System.Collections.Generic;

namespace BloodBankAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiVersion("1")]
    public class BloodRequestController : Controller
    {
        private readonly IBranchLogic _branchLogic;
        private readonly ISecurityService _securityService;
        private readonly IDonorService _donorService;
        private readonly ILocationService _locationService;
        private readonly ICityLogic _cityLogic;
        private readonly ITownLogic _townLogic;
        private readonly IJoinTable _joinTable;
        private readonly IJwtService _jwtService;
        private readonly IUserLogic _userLogic;
        private readonly IHospitalLogic _hospitalLogic;
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;
        private readonly IBloodRequestLogic _bloodRequestLogic;
        private readonly IGeopointLogic _geopointLogic;


        public BloodRequestController(IBranchLogic branchLogic, ISecurityService securityService, ILocationService locationService, ICityLogic cityLogic, ITownLogic townLogic, IDonorService donorService, IJwtService jwtService, IUserLogic userLogic, IHospitalLogic hospitalLogic, IMailService mailService, IConfiguration configuration, IBloodRequestLogic bloodRequestLogic, IJoinTable joinTable, IGeopointLogic geopointLogic)
        {
            _branchLogic = branchLogic;
            _securityService = securityService;
            _locationService = locationService;
            _cityLogic = cityLogic;
            _townLogic = townLogic;
            _donorService = donorService;
            _jwtService = jwtService;
            _userLogic = userLogic;
            _hospitalLogic = hospitalLogic;
            _mailService = mailService;
            _configuration = configuration;
            _bloodRequestLogic = bloodRequestLogic;
            _joinTable = joinTable;
            _geopointLogic = geopointLogic;
        }
        [HttpGet]
        public IActionResult aaa()
        {
            return Ok(3);
        }

        [HttpPost, Authorize(Roles = $"{Role.HospitalEmployee}")]
        public async Task<ActionResult<Hospital>> RequestBlood([FromBody] BloodRequestAndBranchDto bloodRequestAndBranchDto)
        {//bundan sonra ne dönmemi istersin cemre
            if (_securityService.Verify(Request.Headers))
            {
                int userId = _jwtService.GetUserIdFromToken(Request.Headers);
                User? user = _userLogic.GetSingle(userId);
                if (user == null)
                {
                    return Ok(new Response<Hospital> { Message = Error.USER_NOT_FOUND, Data = new Hospital(), Progress = false });
                }
                Hospital? hospital = _hospitalLogic.GetSingle(user.HealthCenterId);
                if (hospital == null)
                {
                    return Ok(new Response<Hospital> { Message = Error.HOSPITAL_NOT_FOUND, Data = new Hospital(), Progress = false });
                }
                City? city = _cityLogic.GetSingleByMethod(bloodRequestAndBranchDto.City);
                if (city == null)
                {
                    return Ok(new Response<Hospital> { Message = Error.CITY_NOT_FOUND, Data = new Hospital(), Progress = false });
                }
                Town? town = _townLogic.GetSingleByMethod(bloodRequestAndBranchDto.Town);
                if (town == null)
                {
                    return Ok(new Response<Hospital> { Message = Error.TOWN_NOT_FOUND, Data = new Hospital(), Progress = false });
                }
                Branch? branch = _branchLogic.GetSingleByMethods(city.Id, town.Id);
                if (branch == null)
                {
                    return Ok(new Response<Hospital> { Message = Error.BRANCH_NOT_FOUND, Data = new Hospital(), Progress = false });
                }

                //TRANSFER
                if (_donorService.HasBlood(branch, bloodRequestAndBranchDto.BloodType, bloodRequestAndBranchDto.BloodCount))
                {
                    bool isBranchBloodCountDecreased = await _donorService.UndoUpdateBranchBloodCount(branch, bloodRequestAndBranchDto.BloodType, bloodRequestAndBranchDto.BloodCount);
                    if (!isBranchBloodCountDecreased)
                    {
                        return Ok(new Response<Hospital> { Message = Error.BRANCH_BLOOD_COUNT_NOT_DECREASED, Data = new Hospital(), Progress = false });
                    }
                    bool isHospitalBloodCountIncreased = await _donorService.UpdateHospitalBloodCount(hospital, bloodRequestAndBranchDto.BloodType, bloodRequestAndBranchDto.BloodCount);
                    if (!isHospitalBloodCountIncreased)
                    {
                        bool isBranchBloodCountIncreased = await _donorService.UpdateBranchBloodCount(branch, bloodRequestAndBranchDto.BloodType, bloodRequestAndBranchDto.BloodCount);
                        if (!isBranchBloodCountIncreased)
                        {
                            return Ok(new Response<Hospital> { Message = Error.BRANCH_BLOOD_COUNT_NOT_INCREASED, Data = new Hospital(), Progress = false });
                        }
                        return Ok(new Response<Hospital> { Message = Error.HOSPITAL_BLOOD_COUNT_NOT_INCREASED, Data = new Hospital(), Progress = false });
                    }
                    _mailService.SendMail(bloodRequestAndBranchDto.Email, "Blood request", "Your request"+ bloodRequestAndBranchDto.BloodType+" "+ bloodRequestAndBranchDto .BloodCount+ " unit is successfully complete", _configuration);
                    return Ok(new Response<Hospital> { Message = Success.TRANSFER_COMPLETE_SUCCESSFULLY, Data = hospital, Progress = true });
                }
                else
                {
                    Geopoint geopoint = _geopointLogic.GetSingle(bloodRequestAndBranchDto.GeopointId);
                    RequestedGeopointDto requestedGeopointDto = new RequestedGeopointDto();
                    GeopointDto hospitalGeopoint = new GeopointDto()
                    {
                        Latitude = geopoint.Latitude,
                        Longitude = geopoint.Longitude,
                    };
                    requestedGeopointDto.HospitalGeopoint = hospitalGeopoint;
                    List<IdDto> idDto = _joinTable.AllBranchListByJoinTable();
                    List<GeopointDto> geopointDtoList = new List<GeopointDto>();
                    foreach (IdDto idDtoItem in idDto)
                    {
                        Geopoint geo = _geopointLogic.GetSingle(idDtoItem.Id);
                        geopointDtoList.Add(new GeopointDto() { Latitude = geo.Latitude, Longitude = geo.Longitude });

                    }
                    requestedGeopointDto.RequestedBloodLine = geopointDtoList;
                    List<GeopointDto> nearestBranchList = DistanceManager.FindNearBranch(requestedGeopointDto);
                    if (nearestBranchList.IsNullOrEmpty())
                    {
                        return Ok(new Response<Hospital> { Message = Error.BRANCH_BLOOD_COUNT_NOT_DECREASED, Data = new Hospital(), Progress = false });
                    }
                    bool isFlag = false;
                    for (int i = 0; i < nearestBranchList.Count; i++)
                    {
                        Geopoint? geoPoint = _geopointLogic.GetSingleByMethods(nearestBranchList[i].Latitude, nearestBranchList[i].Longitude);
                        Branch? nearestBranch = _branchLogic.GetSingleByMethodGeoPoint(geoPoint.Id);
                        if (_donorService.HasBlood(nearestBranch, bloodRequestAndBranchDto.BloodType, bloodRequestAndBranchDto.BloodCount))
                        {
                            bool isBranchBloodCountDecreased = await _donorService.UndoUpdateBranchBloodCount(nearestBranch, bloodRequestAndBranchDto.BloodType, bloodRequestAndBranchDto.BloodCount);
                            if (!isBranchBloodCountDecreased)
                            {
                                return Ok(new Response<Hospital> { Message = Error.BRANCH_BLOOD_COUNT_NOT_DECREASED, Data = new Hospital(), Progress = false });
                            }
                            bool isHospitalBloodCountIncreased = await _donorService.UpdateHospitalBloodCount(hospital, bloodRequestAndBranchDto.BloodType, bloodRequestAndBranchDto.BloodCount);
                            if (!isHospitalBloodCountIncreased)
                            {
                                bool isBranchBloodCountIncreased = await _donorService.UpdateBranchBloodCount(branch, bloodRequestAndBranchDto.BloodType, bloodRequestAndBranchDto.BloodCount);
                                if (!isBranchBloodCountIncreased)
                                {
                                    return Ok(new Response<Hospital> { Message = Error.BRANCH_BLOOD_COUNT_NOT_INCREASED, Data = new Hospital(), Progress = false });
                                }
                                return Ok(new Response<Hospital> { Message = Error.HOSPITAL_BLOOD_COUNT_NOT_INCREASED, Data = new Hospital(), Progress = false });
                            }
                            _mailService.SendMail(bloodRequestAndBranchDto.Email, "Blood request", "Your request"+ bloodRequestAndBranchDto.BloodType+" "+ bloodRequestAndBranchDto .BloodCount+ " unit is successfully complete", _configuration);
                            isFlag = true;
                            return Ok(new Response<Hospital> { Message = Success.TRANSFER_COMPLETE_SUCCESSFULLY_IN_FIFTY, Data = hospital, Progress = true });
                        }
                    }
                    if (!isFlag)
                    {
                        BloodRequest bloodRequest = new BloodRequest()
                        {
                            BloodCount = bloodRequestAndBranchDto.BloodCount,
                            BloodType = bloodRequestAndBranchDto.BloodType,
                            DurationTime = bloodRequestAndBranchDto.DurationTime,
                            Town = bloodRequestAndBranchDto.Town,
                            City = bloodRequestAndBranchDto.City,
                            HospitalLongitude = geopoint.Longitude,
                            HospitalLatitude =geopoint.Latitude,
                            HospitalEmail = bloodRequestAndBranchDto.Email,
                        };
                        bool isBloodRequestAdded = _bloodRequestLogic.Add(bloodRequest);
                        if (!isBloodRequestAdded)
                        {
                            return Ok(new Response<BloodRequest> { Message = Error.BLOOD_REQUEST_NOT_ADDED, Data = new BloodRequest(), Progress = false });
                        }
                        return Ok(new Response<BloodRequest> { Message = Success.BLOOD_REQUEST_ADDED_SUCCESSFULLY, Data = bloodRequest, Progress = true });

                    }

                }
                //buraya bir else 50km arama elsesi Eğer bu da olmazsa aşşadakini çalıştır
                /*
                else
                {
                    BloodRequest bloodRequest = new BloodRequest()
                    {
                        BloodCount = bloodRequestAndBranchDto.BloodCount,
                        BloodType = bloodRequestAndBranchDto.BloodType,
                        DurationTime = bloodRequestAndBranchDto.DurationTime,
                        Town = bloodRequestAndBranchDto.Town,
                        City = bloodRequestAndBranchDto.City,
                        HospitalLongitude = bloodRequestAndBranchDto.HospitalLongitude,
                        HospitalLatitude = bloodRequestAndBranchDto.HospitalLatitude,
                        HospitalEmail = bloodRequestAndBranchDto.Email,
                    };
                    bool isBloodRequestAdded = _bloodRequestLogic.Add(bloodRequest);
                    if (!isBloodRequestAdded)
                    {
                        return Ok(new Response<UserAndBranchDto> { Message = Error.BLOOD_REQUEST_NOT_ADDED, Data = new UserAndBranchDto(), Progress = false });
                    }
                    return Ok(new Response<UserAndBranchDto> { Message = Success.BLOOD_REQUEST_ADDED_SUCCESSFULLY, Data = new UserAndBranchDto(), Progress = false });
                }
                */
            }
            else
            {
                return BadRequest(new Response<Hospital> { Message = Error.USER_NOT_VERIFIED, Data = new Hospital(), Progress = false });
            }

            return View();
        }
    


            [HttpPost]
        public async Task<ActionResult> aaaa()
        {
            RequestedGeopointDto requestedGeopointDto = new RequestedGeopointDto();
            requestedGeopointDto.HospitalGeopoint = new GeopointDto()
            {
                Latitude = 38.469939,
                Longitude = 27.210308,
            };
            List<GeopointDto> list = new List<GeopointDto>();
            list.Add(new GeopointDto()
            {
                Latitude = 38.310211,
                Longitude = 26.316068,

            });

            requestedGeopointDto.RequestedBloodLine = list;


            return Ok(DistanceManager.FindNearBranch(requestedGeopointDto));
        }
        [HttpPost]
        public async Task<ActionResult> AutomaticBloodRequest()
        {
            List<BloodRequest> bloodRequestList = _joinTable.CheckBloodRequestByJoinTable();
            foreach (BloodRequest bloodRequestListItem in bloodRequestList)
            {
                Geopoint geopoint = _geopointLogic.GetSingleByMethods(bloodRequestListItem.HospitalLatitude, bloodRequestListItem.HospitalLongitude);
                Hospital hospital = _hospitalLogic.GetSingleByMethodGeopoint(geopoint.Id);
                City? city = _cityLogic.GetSingleByMethod(bloodRequestListItem.City);
                Town? town = _townLogic.GetSingleByMethod(bloodRequestListItem.Town);
                Branch? branch = _branchLogic.GetSingleByMethods(city.Id, town.Id);
                //TRANSFER
                if (_donorService.HasBlood(branch, bloodRequestListItem.BloodType, bloodRequestListItem.BloodCount))
                {
                    bool isBranchBloodCountDecreased = await _donorService.UndoUpdateBranchBloodCount(branch, bloodRequestListItem.BloodType, bloodRequestListItem.BloodCount);
                    bool isHospitalBloodCountIncreased = await _donorService.UpdateHospitalBloodCount(hospital, bloodRequestListItem.BloodType, bloodRequestListItem.BloodCount);
                    if (!isHospitalBloodCountIncreased)
                    {
                        bool isBranchBloodCountIncreased = await _donorService.UpdateBranchBloodCount(branch, bloodRequestListItem.BloodType, bloodRequestListItem.BloodCount);
                    }
                    //_mailService.SendMail(bloodRequestListItem.Email, "Blood request", "Your request"+ bloodRequestListItem.BloodType+" "+ bloodRequestListItem.BloodCount+ " unit is successfully complete", _configuration);                 
                }
                else
                {
                    RequestedGeopointDto requestedGeopointDto = new RequestedGeopointDto();
                    GeopointDto hospitalGeopoint = new GeopointDto()
                    {
                        Latitude = bloodRequestListItem.HospitalLatitude,
                        Longitude = bloodRequestListItem.HospitalLongitude,
                    };
                    requestedGeopointDto.HospitalGeopoint = hospitalGeopoint;
                    List<IdDto> idDto = _joinTable.AllBranchListByJoinTable();
                    List<GeopointDto> geopointDtoList = new List<GeopointDto>();
                    foreach (IdDto idDtoItem in idDto)
                    {
                        Geopoint geo = _geopointLogic.GetSingle(idDtoItem.Id);
                        geopointDtoList.Add(new GeopointDto() { Latitude = geo.Latitude, Longitude = geo.Longitude });

                    }
                    requestedGeopointDto.RequestedBloodLine = geopointDtoList;
                    List<GeopointDto> nearestBranchList = DistanceManager.FindNearBranch(requestedGeopointDto);
                    for (int i = 0; i < nearestBranchList.Count; i++)
                    {
                        Geopoint? geoPoint = _geopointLogic.GetSingleByMethods(nearestBranchList[i].Latitude, nearestBranchList[i].Longitude);
                        Branch? nearestBranch = _branchLogic.GetSingleByMethodGeoPoint(geoPoint.Id);
                        if (_donorService.HasBlood(nearestBranch, bloodRequestListItem.BloodType, bloodRequestListItem.BloodCount))
                        {
                            bool isBranchBloodCountDecreased = await _donorService.UndoUpdateBranchBloodCount(nearestBranch, bloodRequestListItem.BloodType, bloodRequestListItem.BloodCount);
                            bool isHospitalBloodCountIncreased = await _donorService.UpdateHospitalBloodCount(hospital, bloodRequestListItem.BloodType, bloodRequestListItem.BloodCount);
                            bloodRequestListItem.DurationTime = DateTime.Now.AddDays(-1);
                            await _bloodRequestLogic.UpdateAsync(bloodRequestListItem.Id,bloodRequestListItem);
                            //_mailService.SendMail(bloodRequestListItem.Email, "Blood request", "Your request"+ bloodRequestListItem.BloodType+" "+ bloodRequestListItem.BloodCount+ " unit is successfully complete", _configuration);
                        }
                    }

                }


            }
            return Ok();

        }

        /*
         
        [HttpPost]
        public async Task<ActionResult> AutomaticBloodRequest()
        {
            List<BloodRequest> bloodRequestList = _joinTable.CheckBloodRequestByJoinTable();
            foreach (BloodRequest bloodRequestListItem in bloodRequestList)
            {
                Geopoint geopoint = _geopointLogic.GetSingleByMethods(bloodRequestListItem.HospitalLatitude, bloodRequestListItem.HospitalLongitude);
                Hospital hospital = _hospitalLogic.GetSingleByMethodGeopoint(geopoint.Id);
                City? city = _cityLogic.GetSingleByMethod(bloodRequestListItem.City);
                if (city == null)
                {
                    return Ok(new Response<Hospital> { Message = Error.CITY_NOT_FOUND, Data = new Hospital(), Progress = false });
                }
                Town? town = _townLogic.GetSingleByMethod(bloodRequestListItem.Town);
                if (town == null)
                {
                    return Ok(new Response<Hospital> { Message = Error.TOWN_NOT_FOUND, Data = new Hospital(), Progress = false });
                }
                Branch? branch = _branchLogic.GetSingleByMethods(city.Id, town.Id);
                if (branch == null)
                {
                    return Ok(new Response<Hospital> { Message = Error.BRANCH_NOT_FOUND, Data = new Hospital(), Progress = false });
                }

                //TRANSFER
                if (_donorService.HasBlood(branch, bloodRequestListItem.BloodType, bloodRequestListItem.BloodCount))
                {
                    bool isBranchBloodCountDecreased = await _donorService.UndoUpdateBranchBloodCount(branch, bloodRequestListItem.BloodType, bloodRequestListItem.BloodCount);
                    if (!isBranchBloodCountDecreased)
                    {
                        return Ok(new Response<Hospital> { Message = Error.BRANCH_BLOOD_COUNT_NOT_DECREASED, Data = new Hospital(), Progress = false });
                    }
                    bool isHospitalBloodCountIncreased = await _donorService.UpdateHospitalBloodCount(hospital, bloodRequestListItem.BloodType, bloodRequestListItem.BloodCount);
                    if (!isHospitalBloodCountIncreased)
                    {
                        bool isBranchBloodCountIncreased = await _donorService.UpdateBranchBloodCount(branch, bloodRequestListItem.BloodType, bloodRequestListItem.BloodCount);
                        if (!isBranchBloodCountIncreased)
                        {
                            return Ok(new Response<Hospital> { Message = Error.BRANCH_BLOOD_COUNT_NOT_INCREASED, Data = new Hospital(), Progress = false });
                        }
                        return Ok(new Response<Hospital> { Message = Error.HOSPITAL_BLOOD_COUNT_NOT_INCREASED, Data = new Hospital(), Progress = false });
                    }
                    //_mailService.SendMail(bloodRequestListItem.Email, "Blood request", "Your request"+ bloodRequestListItem.BloodType+" "+ bloodRequestListItem.BloodCount+ " unit is successfully complete", _configuration);
                    return Ok(new Response<Hospital> { Message = Success.TRANSFER_COMPLETE_SUCCESSFULLY, Data = hospital, Progress = true });
                }
                else
                {
                    RequestedGeopointDto requestedGeopointDto = new RequestedGeopointDto();
                    GeopointDto hospitalGeopoint = new GeopointDto()
                    {
                        Latitude = bloodRequestListItem.HospitalLatitude,
                        Longitude = bloodRequestListItem.HospitalLongitude,
                    };
                    requestedGeopointDto.HospitalGeopoint = hospitalGeopoint;
                    List<IdDto> idDto = _joinTable.AllBranchListByJoinTable();
                    List<GeopointDto> geopointDtoList = new List<GeopointDto>();
                    foreach (IdDto idDtoItem in idDto)
                    {
                        Geopoint geo = _geopointLogic.GetSingle(idDtoItem.Id);
                        geopointDtoList.Add(new GeopointDto() { Latitude = geo.Latitude, Longitude = geo.Longitude });

                    }
                    requestedGeopointDto.RequestedBloodLine = geopointDtoList;
                    List<GeopointDto> nearestBranchList = DistanceManager.FindNearBranch(requestedGeopointDto);
                    if (nearestBranchList.IsNullOrEmpty())
                    {
                        return Ok(new Response<Hospital> { Message = Error.BRANCH_BLOOD_COUNT_NOT_DECREASED, Data = new Hospital(), Progress = false });
                    }
                    bool isFlag = false;
                    for (int i = 0; i < nearestBranchList.Count; i++)
                    {
                        Geopoint? geoPoint = _geopointLogic.GetSingleByMethods(nearestBranchList[i].Latitude, nearestBranchList[i].Longitude);
                        Branch? nearestBranch = _branchLogic.GetSingleByMethodGeoPoint(geoPoint.Id);
                        if (_donorService.HasBlood(nearestBranch, bloodRequestListItem.BloodType, bloodRequestListItem.BloodCount))
                        {
                            bool isBranchBloodCountDecreased = await _donorService.UndoUpdateBranchBloodCount(nearestBranch, bloodRequestListItem.BloodType, bloodRequestListItem.BloodCount);
                            if (!isBranchBloodCountDecreased)
                            {
                                return Ok(new Response<Hospital> { Message = Error.BRANCH_BLOOD_COUNT_NOT_DECREASED, Data = new Hospital(), Progress = false });
                            }
                            bool isHospitalBloodCountIncreased = await _donorService.UpdateHospitalBloodCount(hospital, bloodRequestListItem.BloodType, bloodRequestListItem.BloodCount);
                            if (!isHospitalBloodCountIncreased)
                            {
                                bool isBranchBloodCountIncreased = await _donorService.UpdateBranchBloodCount(branch, bloodRequestListItem.BloodType, bloodRequestListItem.BloodCount);
                                if (!isBranchBloodCountIncreased)
                                {
                                    return Ok(new Response<Hospital> { Message = Error.BRANCH_BLOOD_COUNT_NOT_INCREASED, Data = new Hospital(), Progress = false });
                                }
                                return Ok(new Response<Hospital> { Message = Error.HOSPITAL_BLOOD_COUNT_NOT_INCREASED, Data = new Hospital(), Progress = false });
                            }
                            //_mailService.SendMail(bloodRequestListItem.Email, "Blood request", "Your request"+ bloodRequestListItem.BloodType+" "+ bloodRequestListItem.BloodCount+ " unit is successfully complete", _configuration);
                            isFlag = true;
                            return Ok(new Response<Hospital> { Message = Success.TRANSFER_COMPLETE_SUCCESSFULLY_IN_FIFTY, Data = hospital, Progress = true });
                        }
                    }

                }


            }
            return Ok();

        }
          */
    }
}