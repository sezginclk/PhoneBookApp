using PhoneBook.Data.Contexts;
using PhoneBook.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Data.UnitOfWork
{

    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly PBookContext _pbookContext; 
        public PBookContext GetContext()
        {
            return _pbookContext;
        }


        public EFUnitOfWork(PBookContext pbookContext)
        {
            //dbContext.Database.SetInitializer<DbContext>(null);

            if (pbookContext == null)
                throw new ArgumentNullException("pbookContext can not be null.");

            _pbookContext = pbookContext;

            // Buradan istediğiniz gibi EntityFramework'ü konfigure edebilirsiniz.

        }

        #region IUnitOfWork Members
        public IRepository<T> GetRepository<T>() where T : class
        {
            return new EFRepository<T>(_pbookContext);
        }

        public int SaveChanges()
        {
            try
            {
                return _pbookContext.SaveChanges();
            }
            catch
            {
                // Burada DbEntityValidationException hatalarını handle edebiliriz.
                throw;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _pbookContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Burada DbEntityValidationException hatalarını handle edebiliriz.
                throw;
            }
        }

        #endregion

        #region IDisposable Members
        // Burada IUnitOfWork arayüzüne implemente ettiğimiz IDisposable arayüzünün Dispose Patternini implemente ediyoruz.
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _pbookContext.Dispose();
                }
            }

            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion



    }
}
