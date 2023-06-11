using System;
using System.Collections.Generic;

namespace BloodBankAPI.Models
{
    public partial class Branch
    {
        public int Id { get; set; }
        public int City { get; set; }
        public int Town { get; set; }
        public int APlusBloodUnit { get; set; }
        public int AMinusBloodUnit { get; set; }
        public int BPlusBloodUnit { get; set; }
        public int BMinusBloodUnit { get; set; }
        public int AbPlusBloodUnit { get; set; }
        public int AbMinusBloodUnit { get; set; }
        public int ZeroPlusBloodUnit { get; set; }
        public int ZeroMinusBloodUnit { get; set; }
        public int GeopointId { get; set; }
    }
}
