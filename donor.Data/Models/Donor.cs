using System;
using System.Collections.Generic;

namespace donor.Data.Models
{
    public partial class Donor
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string BloodType { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public int City { get; set; }
        public int Town { get; set; }
    }
}
