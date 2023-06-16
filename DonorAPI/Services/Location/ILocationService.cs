namespace DonorAPI.Services.Location
{
    public interface ILocationService
    {
        public int? GetCityIfNotExistCreateAndGet(string name);
        public int? GetTownIfNotExistCreateAndGet(string name);
    }
}
