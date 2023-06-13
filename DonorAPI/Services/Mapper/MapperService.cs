using AutoMapper;
using user.Data.Models.dto;
using user.Data.Models;
using donor.Data.Models.dto.Donor.dto;
using donor.Data.Models;
using donor.Data.Models.dto.DonationHistory.dto;

namespace DonorAPI.Services.Mapper
{
    public class MapperService : Profile
    {
        public MapperService()
        {
            CreateMap<DonorAdditionDto, Donor>();
            CreateMap<DonationHistoryDto, DonationHistory>();

        }
    }
}
