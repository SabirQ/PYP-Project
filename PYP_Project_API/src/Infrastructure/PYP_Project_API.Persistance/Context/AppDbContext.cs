using Microsoft.EntityFrameworkCore;
using PYP_Project_API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Project_API.Persistance.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }
        public DbSet<ExcelCollection> ExcelCollections { get; set; }
        public DbSet<ExcelDataItem> ExcelDataItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            //modelBuilder.ApplyConfiguration(new PlantConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
