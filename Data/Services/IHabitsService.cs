using System.Threading.Tasks;
using TrackMyHabit.Data.Base;
using TrackMyHabit.Models;
using TrackMyHabit.Models.HabitsViewModels;

namespace TrackMyHabit.Data.Services
{
    public interface IHabitsService : IEntityBaseRepository<Habits>
    {
        Task<Habits> GetHabitByIdAsync(int id);

        Task CreateHabitAsync(CreateHabitWithDateViewModel habits);

        Task AddNewDateToHabitAsync(AddHabitToDateViewModel habits);

        //Task DeleteEmptyDates();
    }
}
