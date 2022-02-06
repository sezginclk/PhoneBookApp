using Microsoft.Extensions.DependencyInjection;
using PhoneBook.Data.Contexts;
using PhoneBook.Data.UnitOfWork;
using PhoneBook.Service.Abstract;
using PhoneBook.Service.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.ReportService
{
    public static class StartupManager
    {
        public static IServiceCollection AddDependencyToService(this IServiceCollection services)
        {
            #region ContainerObject

            services.AddScoped<IReportsManager, ReportsManager>();
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
            services.AddScoped<PBookContext>();


            #endregion

            return services;
        }
    }
}
