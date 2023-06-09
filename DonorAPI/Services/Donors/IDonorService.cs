﻿using bloodbank.Data.Models;
using donor.Data.Models;

namespace DonorAPI.Services.Donors
{
    public interface IDonorService
    {
        public Task<bool> UpdateBranchBloodCount(Branch branch, string bloodType, int bloodCount);
        public Task<bool> UndoUpdateBranchBloodCount(Branch branch, string bloodType, int bloodCount);
        public bool HasBlood(Branch branch, string bloodType, int bloodCount);
        public Task<bool> UpdateHospitalBloodCount(Hospital hospital, string bloodType, int bloodCount);
        public Task<bool> UndoUpdateHospitalBloodCount(Hospital hospital, string bloodType, int bloodCount);
    }
}
