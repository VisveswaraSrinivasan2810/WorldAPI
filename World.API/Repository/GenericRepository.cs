﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using World.API.Data;
using World.API.Repository.IRepository;

namespace World.API.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly WorldAPIDbContext _dbContext;
        public GenericRepository(WorldAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Create(T entity)
        {
            await _dbContext.AddAsync(entity);
            await Save();
        }

        public async Task Delete(T entity)
        {
           _dbContext.Remove(entity);
            await Save();
        }

        public async Task<T> Get(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);

        }

        public async Task<List<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public bool IsRecordExists(Expression<Func<T, bool>> condition)
        {
            var result = _dbContext.Set<T>().AsQueryable().Where(condition);
            return result.Any();

        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
    
}
