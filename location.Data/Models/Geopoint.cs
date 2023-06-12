using System;
using System.Collections.Generic;

namespace location.Data.Models
{
    public partial class Geopoint
    {
        public int Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
