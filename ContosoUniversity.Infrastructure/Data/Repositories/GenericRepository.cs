using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContosoUniveristy.Core.Interfaces;
using ContosoUniveristy.Core.Specifications;

namespace ContosoUniversity.Infrastructure.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _table;
        public GenericRepository(DbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public virtual async Task<T> GetById(object id)
        {
            return await _table.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> Find(Specification<T> specification,
            int page = 0, int pageSize = 0)
        {
            var records = _table.Where(specification.ToExpression())
                .Skip(page * pageSize);

            return await (pageSize > 0 ? records.Take(pageSize) : records).ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _table.ToListAsync();
        }

        public virtual Task Insert(T obj)
        {
            _table.Add(obj);
            return Task.CompletedTask;
        }
        public virtual Task Update(T obj)
        {
            _table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            return Task.CompletedTask;
        }
        public virtual Task Delete(object id)
        {
            var existing = _table.Find(id);
            _table.Remove(existing ?? throw new InvalidOperationException());
            return Task.CompletedTask;
        }
        public virtual async Task Save()
        {
           await _context.SaveChangesAsync();
        }
    }
}
