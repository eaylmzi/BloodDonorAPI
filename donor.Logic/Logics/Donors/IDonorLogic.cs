using BloodBankAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace donor.Logic.Logics.Donors
{
    public interface IDonorLogic
    {
        public bool Add(Donor entity);
        public int AddAndGetId(Donor entity);
        public bool Delete(int id);
        public bool DeleteSingleByMethod(int id);

        // public bool DeleteSingleByMethods(int id,string name);
        public bool DeleteList(int id);
        public Donor? GetSingle(int id);
        public Donor? GetSingleByMethod(int id);
        //public Flight? GetSingleByMethods(int id,string name);
        public List<Donor>? GetList(int id);
        public Task<Donor?> UpdateAsync(int id, Donor updatedEntity);
        public Task<Donor?> UpdateAsync(Donor entity, Donor updatedEntity);
    }
}
