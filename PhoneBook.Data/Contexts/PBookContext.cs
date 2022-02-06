using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhoneBook.Data.Model.DomainClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Data.Contexts
{
    public class PBookContext : DbContext
    {

        //private string _connectionString;
        //private readonly IConfiguration _config;
        //public PBookContext(string connectionString, IConfiguration config) : base()
        //{
        //    _config = config;
        //}

        //public PBookContext()
        //{

        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Userid=TestUser;Password=SecuredPassword!123;Database=PhoneBookDb");
            base.OnConfiguring(optionsBuilder);
        }

        #region  Dbsets
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Contacts> Contacts { get; set; }
        public virtual DbSet<Reports> Reports { get; set; }
        public virtual DbSet<ReportContent> ReportContents { get; set; }
        #endregion
    }
}
