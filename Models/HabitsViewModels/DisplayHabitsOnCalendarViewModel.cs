using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackMyHabit.Models.HabitsViewModels
{
    public class DisplayHabitsOnCalendarViewModel
    {
        public int HabitID { get; set; }
        public string HabitInitials { get; set; }
        public string Colour { get; set; }
        public List<Habits> AllHabits { get; set; }
        public List<HabitsDates> HabitDates { get; set; }

        public Habits Habit { get; set; }
        public AllDates AllDates { get; set; }

        public DateTime Day { get; set; } = DateTime.Now;
        public DateTime DisplayDate { get; set; }
        public DateTime FirstOfMonth { get; set; }
        public DateTime StartDate { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public IEnumerable<DateTime> CalendarDates { get; set; }

        public DisplayHabitsOnCalendarViewModel(List<Habits> theHabits, List<HabitsDates> habitDates, Calendars theCalendar)
        {
            //HabitID = theHabit.ID;
            //HabitInitials = theHabit.HabitInitial;
            //Colour = theHabit.Colour;
            HabitDates = habitDates;
            AllHabits;

            DisplayDate = theCalendar.DisplayDate;
            Month = DisplayDate.Month;
            Year = DisplayDate.Year;
            FirstOfMonth = new DateTime(DisplayDate.Year, DisplayDate.Month, 1);
            StartDate = FirstOfMonth.AddDays(-(int)FirstOfMonth.DayOfWeek);
            CalendarDates = Enumerable.Range(0, 42).Select(i => StartDate.AddDays(i));

            //this whole block populates HabitDates with a string of dates.
            //habitDates refers to the viewModel by calling AllDates, AllDates then goes to it's own DateTime property called Date and turns it all into a string


        }
    }
}
