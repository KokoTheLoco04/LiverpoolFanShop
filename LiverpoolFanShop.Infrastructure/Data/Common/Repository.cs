﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolFanShop.Infrastructure.Data.Common
{
    public class Repository : IRepository
    {
        private readonly DbContext context;

        public Repository(LiverpoolFanShopDbContext _context)
        {
            context = _context;
        }

        private DbSet<T> DbSet<T>() where T : class
        {
            return context.Set<T>();
        }

        public IQueryable<T> All<T>() where T : class
        {
            return DbSet<T>();
        }

        public IQueryable<T> AllReadOnly<T>() where T : class
        {
            return DbSet<T>()
                .AsNoTracking();
        }

        public async Task AddAsync<T>(T entity) where T : class
        {
            await DbSet<T>().AddAsync(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public async Task<T?> GetByIdAsync<T>(params object[] keyValues) where T : class
        {
            return await DbSet<T>().FindAsync(keyValues);
        }

        public async Task DeleteAsync<T>(params object[] keyValues) where T : class
        {
            T? entity = await GetByIdAsync<T>(keyValues);

            if (entity != null)
            {
                DbSet<T>().Remove(entity);
            }
        }

        public async Task DeleteRangeAsync<T>(IEnumerable<T> entities) where T : class
        {
            DbSet<T>().RemoveRange(entities);
            await context.SaveChangesAsync();
        }

        public Task UpdateAsync<T>(T entity) where T : class
        {
            DbSet<T>().Update(entity);
            return Task.CompletedTask;
        }
    }
}
