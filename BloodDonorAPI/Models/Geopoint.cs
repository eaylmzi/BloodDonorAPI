﻿using System;
using System.Collections.Generic;

namespace BloodDonorApiGateway.Models
{
    public partial class Geopoint
    {
        public int Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
