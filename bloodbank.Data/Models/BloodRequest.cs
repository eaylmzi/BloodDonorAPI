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

    }
}
