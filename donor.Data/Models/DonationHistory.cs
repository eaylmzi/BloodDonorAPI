using System;
using System.Collections.Generic;

namespace donor.Data.Models
{
    public partial class DonationHistory
    {
        public int Id { get; set; }
        public int DonorId { get; set; }
        public int TupleCount { get; set; }
        public byte[] DonationTime { get; set; } = null!;
    }
}
