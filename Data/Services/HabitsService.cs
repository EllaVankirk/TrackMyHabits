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

        public async Task<List<Habits>> GetAllHabitsByUserAsync(string userId)
        {
            var allHabits = await _context.Habits.Include(h => h.User).Where(h => h.UserId == userId).ToListAsync();
            return allHabits;
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

            //look for existing date
            var date = _context.AllDates.Where(d => d.Date == habits.HabitDate).FirstOrDefault();
            if (date == null)
            {
                date = new AllDates
                {
                    Date = habits.HabitDate,
                };
                await _context.AllDates.AddAsync(date);
                await _context.SaveChangesAsync();
            }

            var newHabitDate = new HabitsDates
            {
                AllDatesId = date.Id,
                HabitsId = newHabit.Id,
            };
            await _context.HabitsDates.AddAsync(newHabitDate);
            await _context.SaveChangesAsync();
        }


        public async Task<Habits> GetHabitByIdAsync(int id)
        {
            var habitDetails = await _context.Habits.Include(hd => hd.HabitsDates)
                .ThenInclude(a => a.AllDates)
                .FirstOrDefaultAsync(h => h.Id == id);
            return habitDetails;
        }


        public async Task AddNewDateToHabitAsync(AddHabitToDateViewModel habits)
        {
            //TODO: figure out if I need this line
            //Searches DB based on PK
            var habitID = _context.Habits.Find(habits.HabitId);

            //searches DB for a matching date (hopefully) ID: 1
            var date = _context.AllDates.Where(d => d.Date == habits.HabitDate).FirstOrDefault();
            //if date is null create a new date and save it.
            if (date == null)
            {
                var newDate = new AllDates
                {
                    Date = habits.HabitDate
                };
                await _context.AllDates.AddAsync(newDate);
                await _context.SaveChangesAsync();

                //create a new habitsdate and save it
                var newHabitsDates = new HabitsDates
                {
                    AllDatesId = newDate.Id,
                    HabitsId = habits.HabitId,
                };
                await _context.HabitsDates.AddAsync(newHabitsDates);
                await _context.SaveChangesAsync();
            }
            else if (date != null)
            {
                var habitsDate = _context.HabitsDates.Where(hd => hd.AllDatesId == date.Id).
                    Where(hd => hd.HabitsId == habits.HabitId);
                if (!habitsDate.Any())
                {
                    var newHabitsDates = new HabitsDates
                    {
                        AllDatesId = date.Id,
                        HabitsId = habits.HabitId
                    };
                    await _context.HabitsDates.AddAsync(newHabitsDates);
                    await _context.SaveChangesAsync();
                }
            }
        }

        //TODO: Implement this method. Not sure how as of 1/25/22.
        //public async Task DeleteEmptyDates()
        //{
        //}
    }
}
