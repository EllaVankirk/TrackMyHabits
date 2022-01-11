using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrackMyHabit.Data.Base
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {

        //returns a value
        Task<IEnumerable<T>> GetAllAsync();

        //returns a value hence the <T>
        Task<T> GetByIdAsync(int id);

        //does not return a value
        Task AddAsync(T entity);

        //does not return a value
        Task UpdateAsync(int id, T entity);

        //does not return a value
        Task DeleteAsync(int id);

    }
}
