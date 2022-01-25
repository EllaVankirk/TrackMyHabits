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
        public HabitsService(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateHabitAsync(CreateHabitWithDateViewModel habits)
        {
            var allDates = await _context.AllDates.ToListAsync();



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
            };

            await _context.Habits.AddAsync(newHabit);
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

            //retrieves data
            var habitDetails = await _context.Habits.Include(hd => hd.HabitsDates)
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

        public async Task DeleteEmptyDates()
        {

            //what do i want to say?
                //if a dates[i] is NOT inside habitsDates
                //remove the date from the db

        }
    }
}
