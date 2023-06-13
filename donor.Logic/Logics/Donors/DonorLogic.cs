using donor.Data.Models;
using donor.Data.Repositories.DonationHistories;
using donor.Data.Repositories.Donors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace donor.Logic.Logics.Donors
{
    public class DonorLogic : IDonorLogic
    {
        private IDonorRepository _repository;
        public DonorLogic(IDonorRepository repository)
        {
            _repository = repository;
        }
        public bool Add(Donor entity)
        {
            bool addResult = _repository.Add(entity);
            return addResult;
        }
        public int AddAndGetId(Donor entity)
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
            Func<Donor, bool> filter = filter => filter.Id == id;
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
            Func<Donor, bool> filter = filter => filter.Id == id;
            bool deleteResult = _repository.DeleteList(filter);
            return deleteResult;
        }
        public Donor? GetSingle(int id)
        {
            Donor? result = _repository.GetSingle(id);
            return result;
        }
        public Donor? GetSingleByMethod(int id)
        {
            Func<Donor, bool> filter = filter => filter.Id == id;
            Donor? result = _repository.GetSingleByMethod(filter);
            return result;
        }



        public Donor? GetSingleByMethods(string name, string surname)
        {
            Func<Donor, bool> filter = filter => filter.Name == name;
            Func<Donor, bool> filter2 = filter => filter.Surname == surname;
            Donor? result = _repository.GetSingleByMethod(filter, filter2);
            return result;
        }
        public List<Donor>? GetList(int id)
        {
            Func<Donor, bool> filter = filter => filter.Id == id;
            var list = _repository.GetList(filter);
            return list;
        }
        public async Task<Donor?> UpdateAsync(int id, Donor updatedEntity)
        {
            Func<Donor, bool> filter = filter => filter.Id == id;
            Donor? updateResult = await _repository.UpdateAsync(filter, updatedEntity);
            return updateResult;
        }
        public async Task<Donor?> UpdateAsync(Donor entity, Donor updatedEntity)
        {
            Donor? updateResult = await _repository.UpdateAsync(entity, updatedEntity);
            return updateResult;
        }

    }
}
