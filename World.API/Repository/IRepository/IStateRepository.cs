using World.API.Models;

namespace World.API.Repository.IRepository
{
    public interface IStateRepository : IGenericRepository<State>
    {
        //Task<List<State>> GetAll();

        //Task<State> GetById(int id);

        //Task Create(State entity);
        Task Update(State entity);
        //Task Delete(State entity);
        //Task Save();

        //bool IsStateExists(string name);
    }
}
