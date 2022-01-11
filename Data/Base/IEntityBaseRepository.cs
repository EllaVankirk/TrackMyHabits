using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrackMyHabit.Data.Base
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);



    }
}
