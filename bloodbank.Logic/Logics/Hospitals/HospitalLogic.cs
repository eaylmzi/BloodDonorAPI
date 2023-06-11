using bloodbank.Data;
using bloodbank.Data.Repositories.Hospitals;
using BloodBankAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodbank.Logic.Logics.Hospitals
{
    public class HospitalLogic: IHospitalLogic
    {
        
        private IHospitalRepository _repository;
        public HospitalLogic(IHospitalRepository repository)
        {
            _repository = repository;
        }
        public bool Add(Hospital entity)
        {
            bool addResult = _repository.Add(entity);
            return addResult;
        }
        public int AddAndGetId(Hospital entity)
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
            Func<Hospital, bool> filter = filter => filter.Id == id;
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
            Func<Hospital, bool> filter = filter => filter.Id == id;
            bool deleteResult = _repository.DeleteList(filter);
            return deleteResult;
        }
        public Hospital? GetSingle(int id)
        {
            Hospital? result = _repository.GetSingle(id);
            return result;
        }
        public Hospital? GetSingleByMethod(int id)
        {
            Func<Hospital, bool> filter = filter => filter.Id == id;
            Hospital? result = _repository.GetSingleByMethod(filter);
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
        
        public List<Hospital>? GetList(int id)
        {
            Func<Hospital, bool> filter = filter => filter.Id == id;
            var list = _repository.GetList(filter);
            return list;
        }

      

        public async Task<Hospital?> UpdateAsync(int id, Hospital updatedEntity)
        {
            Func<Hospital, bool> filter = filter => filter.Id == id;
            Hospital? updateResult = await _repository.UpdateAsync(filter, updatedEntity);
            return updateResult;
        }
        public async Task<Hospital?> UpdateAsync(Hospital entity, Hospital updatedEntity)
        {
            Hospital? updateResult = await _repository.UpdateAsync(entity, updatedEntity);
            return updateResult;
        }
        
    }

}
