﻿using bloodbank.Data.Models;
using donor.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user.Data.Models.dto
{
    public class UserAndHospitalDto
    {
        public User User { get; set; } = null!;
        public Hospital Hospital { get; set; } = null!;
    }
}
