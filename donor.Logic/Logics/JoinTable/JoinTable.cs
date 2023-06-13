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

namespace donor.Logic.Logics.JoinTable
{
    public class JoinTable : IJoinTable
    {
        DonorDBContext _context = new DonorDBContext();
        private DbSet<Donor> DonorTable { get; set; }
        private DbSet<Branch> BranchTable { get; set; }






        public JoinTable()
        {
            DonorTable = _context.Set<Donor>();
            BranchTable = _context.Set<Branch>();
        }
        
        public List<Donor> FindDonorByJoinTable(int branchId, string name, string surname, string phoneNumber)

        {

            var result = (from donorTable in DonorTable
                          join branchTable in BranchTable on donorTable.BranchId equals branchTable.Id

                          where donorTable.Name == name && donorTable.Surname == surname && donorTable.Phone == phoneNumber

                          select new Donor
                          {
                              Id = donorTable.Id,
                              BranchId = donorTable.BranchId,
                              Name = donorTable.Name,
                              Surname = donorTable.Surname,
                              BloodType = donorTable.BloodType,
                              Phone = donorTable.Phone,
                              City = donorTable.City,
                              Town = donorTable.Town
                          }).ToList();

            return result;


        }
        
    }
}
