using AutoMapper;
using location.Data.Models;
using location.logic.Logics.Cities;
using location.logic.Logics.Towns;

namespace DonorAPI.Services.Location
{
    public class LocationService : ILocationService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ITownLogic _townLogic;
        private readonly ICityLogic _cityLogic;



        public LocationService(IMapper mapper, IConfiguration configuration, ITownLogic townLogic, ICityLogic cityLogic)
        {
            _mapper = mapper;
            _configuration = configuration;
            _townLogic = townLogic;
            _cityLogic = cityLogic;
        }

        public int? GetCityIfNotExistCreateAndGet(string name)
        {
            City? city = _cityLogic.GetSingleByMethod(name);
            if (city == null)
            {
                int cityId = _cityLogic.AddAndGetId(new City() {Name = name });
                if (cityId > 0)
                {
                    return cityId;
                }
                return null;
            }
            else
            {
                return city.Id;
            }
        }
        public int? GetTownIfNotExistCreateAndGet(string name)
        {
            Town? town = _townLogic.GetSingleByMethod(name);
            if (town == null)
            {
                int townId = _townLogic.AddAndGetId(new Town() { Name = name });
                if (townId > 0)
                {
                    return townId;
                }
                return null;
            }
            else
            {
                return town.Id;
            }
        }
    }
}
