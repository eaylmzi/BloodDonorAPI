using AutoMapper;
using bloodbank.Data.Models;
using bloodbank.Logic.Logics.Hospitals;
using donor.Data.Models;
using donor.Logic.Logics.Brances;
using donor.Logic.Logics.DonationHistories;
using donor.Logic.Logics.Donors;
using DonorAPI.Services.Location;

namespace DonorAPI.Services.Donors
{
    public class DonorService : IDonorService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IBranchLogic _branchLogic;
        private readonly IDonationHistoryLogic _donationHistoryLogic;
        private readonly ILocationService _locationService;
        private readonly IHospitalLogic _hospitalLogic;


        public DonorService(IMapper mapper, IConfiguration configuration, ILocationService locationService, IBranchLogic branchLogic, IDonationHistoryLogic donationHistoryLogic, IHospitalLogic hospitalLogic)

        {
            _mapper = mapper;
            _configuration = configuration;
            _locationService = locationService;
            _branchLogic = branchLogic;
            _donationHistoryLogic = donationHistoryLogic;
            _hospitalLogic = hospitalLogic;
        }
        public async Task<bool> UpdateBranchBloodCount(Branch branch ,string bloodType, int bloodCount)
        {
           
            if (bloodType == "A+")
            {
                branch.APlusBloodUnit = branch.APlusBloodUnit + bloodCount;
                Branch? updatedBranch = await _branchLogic.UpdateAsync(branch.Id, branch);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "A-")
            {
                branch.AMinusBloodUnit = branch.AMinusBloodUnit + bloodCount;
                Branch? updatedBranch = await _branchLogic.UpdateAsync(branch.Id, branch);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "B+")
            {
                branch.BPlusBloodUnit = branch.BPlusBloodUnit + bloodCount;
                Branch? updatedBranch = await _branchLogic.UpdateAsync(branch.Id, branch);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "B-")
            {
                branch.BMinusBloodUnit = branch.BMinusBloodUnit + bloodCount;
                Branch? updatedBranch = await _branchLogic.UpdateAsync(branch.Id, branch);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "AB+")
            {
                branch.AbPlusBloodUnit = branch.AbPlusBloodUnit + bloodCount;
                Branch? updatedBranch = await _branchLogic.UpdateAsync(branch.Id, branch);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "AB-")
            {
                branch.AbMinusBloodUnit = branch.AbMinusBloodUnit + bloodCount;
                Branch? updatedBranch = await _branchLogic.UpdateAsync(branch.Id, branch);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "O+")
            {
                branch.ZeroPlusBloodUnit = branch.ZeroPlusBloodUnit + bloodCount;
                Branch? updatedBranch = await _branchLogic.UpdateAsync(branch.Id, branch);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "O-")
            {
                branch.ZeroMinusBloodUnit = branch.ZeroMinusBloodUnit + bloodCount;
                Branch? updatedBranch = await _branchLogic.UpdateAsync(branch.Id, branch);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
           
        }
        public bool HasBlood(Branch branch, string bloodType, int bloodCount)
        {

            if (bloodType == "A+")
            {
                if (branch.APlusBloodUnit < bloodCount)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "A-")
            {
                if (branch.AMinusBloodUnit < bloodCount)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "B+")
            {
                if (branch.BPlusBloodUnit < bloodCount)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "B-")
            {
                if (branch.BMinusBloodUnit < bloodCount)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "AB+")
            {
                if (branch.AbPlusBloodUnit < bloodCount)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "AB-")
            {
                if (branch.AbMinusBloodUnit < bloodCount)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "O+")
            {
                if (branch.ZeroPlusBloodUnit < bloodCount)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "O-")
            {
                if (branch.ZeroMinusBloodUnit < bloodCount)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<bool> UndoUpdateBranchBloodCount(Branch branch, string bloodType, int bloodCount)
        {

            if (bloodType == "A+")
            {
                branch.APlusBloodUnit = branch.APlusBloodUnit - bloodCount;
                Branch? updatedBranch = await _branchLogic.UpdateAsync(branch.Id, branch);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "A-")
            {
                branch.AMinusBloodUnit = branch.AMinusBloodUnit - bloodCount;
                Branch? updatedBranch = await _branchLogic.UpdateAsync(branch.Id, branch);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "B+")
            {
                branch.BPlusBloodUnit = branch.BPlusBloodUnit - bloodCount;
                Branch? updatedBranch = await _branchLogic.UpdateAsync(branch.Id, branch);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "B-")
            {
                branch.BMinusBloodUnit = branch.BMinusBloodUnit - bloodCount;
                Branch? updatedBranch = await _branchLogic.UpdateAsync(branch.Id, branch);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "AB+")
            {
                branch.AbPlusBloodUnit = branch.AbPlusBloodUnit - bloodCount;
                Branch? updatedBranch = await _branchLogic.UpdateAsync(branch.Id, branch);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "AB-")
            {
                branch.AbMinusBloodUnit = branch.AbMinusBloodUnit - bloodCount;
                Branch? updatedBranch = await _branchLogic.UpdateAsync(branch.Id, branch);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "O+")
            {
                branch.ZeroPlusBloodUnit = branch.ZeroPlusBloodUnit - bloodCount;
                Branch? updatedBranch = await _branchLogic.UpdateAsync(branch.Id, branch);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "O-")
            {
                branch.ZeroMinusBloodUnit = branch.ZeroMinusBloodUnit - bloodCount;
                Branch? updatedBranch = await _branchLogic.UpdateAsync(branch.Id, branch);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<bool> UpdateHospitalBloodCount(Hospital hospital, string bloodType, int bloodCount)
        {

            if (bloodType == "A+")
            {
                hospital.APlusBloodUnit = hospital.APlusBloodUnit + bloodCount;
                Hospital? updatedBranch = await _hospitalLogic.UpdateAsync(hospital.Id, hospital);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "A-")
            {
                hospital.AMinusBloodUnit = hospital.AMinusBloodUnit + bloodCount;
                Hospital? updatedBranch = await _hospitalLogic.UpdateAsync(hospital.Id, hospital);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "B+")
            {
                hospital.BPlusBloodUnit = hospital.BPlusBloodUnit + bloodCount;
                Hospital? updatedBranch = await _hospitalLogic.UpdateAsync(hospital.Id, hospital);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "B-")
            {
                hospital.BMinusBloodUnit = hospital.BMinusBloodUnit + bloodCount;
                Hospital? updatedBranch = await _hospitalLogic.UpdateAsync(hospital.Id, hospital);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "AB+")
            {
                hospital.AbPlusBloodUnit = hospital.AbPlusBloodUnit + bloodCount;
                Hospital? updatedBranch = await _hospitalLogic.UpdateAsync(hospital.Id, hospital);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "AB-")
            {
                hospital.AbMinusBloodUnit = hospital.AbMinusBloodUnit + bloodCount;
                Hospital? updatedBranch = await _hospitalLogic.UpdateAsync(hospital.Id, hospital);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "O+")
            {
                hospital.ZeroPlusBloodUnit = hospital.ZeroPlusBloodUnit + bloodCount;
                Hospital? updatedBranch = await _hospitalLogic.UpdateAsync(hospital.Id, hospital);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "O-")
            {
                hospital.ZeroMinusBloodUnit = hospital.ZeroMinusBloodUnit + bloodCount;
                Hospital? updatedBranch = await _hospitalLogic.UpdateAsync(hospital.Id, hospital);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<bool> UndoUpdateHospitalBloodCount(Hospital hospital, string bloodType, int bloodCount)
        {

            if (bloodType == "A+")
            {
                hospital.APlusBloodUnit = hospital.APlusBloodUnit - bloodCount;
                Hospital? updatedBranch = await _hospitalLogic.UpdateAsync(hospital.Id, hospital);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "A-")
            {
                hospital.AMinusBloodUnit = hospital.AMinusBloodUnit - bloodCount;
                Hospital? updatedBranch = await _hospitalLogic.UpdateAsync(hospital.Id, hospital);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "B+")
            {
                hospital.BPlusBloodUnit = hospital.BPlusBloodUnit - bloodCount;
                Hospital? updatedBranch = await _hospitalLogic.UpdateAsync(hospital.Id, hospital);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "B-")
            {
                hospital.BMinusBloodUnit = hospital.BMinusBloodUnit - bloodCount;
                Hospital? updatedBranch = await _hospitalLogic.UpdateAsync(hospital.Id, hospital);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "AB+")
            {
                hospital.AbPlusBloodUnit = hospital.AbPlusBloodUnit - bloodCount;
                Hospital? updatedBranch = await _hospitalLogic.UpdateAsync(hospital.Id, hospital);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "AB-")
            {
                hospital.AbMinusBloodUnit = hospital.AbMinusBloodUnit - bloodCount;
                Hospital? updatedBranch = await _hospitalLogic.UpdateAsync(hospital.Id, hospital);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "O+")
            {
                hospital.ZeroPlusBloodUnit = hospital.ZeroPlusBloodUnit - bloodCount;
                Hospital? updatedBranch = await _hospitalLogic.UpdateAsync(hospital.Id, hospital);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else if (bloodType == "O-")
            {
                hospital.ZeroMinusBloodUnit = hospital.ZeroMinusBloodUnit - bloodCount;
                Hospital? updatedBranch = await _hospitalLogic.UpdateAsync(hospital.Id, hospital);
                if (updatedBranch == null)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
