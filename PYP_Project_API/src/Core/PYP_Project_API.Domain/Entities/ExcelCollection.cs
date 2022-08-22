using PYP_Project_API.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Project_API.Domain.Entities
{
    public class ExcelCollection:BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public List<ExcelDataItem> ExcelDataItems { get; set; }
    }
}
