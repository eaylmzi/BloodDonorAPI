using bloodbank.Logic.Models;
using donor.Data.Repositories.Branches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// knk bak burayı oku Dependecy yanlış , interfaceleri inherit almadın logiclerin interfacesi boş theny yuo
namespace donor.Logic.Logics.Brances
{
    public class BranchLogic : IBranchLogic
    {
        private IBranchRepository _repository;
        public BranchLogic(IBranchRepository repository)
        {
            _repository = repository;
        }
        public bool Add(Branch entity)
        {
            bool addResult = _repository.Add(entity);
            return addResult;
        }
        public int AddAndGetId(Branch entity)
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
            Func<Branch, bool> filter = filter => filter.Id == id;
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
            Func<Branch, bool> filter = filter => filter.Id == id;
            bool deleteResult = _repository.DeleteList(filter);
            return deleteResult;
        }
        public Branch? GetSingle(int id)
        {
            Branch? result = _repository.GetSingle(id);
            return result;
        }
        public Branch? GetSingleByMethod(int id)
        {
            Func<Branch, bool> filter = filter => filter.Id == id;
            Branch? result = _repository.GetSingleByMethod(filter);
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
        public List<Branch>? GetList(int id)
        {
            Func<Branch, bool> filter = filter => filter.Id == id;
            var list = _repository.GetList(filter);
            return list;
        }



        public async Task<Branch?> UpdateAsync(int id, Branch updatedEntity)
        {
            Func<Branch, bool> filter = filter => filter.Id == id;
            Branch? updateResult = await _repository.UpdateAsync(filter, updatedEntity);
            return updateResult;
        }
        public async Task<Branch?> UpdateAsync(Branch entity, Branch updatedEntity)
        {
            Branch? updateResult = await _repository.UpdateAsync(entity, updatedEntity);
            return updateResult;
        }
    }
}
