using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ContosoUniveristy.Core.Entities;
using ContosoUniveristy.Core.Specifications;

namespace ContosoUniveristy.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(object id);
        Task<IEnumerable<T>> Find(Specification<T> specification, int page = 0, int pageSize = 0);
        Task<IEnumerable<T>> GetAll();
        Task Insert(T obj);
        Task Update(T obj);
        Task Delete(object id);
        Task Save();
    }
}
