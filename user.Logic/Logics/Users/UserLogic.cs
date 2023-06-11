using BloodBankAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using user.Data.Repositories.Users;

namespace user.Logic.Logics.Users
{
    public class UserLogic : IUserLogic
    {
        private IUserRepository _repository;
        public UserLogic(IUserRepository repository)
        {
            _repository = repository;
        }
        public bool Add(User entity)
        {
            bool addResult = _repository.Add(entity);
            return addResult;
        }
        public int AddAndGetId(User entity)
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
            Func<User, bool> filter = filter => filter.Id == id;
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
            Func<User, bool> filter = filter => filter.Id == id;
            bool deleteResult = _repository.DeleteList(filter);
            return deleteResult;
        }
        public User? GetSingle(int id)
        {
            User? result = _repository.GetSingle(id);
            return result;
        }
        public User? GetSingleByMethod(int id)
        {
            Func<User, bool> filter = filter => filter.Id == id;
            User? result = _repository.GetSingleByMethod(filter);
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
        public List<User>? GetList(int id)
        {
            Func<User, bool> filter = filter => filter.Id == id;
            var list = _repository.GetList(filter);
            return list;
        }
        public async Task<User?> UpdateAsync(int id, User updatedEntity)
        {
            Func<User, bool> filter = filter => filter.Id == id;
            User? updateResult = await _repository.UpdateAsync(filter, updatedEntity);
            return updateResult;
        }
        public async Task<User?> UpdateAsync(User entity, User updatedEntity)
        {
            User? updateResult = await _repository.UpdateAsync(entity, updatedEntity);
            return updateResult;
        }
    }
}
