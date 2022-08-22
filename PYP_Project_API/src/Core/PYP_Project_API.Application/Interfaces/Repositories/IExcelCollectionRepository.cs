using PYP_Project_API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Project_API.Application.Interfaces.Repositories
{
    public interface IExcelCollectionRepository:IGenericRepository<ExcelCollection>
    {
       public bool CheckTemplate(List<string> columnNames);

    }
}
