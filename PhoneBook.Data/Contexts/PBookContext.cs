using PhoneBook.Data.Model.DomainClass;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace PhoneBook.Data.Contexts
{
    public class PBookContext : DbContext
    {
        public PBookContext(string connectionString) : base(connectionString)
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public PBookContext()
        {

        }

        #region  Dbsets
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Contacts> Contacts { get; set; }
        #endregion
    }
}
