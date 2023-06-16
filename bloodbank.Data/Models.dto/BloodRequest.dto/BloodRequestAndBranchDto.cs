using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodbank.Data.Models.dto.BloodRequest.dto
{
    public class BloodRequestAndBranchDto
    {
        public int BloodCount { get; set; }
        public string BloodType { get; set; } = null!;
        public DateTime DurationTime { get; set; }
        public string City { get; set; } = null!;
        public string Town { get; set; } = null!;
        public int GeopointId { get; set; } 
        public string Email { get; set; } = null!;
    }
}
