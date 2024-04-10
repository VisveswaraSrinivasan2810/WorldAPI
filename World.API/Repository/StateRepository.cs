using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using World.API.Data;
using World.API.Models;
using World.API.Repository.IRepository;

namespace World.API.Repository
{
    public class StateRepository : GenericRepository<State>,IStateRepository
    {
        private readonly WorldAPIDbContext _dbContext;
        public StateRepository(WorldAPIDbContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        //public async Task<List<State>> GetAll()
        //{
        //    List<State> states = await _dbContext.States.ToListAsync();
        //    return states;
        //}

        //public async Task Create(State entity)
        //{
        //    await _dbContext.States.AddAsync(entity);
        //    await Save();
        //}

        //public async Task Delete(State entity)
        //{
        //    _dbContext.States.Remove(entity);
        //    await Save();
        //}

        public async Task Update(State entity)
        {
            _dbContext.States.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        //public async Task<State> GetById(int id)
        //{
        //    var state =  await _dbContext.States.FindAsync(id);
        //    return state;
        //}

        //public bool IsStateExists(string name)
        //{
        //    var result = _dbContext.States.AsQueryable().Where(x => x.Name.ToLower() == name.ToLower().Trim()).Any();
        //    return result;
        //}
        //public async Task Save()
        //{
        //    await _dbContext.SaveChangesAsync();
        //}

    }
}
