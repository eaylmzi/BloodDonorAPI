using AutoMapper;
using donor.Data.Models;
using donor.Logic.Logics.Brances;
using donor.Logic.Logics.DonationHistories;
using donor.Logic.Logics.Donors;
using DonorAPI.Services.Security;
using LocationAPI.Services.Locations;

namespace DonorAPI.Services.Donors
{
    public class DonorService : IDonorService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IDonorLogic _donorLogic;
        private readonly IBranchLogic _branchLogic;
        private readonly ISecurityService _securityService;
        private readonly IDonationHistoryLogic _donationHistoryLogic;
        private readonly ILocationService _locationService;


        public DonorService(IMapper mapper, IConfiguration configuration, IDonorLogic donorLogic, ISecurityService securityService, ILocationService locationService, IBranchLogic branchLogic, IDonationHistoryLogic donationHistoryLogic)

        {
            _mapper = mapper;
            _configuration = configuration;
            _donorLogic = donorLogic;
            _securityService = securityService;
            _locationService = locationService;
            _branchLogic = branchLogic;
            _donationHistoryLogic = donationHistoryLogic;
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
    }
}
