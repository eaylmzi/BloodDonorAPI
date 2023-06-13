using bloodbank.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodbank.Logic.Logics.BloodRequests
{
    public interface IBloodRequestLogic
    {
        public bool Add(BloodRequest entity);

        public int AddAndGetId(BloodRequest entity);

        public bool Delete(int id);

        public bool DeleteSingleByMethod(int id);

      
        //public bool DeleteSingleByMethods(int id,string name);

        public bool DeleteList(int id);

        public BloodRequest? GetSingle(int id);

        public BloodRequest? GetSingleByMethod(int id);

        //public Flight? GetSingleByMethods(int id,string name);

        public List<BloodRequest>? GetList(int id);

        public Task<BloodRequest?> UpdateAsync(int id, BloodRequest updatedEntity);

        public Task<BloodRequest?> UpdateAsync(BloodRequest entity, BloodRequest updatedEntity);

    }
}
