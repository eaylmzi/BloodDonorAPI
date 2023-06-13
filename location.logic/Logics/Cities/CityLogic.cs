using location.Data.Models;
using location.Data.Repositories.Cities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace location.logic.Logics.Cities
{
    public class CityLogic : ICityLogic
    {
        private ICityRepository _repository;
        public CityLogic(ICityRepository repository)
        {
            _repository = repository;
        }
        public bool Add(City entity)
        {
            bool addResult = _repository.Add(entity);
            return addResult;
        }
        public int AddAndGetId(City entity)
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
            Func<City, bool> filter = filter => filter.Id == id;
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
            Func<City, bool> filter = filter => filter.Id == id;
            bool deleteResult = _repository.DeleteList(filter);
            return deleteResult;
        }
        public City? GetSingle(int id)
        {
            City? result = _repository.GetSingle(id);
            return result;
        }
        public City? GetSingleByMethod(int id)
        {
            Func<City, bool> filter = filter => filter.Id == id;
            City? result = _repository.GetSingleByMethod(filter);
            return result;
        }
        public City? GetSingleByMethod(string name)
        {
            Func<City, bool> filter = filter => filter.Name == name;
            City? result = _repository.GetSingleByMethod(filter);
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
        public List<City>? GetList(int id)
        {
            Func<City, bool> filter = filter => filter.Id == id;
            var list = _repository.GetList(filter);
            return list;
        }
        public async Task<City?> UpdateAsync(int id, City updatedEntity)
        {
            Func<City, bool> filter = filter => filter.Id == id;
            City? updateResult = await _repository.UpdateAsync(filter, updatedEntity);
            return updateResult;
        }
        public async Task<City?> UpdateAsync(City entity, City updatedEntity)
        {
            City? updateResult = await _repository.UpdateAsync(entity, updatedEntity);
            return updateResult;
        }
    }
}
