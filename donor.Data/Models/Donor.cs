using System;
using System.Collections.Generic;

namespace BloodBankAPI.Models
{
    public partial class Donor
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string BloodType { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public int CityId { get; set; }
        public int TownId { get; set; }
    }
}
