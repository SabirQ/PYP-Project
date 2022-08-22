using PYP_Project_API.Application.Enums;
using PYP_Project_API.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Project_API.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> UploadDataAsync(T entity);
    }
}
