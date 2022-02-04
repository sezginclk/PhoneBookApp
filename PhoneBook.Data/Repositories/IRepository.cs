using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneBook.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity); 

        IQueryable<T> Table { get; }
    }
}
