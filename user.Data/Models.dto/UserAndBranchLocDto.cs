using donor.Data.Models;
using donor.Data.Models.dto.Branch.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user.Data.Models.dto
{
    public class UserAndBranchLocDto
    {
  
            public User User { get; set; } = null!;
            public BranchDto Branch { get; set; } = null!;
        
    }
}
