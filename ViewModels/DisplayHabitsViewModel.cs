using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackMyHabit.Models;

namespace TrackMyHabit.ViewModels
{
    public class DisplayHabitsViewModel
    {
        public Calendars Calendar { get; set; }
        public List<Habits> Habit { get; set; }

        public DisplayHabitsViewModel(Calendars calendar, List<Habits> habit)
        {
            calendar = Calendar;
            habit = Habit;
        }
    }
}
