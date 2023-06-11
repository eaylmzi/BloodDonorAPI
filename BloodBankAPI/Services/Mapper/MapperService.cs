using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Drawing;
using System.Numerics;
using bloodbank.Data.Models.dto.Hospital.dto;
using bloodbank.Data;
using BloodBankAPI.Models;

namespace BloodBankAPI.Services.Mapper
{
    public class MapperService : Profile
    {
        public MapperService()
        {
            CreateMap<HospitalDto, Hospital>();
        }
    }
}
