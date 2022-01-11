using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackMyHabit.Models
{
    public class HabitsDates
    {
        public int HabitsId { get; set; }
        public Habits Habit { get; set; }

        public int AllDatesId { get; set; }
        public AllDates AllDates { get; set; }

        public HabitsDates() { }
    }
}
