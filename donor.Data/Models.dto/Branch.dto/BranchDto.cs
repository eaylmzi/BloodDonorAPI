using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace donor.Data.Models.dto.Branch.dto
{
    public class BranchDto
    {
        public int Id { get; set; }
        public string City { get; set; } = null!;
        public string Town { get; set; } = null!;
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
