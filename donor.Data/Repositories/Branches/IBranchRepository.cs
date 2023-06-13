using bloodbank.Data.Repository.RepositoryBase;
using donor.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace donor.Data.Repositories.Branches
{
    public interface IBranchRepository : IRepositoryBase<Branch>
    {
        public bool CheckBranchIfExists(int branchId);
    }
}
