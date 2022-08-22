using Microsoft.EntityFrameworkCore;
using PYP_Project_API.Application.Interfaces.Repositories;
using PYP_Project_API.Domain.Entities;
using PYP_Project_API.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Project_API.Persistance.Repositories
{
    public class ExcelCollectionRepository:GenericRepository<ExcelCollection>,IExcelCollectionRepository
    {
        public ExcelCollectionRepository(AppDbContext context):base(context)
        {

        }

        public bool CheckTemplate(List<string> columnNames)
        {
            bool result = true;
            List<string> properties = new List<string>
            {
                "Segment","Country","Product","Discount Band",
                "Units Sold","Manufacturing Price",
                "Sale Price","Gross Sales","Discounts",
                "COGS","Profit","Date","Sales"
            };
            if (properties.Except(columnNames,StringComparer.OrdinalIgnoreCase).Any() || columnNames.Except(properties,StringComparer.OrdinalIgnoreCase).Any())
            {
                result = false;
            }
            return result;
        } 
    }
}
