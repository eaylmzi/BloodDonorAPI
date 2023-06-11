using BloodBankAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace donor.Logic.Logics.Brances
{
    public interface IBranchLogic
    {
        public bool Add(Branch entity);
        public int AddAndGetId(Branch entity);
        public bool Delete(int id);
        public bool DeleteSingleByMethod(int id);
        //public bool DeleteSingleByMethods(int id, string name);
        public bool DeleteList(int id);
        public Branch? GetSingle(int id);
        public Branch? GetSingleByMethod(int id);
        // public Flight? GetSingleByMethods(int id, string name);
        public List<Branch>? GetList(int id);
        public Task<Branch?> UpdateAsync(int id, Branch updatedEntity);
        public Task<Branch?> UpdateAsync(Branch entity, Branch updatedEntity);
    }
}
