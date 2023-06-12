using bloodbank.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace location.logic.Logics.Cities
{
    public interface ICityLogic
    {
        public bool Add(City entity);
        public int AddAndGetId(City entity);
        public bool Delete(int id);
        public bool DeleteSingleByMethod(int id);

        // public bool DeleteSingleByMethods(int id,string name);
        public bool DeleteList(int id);
        public City? GetSingle(int id);
        public City? GetSingleByMethod(int id);
        //public Flight? GetSingleByMethods(int id,string name);
        public List<City>? GetList(int id);
        public Task<City?> UpdateAsync(int id, City updatedEntity);
        public Task<City?> UpdateAsync(City entity, City updatedEntity);
    }
}
