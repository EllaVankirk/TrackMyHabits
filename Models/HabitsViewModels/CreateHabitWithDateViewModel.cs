using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackMyHabit.Models.HabitsViewModels
{
    public class CreateHabitWithDateViewModel
    {
        public int ID { get; set; }
        public Habits Habit { get; set; }

        public DateTime HabitDate { get; set; }

        public AllDates AllDates { get; set; }

    }
}
