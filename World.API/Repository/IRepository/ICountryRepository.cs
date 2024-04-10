using World.API.Data;
using World.API.Models;

namespace World.API.Repository.IRepository
{
    public interface ICountryRepository : IGenericRepository<Country> 
    {
        //Task<List<Country>> GetAll();

        //Task<Country> GetById(int id);

        //Task Create(Country entity);
        Task Update(Country entity);
        //Task Delete(Country entity);

        //Task Save();

        //bool IsCountryExists(string name);

    }
}
