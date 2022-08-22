using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PYP_Project_API.Application.Interfaces.Repositories;
using PYP_Project_API.Application.Interfaces.Services;
using PYP_Project_API.Persistance.Context;
using PYP_Project_API.Persistance.Repositories;
using PYP_Project_API.Persistance.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Project_API.Persistance.ServiceRegistration
{
    public static class ServicesRegistration
    {
        public static void AddPersistenceRegistration(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(config.GetConnectionString("Default")
             , d => d.MigrationsAssembly("PYP_Project_API.Persistance")));
            services.AddTransient<IExcelCollectionRepository, ExcelCollectionRepository>();
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
