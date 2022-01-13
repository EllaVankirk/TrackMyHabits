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

            //Create new habit.
            var newHabit = new Habits
            {
                Name = habits.HabitName,
                Colour = habits.HabitColor,
            };

            _context.Habits.Add(newHabit);
            _context.SaveChanges();


            //Create new date
            var newDate = new AllDates
            {
                Date = habits.HabitDate,
            };

            _context.AllDates.Add(newDate);
            _context.SaveChanges();

            var habitsDates = new HabitsDates
            {
                HabitsId = newHabit.Id,
                AllDatesId = newDate.Id,
            };

            _context.HabitsDates.Add(habitsDates);

            _context.SaveChanges();
            //Check for existing date
            //foreach (var date in allDates)
            //{
            //    if (newDate.Date.ToLongDateString() == date.Date.ToLongDateString())
            //    {
            //        //If they match, then what?
            //        var habitsDates = new HabitsDates
            //        {
            //            HabitsId = newHabit.Id,
            //            AllDatesId = date.Id,
            //        };
            //        _context.HabitsDates.Add(habitsDates);
            //        _context.SaveChanges();
            //    }
            //    else
            //    {
            //        _context.AllDates.Add(newDate);
            //        var habitsDates = new HabitsDates
            //        {
            //            HabitsId = newHabit.Id,
            //            AllDatesId = newDate.Id
            //        };
            //        _context.HabitsDates.Add(habitsDates);
            //        _context.SaveChanges();
            //    }
            //}
            //_context.SaveChanges();
        }

        public async Task<Habits> GetHabitByIdAsync(int id)
        {
            var habitsDetails = _context.Habits.Include(d => d.AllDates).FirstOrDefaultAsync(h => h.Id == id);
            return await habitsDetails;
        }



    }
}
