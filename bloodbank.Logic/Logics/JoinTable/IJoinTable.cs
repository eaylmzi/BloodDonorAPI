using bloodbank.Data.Models;
using bloodbank.Data.Models.dto.BloodRequest.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodbank.Logic.Logics.JoinTable
{
    public interface IJoinTable
    {
        public List<BloodRequest> CheckBloodRequestByJoinTable();
        public List<GeopointDto> BranchGeopointListByJoinTable();
        public List<IdDto> AllBranchListByJoinTable();
        public List<BloodRequest> AllBloodRequestListByJoinTable();
    }
}
