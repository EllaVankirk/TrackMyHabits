using System;
using System.Collections.Generic;

namespace TrackMyHabit.Models.HabitsViewModels
{
    public class UpdateHabitWithDateViewModel
    {
        public int HabitId { get; set; }

        public string HabitName { get; set; }

        public string HabitColor { get; set; }

        public List<HabitsDates> HabitsDates { get; set; }

        public DateTime HabitDate { get; set; }

    }
}
