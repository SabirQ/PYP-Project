using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PYP_Project_API.Application.Enums;
using PYP_Project_API.Application.Interfaces.Repositories;
using PYP_Project_API.Domain.Entities.Base;
using PYP_Project_API.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Project_API.Persistance.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {

        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<T> UploadDataAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
