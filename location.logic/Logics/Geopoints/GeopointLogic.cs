using bloodbank.Logic.Models;
using location.Data.Repositories.Cities;
using location.Data.Repositories.Geopoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace location.logic.Logics.Geopoints
{
    public class GeopointLogic : IGeopointLogic
    {
        private IGeopointRepository _repository;
        public GeopointLogic(IGeopointRepository repository)
        {
            _repository = repository;
        }
        public bool Add(Geopoint entity)
        {
            bool addResult = _repository.Add(entity);
            return addResult;
        }
        public int AddAndGetId(Geopoint entity)
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
            Func<Geopoint, bool> filter = filter => filter.Id == id;
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
            Func<Geopoint, bool> filter = filter => filter.Id == id;
            bool deleteResult = _repository.DeleteList(filter);
            return deleteResult;
        }
        public Geopoint? GetSingle(int id)
        {
            Geopoint? result = _repository.GetSingle(id);
            return result;
        }
        public Geopoint? GetSingleByMethod(int id)
        {
            Func<Geopoint, bool> filter = filter => filter.Id == id;
            Geopoint? result = _repository.GetSingleByMethod(filter);
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
        public List<Geopoint>? GetList(int id)
        {
            Func<Geopoint, bool> filter = filter => filter.Id == id;
            var list = _repository.GetList(filter);
            return list;
        }
        public async Task<Geopoint?> UpdateAsync(int id, Geopoint updatedEntity)
        {
            Func<Geopoint, bool> filter = filter => filter.Id == id;
            Geopoint? updateResult = await _repository.UpdateAsync(filter, updatedEntity);
            return updateResult;
        }
        public async Task<Geopoint?> UpdateAsync(Geopoint entity, Geopoint updatedEntity)
        {
            Geopoint? updateResult = await _repository.UpdateAsync(entity, updatedEntity);
            return updateResult;
        }
    }
}
