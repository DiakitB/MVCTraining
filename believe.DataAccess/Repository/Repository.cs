using believe.DataAccess.Repository.IRepository;
using believe.DataOD;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace believe.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationContext _context;
        internal DbSet<T> dbSet;
        public Repository(ApplicationContext context)
        {
            _context = context;
           // _context.products.Include(u => u.Category).Include(u => u.CoverType);
            this.dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }
        //includeProp -"Category, CoverType"
        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if(includeProperties != null)
            {
                foreach(var includeProp in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries)) 
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            query.Where(filter);
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);    
        }
    }
}
