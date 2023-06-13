using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace donor.Data.Models.dto.Donor.dto
{
    public class DonorAdditionDto
    {
        public int BranchId { get; set; } 
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string BloodType { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Town { get; set; } = null!;
    }
}
