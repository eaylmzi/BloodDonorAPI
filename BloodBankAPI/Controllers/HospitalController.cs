using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;

using AutoMapper;
using bloodbank.Logic.Logics.Hospitals;
using bloodbank.Data.Models.dto.Hospital.dto;
using bloodbank.Data;
using bloodbank.Data.Models;

namespace bloodbank.Logic.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HospitalController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IHospitalLogic _hospitalLogic;


        public HospitalController(IHospitalLogic hospitalLogic, IMapper mapper)
        {
            _hospitalLogic = hospitalLogic;
            _mapper = mapper;


        }
        
        [HttpPost]
        public ActionResult<int> Add([FromBody] HospitalDto hospitalDto)
        {
            try
            {
                Hospital hospital = _mapper.Map<Hospital>(hospitalDto);
                int companyId = _hospitalLogic.AddAndGetId(hospital);
                if (companyId != -1)
                {
                    return Ok(hospital);
                }
                return Ok("no");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
