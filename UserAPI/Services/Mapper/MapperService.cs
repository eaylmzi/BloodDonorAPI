using AutoMapper;
using System.Drawing;
using user.Data.Models;
using user.Data.Models.dto;
using UserAPI.Services.Users;

namespace UserAPI.Services.Mapper
{
    public class MapperService : Profile
    {
        public MapperService()
        {
            CreateMap<UserRegisterDto, User>();
           
        }
    }
}
