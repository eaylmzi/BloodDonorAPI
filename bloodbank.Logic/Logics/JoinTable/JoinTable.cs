using bloodbank.Data.Models;
using bloodbank.Data.Models.dto.BloodRequest.dto;
using donor.Data.Models;
using location.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodbank.Logic.Logics.JoinTable
{
    public class JoinTable : IJoinTable
    {
        BloodBankDBContext _context = new BloodBankDBContext();

        private DbSet<BloodRequest> BloodRequestTable { get; set; }







        public JoinTable()
        {
            BloodRequestTable = _context.Set<BloodRequest>();


        }

        public List<BloodRequest> CheckBloodRequestByJoinTable()

        {

            var result = (from bloodRequestTable in BloodRequestTable

                          where bloodRequestTable.DurationTime > DateTime.Now

                          select new BloodRequest
                          {
                              Id = bloodRequestTable.Id,
                              BloodCount = bloodRequestTable.BloodCount,
                              BloodType = bloodRequestTable.BloodType,
                              DurationTime = bloodRequestTable.DurationTime,
                              Town = bloodRequestTable.Town,
                              City = bloodRequestTable.City,
                              HospitalLongitude = bloodRequestTable.HospitalLongitude,
                              HospitalLatitude = bloodRequestTable.HospitalLatitude,
                              HospitalEmail = bloodRequestTable.HospitalEmail,
                          }).ToList();

            return result;


        }
        public List<IdDto> AllBranchListByJoinTable()

        {
            using (var _donorDBContext = new DonorDBContext())
            {
                var result = (from branchTable in _donorDBContext.Set<Branch>()
                    
                              select new IdDto
                              {
                                  Id = branchTable.GeopointId,
                              }).ToList();

                return result;
            }


        }
        public List<BloodRequest> AllBloodRequestListByJoinTable()

        {
            using (var _bloodBankDBContext = new BloodBankDBContext())
            {
                var result = (from bloodTable in _bloodBankDBContext.Set<BloodRequest>()

                              select new BloodRequest
                              {
                                  Id = bloodTable.Id,
                                  BloodCount = bloodTable.BloodCount,
                                  BloodType = bloodTable.BloodType,
                                  City = bloodTable.City,
                                  Town = bloodTable.Town,
                                  HospitalLatitude = bloodTable.HospitalLatitude,
                                  HospitalLongitude = bloodTable.HospitalLongitude,
                                  DurationTime = bloodTable.DurationTime,
                                  HospitalEmail = bloodTable.HospitalEmail

                              }).ToList();

                return result;
            }


        }
        public List<GeopointDto> BranchGeopointListByJoinTable()

        {

          
            using (var _donorDBContext = new DonorDBContext())
            using (var _locationDBContext = new LocationDBContext())
            {
                var result = (from branchTable in _donorDBContext.Set<Branch>()
                join locationTable in _locationDBContext.Set<Geopoint>() on branchTable.GeopointId equals locationTable.Id
                              select new GeopointDto
                              {
                                  Latitude = locationTable.Latitude,
                                  Longitude = locationTable.Longitude,
                              }).ToList();

                return result;
            }


        }
    }
}
