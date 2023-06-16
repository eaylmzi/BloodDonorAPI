using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodbank.Data.Models
{
    public class BloodRequest
    {
        public int Id { get; set; }
        public int BloodCount { get; set; }
        public string BloodType { get; set; } = null!;
        public DateTime DurationTime { get; set; }
        public string City { get; set; } = null!;
        public string Town { get; set; } = null!;
        public double HospitalLongitude { get; set; }
        public double HospitalLatitude { get; set; }
        public string HospitalEmail { get; set; } = null!;

    }
}
