
using bloodbank.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodbank.Logic.Logics.Hospitals
{
    public interface IHospitalLogic
    {
        
        public bool Add(Hospital entity);
        public int AddAndGetId(Hospital entity);
        public bool Delete(int id);
        public bool DeleteSingleByMethod(int id);
        //public bool DeleteSingleByMethods(int id,string name);
        public bool DeleteList(int id);
        public Hospital? GetSingle(int id);
        public Hospital? GetSingleByMethod(int id);
        public Hospital? GetSingleByMethod(string name);
        //public Flight? GetSingleByMethods(int id,string name);
        public List<Hospital>? GetList(int id);
        public Task<Hospital?> UpdateAsync(int id, Hospital updatedEntity);
        public Task<Hospital?> UpdateAsync(Hospital entity, Hospital updatedEntity);
        
    }
}
