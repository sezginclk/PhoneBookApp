using PhoneBook.Data.Contexts;
using PhoneBook.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        PBookContext GetContext();

        IRepository<T> GetRepository<T>() where T : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
