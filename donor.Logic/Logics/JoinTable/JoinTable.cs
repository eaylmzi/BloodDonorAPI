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
        private DbSet<DonationHistory> DonationHistoryTable { get; set; }






        public JoinTable()
        {
            DonorTable = _context.Set<Donor>();
            BranchTable = _context.Set<Branch>();
            DonationHistoryTable = _context.Set<DonationHistory>();
        }
        
        public List<Donor> FindDonorByJoinTable(int branchId, string name, string surname)

        {

            var result = (from donorTable in DonorTable
                          join branchTable in BranchTable on donorTable.BranchId equals branchTable.Id

                          where donorTable.Name == name && donorTable.Surname == surname 

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
        public List<DonationHistory> CheckDonationListByJoinTable()

        {

            var result = (from donationHistoryTable in DonationHistoryTable

                          where (donationHistoryTable.DonationTime - DateTime.Now).TotalSeconds > 0 

                          select new DonationHistory
                          {
                            Id = donationHistoryTable.Id,
                            DonorId = donationHistoryTable.DonorId,
                            TupleCount = donationHistoryTable.TupleCount,
                            DonationTime  = donationHistoryTable.DonationTime,
                          }).ToList();

            return result;


        }
        public List<Donor> GetDonorListByJoinTable(int id)

        {

            var result = (from donorTable in DonorTable

                          where donorTable.BranchId == id

                          select new Donor
                          {
                              Id = donorTable.Id,
                              BranchId = donorTable.BranchId,
                              BloodType = donorTable.BloodType,
                              City = donorTable.City,
                              Town = donorTable.Town,
                              Name = donorTable.Name,
                              Surname = donorTable.Surname,
                              Phone = donorTable.Phone,
                              
                          }).ToList();

            return result;


        }

    }
}
