using bloodbank.Data.Models.dto.BloodRequest.dto;

namespace BloodBankAPI
{
    public static class DistanceManager
    {
        public static List<GeopointDto> FindNearBranch(RequestedGeopointDto requestedGeopoint, int distance = 50)
        {
            var list = new List<GeopointDto>();



            requestedGeopoint.RequestedBloodLine.ForEach(geopoint =>
            {
                var distanceToBranch = Haversine(requestedGeopoint.HospitalGeopoint, geopoint);
                if (distanceToBranch <= distance)
                {
                    list.Add(geopoint);
                }
            });
            return list;
        }
        public static double Haversine(GeopointDto start, GeopointDto end)
        {
            const double R = 6371; // Earth radius in km
            double lat1 = start.Latitude * Math.PI / 180;
            double lat2 = end.Latitude * Math.PI / 180;
            double dLat = (end.Latitude - start.Latitude) * Math.PI / 180;
            double dLon = (end.Longitude - start.Longitude) * Math.PI / 180;
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c;
            return d;
        }
    }
}
