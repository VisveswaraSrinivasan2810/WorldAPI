﻿using Microsoft.EntityFrameworkCore;
using World.API.Data;
using World.API.Models;
using World.API.Repository.IRepository;

namespace World.API.Repository
{
    public class CountryRepository : GenericRepository<Country>,ICountryRepository
    {
        private readonly WorldAPIDbContext _dbContext;
        public CountryRepository(WorldAPIDbContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        //public async Task Create(Country entity)
        //{
        //    await _dbContext.Countries.AddAsync(entity);
        //    await Save();
        //}

        //public async Task Delete(Country entity)
        //{
        //    _dbContext.Countries.Remove(entity);
        //    await Save();
        //}

        //public async Task<List<Country>> GetAll()
        //{
        //    List<Country> countries = await _dbContext.Countries.ToListAsync();
        //    return countries;

        //}

        //public async Task<Country> GetById(int id)
        //{
        //    Country country = await _dbContext.Countries.FindAsync(id);
        //    return country;
        //}

        //public bool IsCountryExists(string name)
        //{
        //    var result = _dbContext.Countries.AsQueryable().Where(x=>x.Name.ToLower().Trim() == name.ToLower().Trim()).Any();
        //    return result;
        //}

        //public async Task Save()
        //{
        //   await _dbContext.SaveChangesAsync();
        //}

        public async Task Update(Country entity)
        {
           _dbContext.Countries.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
