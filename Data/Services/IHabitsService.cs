using System.Threading.Tasks;
using TrackMyHabit.Data.Base;
using TrackMyHabit.Models;

namespace TrackMyHabit.Data.Services
{
    public interface IHabitsService : IEntityBaseRepository<Habits>
    {
        Task<Habits> GetHabitByIdAsync(int id);

    }
}
