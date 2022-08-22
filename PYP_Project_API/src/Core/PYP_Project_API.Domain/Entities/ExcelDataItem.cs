using PYP_Project_API.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Project_API.Domain.Entities
{
    public class ExcelDataItem:BaseEntity
    {
        public string Segment { get; set; }
        public string Country { get; set; }
        public string Product { get; set; }
        public string DiscountBand { get; set; }
        public double UnitsSold { get; set; }
        public double ManufacturingPrice { get; set; }
        public double SalePrice { get; set; }
        public double GrossSales { get; set; }
        public double Discounts { get; set; }
        public double Sales { get; set; }
        public double COGS { get; set; }
        public double Profit { get; set; }
        public DateTime Date { get; set; }
        public ExcelCollection ExcelCollection { get; set; }
        public int ExcelCollectionId { get; set; }
    }
}
