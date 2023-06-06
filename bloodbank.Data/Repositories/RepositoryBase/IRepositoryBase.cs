using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodbank.Data.Repository.RepositoryBase
{
    public interface IRepositoryBase<T> where T : class
    {
        public bool Add(T entity);        
        public int AddAndGetId(T entity);
        public bool Delete(int id);
        public bool DeleteSingleByMethod(Func<T, bool> method);
        public bool DeleteSingleByMethod(Func<T, bool> metot, Func<T, bool> metot2);
        public bool DeleteList(Func<T, bool> metot);
        public Task<T?> UpdateAsync(Func<T, bool> method, T updatedEntity);
        public Task<T?> UpdateAsync(T? entity, T updatedEntity);
        public List<T>? GetList(Func<T, bool> method);
        public T? GetSingle(int number);
        public T? GetSingleByMethod(Func<T, bool> method);
        public T? GetSingleByMethod(Func<T, bool> method, Func<T, bool> method2);

    }
}
