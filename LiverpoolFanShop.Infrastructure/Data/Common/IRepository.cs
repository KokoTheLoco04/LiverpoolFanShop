using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolFanShop.Infrastructure.Data.Common
{
    public interface IRepository
    {
        IQueryable<T> All<T>() where T : class;

        IQueryable<T> AllReadOnly<T>() where T : class;

        Task AddAsync<T>(T entity) where T : class;

        Task<int> SaveChangesAsync();

        Task<T?> GetByIdAsync<T>(params object[] keyValues) where T : class;

        Task DeleteAsync<T>(params object[] keyValues) where T : class;
        Task DeleteRangeAsync<T>(IEnumerable<T> entities) where T : class;
    }
}
