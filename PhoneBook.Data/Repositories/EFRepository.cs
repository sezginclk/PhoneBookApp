using Microsoft.EntityFrameworkCore;
using PhoneBook.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneBook.Data.Repositories
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        protected readonly PBookContext _pbookContext;

        private readonly DbSet<T> _dbSet;

        public EFRepository(PBookContext pbookContext)
        {
            if (pbookContext == null)
                throw new ArgumentNullException("pbookContext can not be null.");

            _pbookContext = pbookContext;
            try
            {
                _dbSet = pbookContext.Set<T>();
            }
            catch (Exception ex)
            {

            }
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public void Add(T entity)
        {
            _pbookContext.Entry(entity).State = EntityState.Added;
            // _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _pbookContext.Entry(entity).State = EntityState.Deleted;
            // _dbSet.Delete(entity);
        }

        public void Update(T entity)
        {
            _pbookContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return this._dbSet;
            }
        }

    }
}
