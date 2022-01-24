using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMyHabit.Data.Base;
using TrackMyHabit.Models;
using TrackMyHabit.Models.HabitsViewModels;

namespace TrackMyHabit.Data.Services
{
    public class HabitsService : EntityBaseRepository<Habits>, IHabitsService
    {
        private readonly ApplicationDbContext _context;
        public HabitsService(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        public async Task CreateHabitAsync(CreateHabitWithDateViewModel habits)
        {
            var allDates = _context.AllDates.ToList();

            //Create new date
            var newDate = new AllDates
            {
                Date = habits.HabitDate,
            };

            _context.AllDates.Add(newDate);
            _context.SaveChanges();

            //Create new habit.
            var newHabit = new Habits
            {
                Name = habits.HabitName,
                Colour = habits.HabitColor,
                AllDatesId = newDate.Id,

            };

            _context.Habits.Add(newHabit);
            _context.SaveChanges();


            var habitsDates = new HabitsDates
            {
                HabitsId = newHabit.Id,
                AllDatesId = newDate.Id,
            };

            _context.HabitsDates.Add(habitsDates);

            _context.SaveChanges();

        }


        public async Task<Habits> GetHabitByIdAsync(int id)
        {
            var habitDetails = _context.Habits.Include(hd => hd.HabitsDates)
                .ThenInclude(a => a.AllDates)
                .FirstOrDefaultAsync(h => h.Id == id);
            return await habitDetails;
        }


        public async Task AddDateToHabit(UpdateHabitWithDateViewModel habit)
        {
            //display the habit
            //pass in a new date
            //check if it exists
            //add that id or create a new one

            //retrieves data
            var habitDetails = _context.Habits.Include(hd => hd.HabitsDates)
                .ThenInclude(a => a.AllDates)
                .FirstOrDefaultAsync(h => h.Id == habit.HabitId);

            //Create new date
            var newDate = new AllDates
            {
                Date = habit.HabitDate,
            };

            _context.AllDates.Add(newDate);
            _context.SaveChanges();

            var habitsDates = new HabitsDates
            {
                HabitsId = habit.HabitId,
                AllDatesId = newDate.Id,
            };

            _context.HabitsDates.Add(habitsDates);

            _context.SaveChanges();

        }
    }
}
