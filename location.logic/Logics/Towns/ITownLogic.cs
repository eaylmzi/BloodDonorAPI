using location.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace location.logic.Logics.Towns
{
    public interface ITownLogic
    {
        public bool Add(Town entity);
        public int AddAndGetId(Town entity);
        public bool Delete(int id);
        public bool DeleteSingleByMethod(int id);
        public Town? GetSingleByMethod(string name);


        // public bool DeleteSingleByMethods(int id,string name);
        public bool DeleteList(int id);
        public Town? GetSingle(int id);
        public Town? GetSingleByMethod(int id);
        //public Flight? GetSingleByMethods(int id,string name);
        public List<Town>? GetList(int id);
        public Task<Town?> UpdateAsync(int id, Town updatedEntity);
        public Task<Town?> UpdateAsync(Town entity, Town updatedEntity);
    }
}
