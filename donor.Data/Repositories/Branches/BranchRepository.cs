using bloodbank.Data.Repository.RepositoryBase;
using donor.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace donor.Data.Repositories.Branches
{
    public class BranchRepository : RepositoryBase<Branch>, IBranchRepository
    {
        DonorDBContext _context = new DonorDBContext();

        private DbSet<Branch> query { get; set; }
        public BranchRepository()
        {
            query = _context.Set<Branch>();
        }
        public bool CheckBranchIfExists(int branchId)
        {
            bool isExist = query.Any(p => p.Id == branchId);
            if (isExist)
            {
                return true;
            }
            return false;
        }
    }
}
