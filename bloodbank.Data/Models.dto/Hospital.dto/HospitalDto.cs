using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodbank.Data.Models.dto.Hospital.dto
{
    public class HospitalDto
    {
        public string Name { get; set; } = null!;
        public int APlusBloodUnit { get; set; }
        public int AMinusBloodUnit { get; set; }
        public int BPlusBloodUnit { get; set; }
        public int BMinusBloodUnit { get; set; }
        public int AbPlusBloodUnit { get; set; }
        public int AbMinusBloodUnit { get; set; }
        public int ZeroPlusBloodUnit { get; set; }
        public int ZeroMinusBloodUnit { get; set; }
    }
}
