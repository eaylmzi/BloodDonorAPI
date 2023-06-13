using bloodbank.Data.Models;
using bloodbank.Data.Repositories.BloodRequests;
using bloodbank.Data.Repositories.Hospitals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodbank.Logic.Logics.BloodRequests
{
    public class BloodRequestLogic : IBloodRequestLogic
    {
        private IBloodRequestRepository _repository;
        public BloodRequestLogic(IBloodRequestRepository repository)
        {
            _repository = repository;
        }
        public bool Add(BloodRequest entity)
        {
            bool addResult = _repository.Add(entity);
            return addResult;
        }
        public int AddAndGetId(BloodRequest entity)
        {
            int addResult = _repository.AddAndGetId(entity);
            return addResult;
        }
        public bool Delete(int id)
        {
            bool deleteResult = _repository.Delete(id);
            return deleteResult;
        }
        public bool DeleteSingleByMethod(int id)
        {
            Func<BloodRequest, bool> filter = filter => filter.Id == id;
            bool deleteResult = _repository.DeleteSingleByMethod(filter);
            return deleteResult;
        }
        /*
         * istediğin gibi Func parametrelerini değiştirebilirsin
        public bool DeleteSingleByMethods(int id,string name)
        {
            Func<Flight, bool> filter = filter => filter.Id == id;
            Func<Flight, bool> filter = filter => filter.Name == name;
            bool deleteResult = _repository.DeleteSingleByMethod(filter);
            return deleteResult;
        }
        */

        public bool DeleteList(int id)
        {
            Func<BloodRequest, bool> filter = filter => filter.Id == id;
            bool deleteResult = _repository.DeleteList(filter);
            return deleteResult;
        }
        public BloodRequest? GetSingle(int id)
        {
            BloodRequest? result = _repository.GetSingle(id);
            return result;
        }
        public BloodRequest? GetSingleByMethod(int id)
        {
            Func<BloodRequest, bool> filter = filter => filter.Id == id;
            BloodRequest? result = _repository.GetSingleByMethod(filter);
            return result;
        }

        /*
     * istediğin gibi Func parametrelerini değiştirebilirsin
    public Flight? GetSingleByMethods(int id,string name)
    {
        Func<Flight, bool> filter = filter => filter.Id == id;
        Func<Flight, bool> filter = filter => filter.Name == name;
        Flight result = _repository.GetSingleByMethod(filter);
        return result;
    }
    */

        public List<BloodRequest>? GetList(int id)
        {
            Func<BloodRequest, bool> filter = filter => filter.Id == id;
            var list = _repository.GetList(filter);
            return list;
        }



        public async Task<BloodRequest?> UpdateAsync(int id, BloodRequest updatedEntity)
        {
            Func<BloodRequest, bool> filter = filter => filter.Id == id;
            BloodRequest? updateResult = await _repository.UpdateAsync(filter, updatedEntity);
            return updateResult;
        }
        public async Task<BloodRequest?> UpdateAsync(BloodRequest entity, BloodRequest updatedEntity)
        {
            BloodRequest? updateResult = await _repository.UpdateAsync(entity, updatedEntity);
            return updateResult;
        }
    }
}
