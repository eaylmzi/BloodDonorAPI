using BloodBankAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user.Logic.Logics.Users
{
    public interface IUserLogic
    {
        public bool Add(User entity);
        public int AddAndGetId(User entity);
        public bool Delete(int id);
        public bool DeleteSingleByMethod(int id);

        // public bool DeleteSingleByMethods(int id,string name);
        public bool DeleteList(int id);
        public User? GetSingle(int id);
        public User? GetSingleByMethod(int id);
        //public Flight? GetSingleByMethods(int id,string name);
        public List<User>? GetList(int id);
        public Task<User?> UpdateAsync(int id, User updatedEntity);
        public Task<User?> UpdateAsync(User entity, User updatedEntity);
    }
}
