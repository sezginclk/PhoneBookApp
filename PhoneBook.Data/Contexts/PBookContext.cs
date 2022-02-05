using Microsoft.EntityFrameworkCore;
using PhoneBook.Data.Model.DomainClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Data.Contexts
{
    public class PBookContext : DbContext
    {
        public PBookContext(string connectionString) : base()
        {
            
        }

        public PBookContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("");

            base.OnConfiguring(optionsBuilder);
        }

        #region  Dbsets
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Contacts> Contacts { get; set; }
        public virtual DbSet<Reports> Reports { get; set; }
        #endregion
    }
}
