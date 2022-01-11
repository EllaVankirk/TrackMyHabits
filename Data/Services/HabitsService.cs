using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMyHabit.Data.Base;
using TrackMyHabit.Models;

namespace TrackMyHabit.Data.Services
{
    public class HabitsService : EntityBaseRepository<Habits>, IHabitsService
    {
        private readonly ApplicationDbContext _context;
        public HabitsService(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        public async Task<Habits> GetHabitByIdAsync(int id)
        {
            var habitsDetails = _context.Habits.Include(d => d.AllDates).FirstOrDefaultAsync(h => h.Id == id);
            return await habitsDetails;
        }


    }
}
