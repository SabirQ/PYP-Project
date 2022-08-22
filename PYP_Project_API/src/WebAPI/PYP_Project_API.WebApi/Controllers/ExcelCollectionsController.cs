using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MimeKit.Text;
using OfficeOpenXml;
using PYP_Project_API.Application.Enums;
using PYP_Project_API.Application.Interfaces.Repositories;
using PYP_Project_API.Application.Interfaces.Services;
using PYP_Project_API.Domain.Entities;
using PYP_Project_API.Persistance.Context;

namespace PYP_Project_API.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelCollectionsController : ControllerBase
    {
        private readonly IExcelCollectionRepository _repository;
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;

        public ExcelCollectionsController(IExcelCollectionRepository repository, AppDbContext context,IEmailService emailService)
        {
            _repository = repository;
            _context = context;
            _emailService = emailService;
        }
        [HttpGet("{type}")]
        public async Task<IActionResult> Get(int id, DateTime start, DateTime end,[FromQuery,BindRequired] List<string> receivers ,ReportType type)
        {
            if (id == 0) return NotFound("Id is not valid");
            if (start >= end) return BadRequest("Please,choose Valid Dates");
            List<ExcelDataItem> items = await _context.ExcelDataItems.Where(x => x.ExcelCollectionId == id && x.Date >= start && x.Date <= end).ToListAsync();
            if (items == null || items.Count == 0) return NotFound();
            for (int i = 0; i < receivers.Count; i++)
            {
                if (!_emailService.CheckEmail(receivers[i]))
                {
                    receivers.Remove(receivers[i]);
                    i--;
                }
            }
            if (receivers.Count == 0) return BadRequest("Email Addresses were not valid");
            string message = string.Empty;
            string subject = string.Empty;
            switch ((int)type)
            {
                case 0:
                    subject = "Report according to Segment";
                    message = "<table> <thead><tr><th>Segment</th><th>Products</th><th>Sales</th><th>Discount</th><th>Profit</th></tr></thead>";
                    for (int i = 0; i < items.Count; i++)
                    {
                        int count = 0;
                        double sale = 0d;
                        double discount = 0d;
                        double profit = 0d;
                        for (int j = 0; j < items.Count; j++)
                        {
                            if (items[i].Segment.ToLower().Trim() == items[j].Segment.ToLower().Trim())
                            {
                                count++;
                                sale += items[j].Sales;
                                discount += items[j].Discounts;
                                profit += items[j].Profit;
                                if (count > 1)
                                {
                                    items.Remove(items[j]);
                                    j--;
                                }
                            }
                        }
                        message += $"<tbody><tr><td>{items[i].Segment}</td><td>{count}</td><td>{sale}</td><td>{discount}</td><td>{profit}</td></tr></tbody>";
                        items.Remove(items[i]);
                        i--;
                    }
                    message += "</table>";
                    break;
                case 1:
                    subject = "Report according to Country";
                    message = "<table> <thead><tr><th>Country</th><th>Products</th><th>Sales</th><th>Discount</th><th>Profit</th></tr></thead>";
                    for (int i = 0; i < items.Count; i++)
                    {
                        int count = 0;
                        double sale = 0d;
                        double discount = 0d;
                        double profit = 0d;
                        for (int j = 0; j < items.Count; j++)
                        {
                            if (items[i].Country.ToLower().Trim() == items[j].Country.ToLower().Trim())
                            {
                                count++;
                                sale += items[j].Sales;
                                discount += items[j].Discounts;
                                profit += items[j].Profit;
                                if (count > 1)
                                {
                                    items.Remove(items[j]);
                                    j--;
                                }
                            }
                        }
                        message += $"<tbody><tr><td>{items[i].Country}</td><td>{count}</td><td>{sale}</td><td>{discount}</td><td>{profit}</td></tr></tbody>";
                        items.Remove(items[i]);
                        i--;
                    }
                    message += "</table>";
                    break;
                case 2:
                    subject = "Report according to Product";
                    message = "<table> <thead><tr><th>Product</th><th>Products Count</th><th>Sales</th><th>Discount</th><th>Profit</th></tr></thead>";
                    for (int i = 0; i < items.Count; i++)
                    {
                        int count = 0;
                        double sale = 0d;
                        double discount = 0d;
                        double profit = 0d;
                        for (int j = 0; j < items.Count; j++)
                        {
                            if (items[i].Product.ToLower().Trim() == items[j].Product.ToLower().Trim())
                            {
                                count++;
                                sale += items[j].Sales;
                                discount += items[j].Discounts;
                                profit += items[j].Profit;
                                if (count > 1)
                                {
                                    items.Remove(items[j]);
                                    j--;
                                }
                            }
                        }
                        message += $"<tbody><tr><td>{items[i].Product}</td><td>{count}</td><td>{sale}</td><td>{discount}</td><td>{profit}</td></tr></tbody>";
                        items.Remove(items[i]);
                        i--;
                    }
                    message += "</table>";
                    break;
                case 3:
                    subject = "Report according to Product Discount";
                    message = "<table> <thead><tr><th>Product</th><th>Discount Persentage</th></tr></thead>";
                    for (int i = 0; i < items.Count; i++)
                    {
                        int count = 0;
                        double sale = 0d;
                        double discount = 0d;
                        double persentage = 0d;
                        
                        for (int j = 0; j < items.Count; j++)
                        {
                            if (items[i].Product.ToLower().Trim() == items[j].Product.ToLower().Trim())
                            {
                                if (count==0)
                                {
                                    sale += items[j].GrossSales;
                                    discount += items[j].Discounts;
                                    persentage = discount / sale * 100;

                                }
                                count++;
                                if (count > 1)
                                {
                                    items.Remove(items[j]);
                                    j--;
                                }
                            }
                        }
                        message += $"<tbody><tr><td>{items[i].Product}</td><td>{persentage}%</td></tr></tbody>";
                        items.Remove(items[i]);
                        i--;
                    }
                    message += "</table>";
                    break;
                default:
                    return BadRequest();

            }
            _emailService.SendEmail(message, subject, receivers);
            return Ok(items);
        }  
        [HttpPost]
        public async Task<IActionResult> Post([BindRequired] IFormFile file)
        {
            if (!file.FileName.Contains("xlxs") && !file.FileName.Contains("xls") || file.Length / 1024 / 1024 > 5) return BadRequest("Please, choose valid excel file");
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var excelDataItems = new List<ExcelDataItem>();
            List<string> columnNames = new List<string>();
            ExcelCollection collection = new ExcelCollection();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    if (package.Workbook.Worksheets == null || package.Workbook.Worksheets.Count == 0) return BadRequest("Please, choose valid excel file");
                    foreach (var worksheet in package.Workbook.Worksheets)
                    {
                        if (worksheet == null) continue;
                        var rowcount = worksheet.Dimension.Rows;
                        for (int i = 1; i <= worksheet.Dimension.Columns; i++)
                        {
                            columnNames.Add(worksheet.Cells[1, i].Value.ToString().Trim());
                        }
                        if (!_repository.CheckTemplate(columnNames)) return BadRequest("Please, choose valid excel file");
                        for (int i = 2; i <= rowcount; i++)
                        {
                            ExcelDataItem item = new ExcelDataItem();
                            item.ExcelCollection = collection;
                            for (int j = 1; j <= worksheet.Dimension.Columns; j++)
                            {
                                if (worksheet.Cells[i, j].Value == null) return BadRequest("Please, choose valid excel file");
                                string word = worksheet.Cells[1, j].Value.ToString().ToLower().Trim();
                                switch (word)
                                {
                                    case "segment":
                                        item.Segment = worksheet.Cells[i, j].Value.ToString().Trim();
                                        break;
                                    case "country":
                                        item.Country = worksheet.Cells[i, j].Value.ToString().Trim();
                                        break;
                                    case "product":
                                        item.Product = worksheet.Cells[i, j].Value.ToString().Trim();
                                        break;
                                    case "discount band":
                                        item.DiscountBand = worksheet.Cells[i, j].Value.ToString().Trim();
                                        break;
                                    case "units sold":
                                        item.UnitsSold = (double)worksheet.Cells[i, j].Value;
                                        break;
                                    case "manufacturing price":
                                        item.ManufacturingPrice = (double)worksheet.Cells[i, j].Value;
                                        break;
                                    case "sale price":
                                        item.SalePrice = (double)worksheet.Cells[i, j].Value;
                                        break;
                                    case "gross sales":
                                        item.GrossSales = (double)worksheet.Cells[i, j].Value;
                                        break;
                                    case "discounts":
                                        item.Discounts = (double)worksheet.Cells[i, j].Value;
                                        break;
                                    case "sales":
                                        item.Sales = (double)worksheet.Cells[i, j].Value;
                                        break;
                                    case "cogs":
                                        item.COGS = (double)worksheet.Cells[i, j].Value;
                                        break;
                                    case "profit":
                                        item.Profit = (double)worksheet.Cells[i, j].Value;
                                        break;
                                    case "date":
                                        item.Date = (DateTime)worksheet.Cells[i, j].Value;
                                        break;
                                    default:
                                        return BadRequest("Please, choose valid excel file");
                                }
                            }
                            excelDataItems.Add(item);
                        }
                    }
                }
                collection.ExcelDataItems = excelDataItems;
                await _repository.UploadDataAsync(collection);
            }
            return Ok(excelDataItems);
        }

    }
}
