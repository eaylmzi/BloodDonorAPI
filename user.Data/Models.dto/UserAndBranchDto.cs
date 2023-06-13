using donor.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user.Data.Models.dto
{
    public class UserAndBranchDto
    {
        public User User { get; set; } = null!;
        public Branch Branch { get; set; } = null!;
    }
}
