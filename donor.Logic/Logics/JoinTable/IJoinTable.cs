using donor.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace donor.Logic.Logics.JoinTable
{
    public interface IJoinTable
    {
        public List<Donor> FindDonorByJoinTable(int branchId, string name, string surname);
        public List<DonationHistory> CheckDonationListByJoinTable();
        public List<Donor> GetDonorListByJoinTable(int id);
    }
}
