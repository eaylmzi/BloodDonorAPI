using location.Data.Models;

namespace LocationAPI.Services.Locations
{
    public interface ILocationService
    {
        public int? GetCityIfNotExistCreateAndGet(string name);
        public int? GetTownIfNotExistCreateAndGet(string name);
    }
}
