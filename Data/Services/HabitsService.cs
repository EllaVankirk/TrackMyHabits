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
            //Create new habit.
            var newHabit = new Habits
            {
                Name = habits.HabitName,
                Colour = habits.HabitColor,
            };
            await _context.Habits.AddAsync(newHabit);
            await _context.SaveChangesAsync();

            var allDates = await _context.AllDates.ToListAsync();
            var currentHabitDate = habits.HabitDate.ToLongDateString();

            var incrementCounter = 0;

            if (allDates.Count > 0)
            {
                foreach (var date in allDates)
                {
                    //if found add the dateID to make a new habitsdates
                    if (currentHabitDate == date.Date.ToLongDateString()) //if true, increment existingDatesCounter, create a new habitsdates, save it and exit the loop.
                    {
                        var habitsDates = new HabitsDates
                        {
                            HabitsId = newHabit.Id,
                            AllDatesId = date.Id
                        };
                        await _context.HabitsDates.AddAsync(habitsDates);
                        await _context.SaveChangesAsync();
                        break;
                    }
                    else if (currentHabitDate != date.Date.ToLongDateString())
                    {
                        incrementCounter++;
                        continue;
                    }
                }
                if (incrementCounter == allDates.Count)
                {
                    var newDate = new AllDates
                    {
                        Date = habits.HabitDate,
                    };
                    await _context.AllDates.AddAsync(newDate);
                    await _context.SaveChangesAsync();

                    var habitsDates = new HabitsDates
                    {
                        HabitsId = newHabit.Id,
                        AllDatesId = newDate.Id,
                    };
                    await _context.HabitsDates.AddAsync(habitsDates);
                    await _context.SaveChangesAsync();
                }
            }
            else //if it is less than 1, create a new date and habitdate.
            {
                var newDate = new AllDates
                {
                    Date = habits.HabitDate,
                };
                await _context.AllDates.AddAsync(newDate);
                await _context.SaveChangesAsync();

                var habitsDates = new HabitsDates
                {
                    HabitsId = newHabit.Id,
                    AllDatesId = newDate.Id,
                };
                await _context.HabitsDates.AddAsync(habitsDates);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<Habits> GetHabitByIdAsync(int id)
        {
            var habitDetails = await _context.Habits.Include(hd => hd.HabitsDates)
                .ThenInclude(a => a.AllDates)
                .FirstOrDefaultAsync(h => h.Id == id);
            return habitDetails;
        }


        public async Task AddNewDateToHabitAsync(AddHabitToDateViewModel habit)
        {
            //Create new date
            var newDate = new AllDates
            {
                Date = habit.HabitDate,
            };

            await _context.AllDates.AddAsync(newDate);
            await _context.SaveChangesAsync();

            var habitsDates = new HabitsDates
            {
                HabitsId = habit.HabitId,
                AllDatesId = newDate.Id,
            };

            await _context.HabitsDates.AddAsync(habitsDates);

            await _context.SaveChangesAsync();
        }

        //TODO: Implement this method. Not sure how as of 1/25/22.
        //public async Task DeleteEmptyDates()
        //{
        //}
    }
}
