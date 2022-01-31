using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrackMyHabit.Models.HabitsViewModels
{
    public class AddHabitToDateViewModel
    {
        public int HabitId { get; set; }

        [Display(Name = "Habit")]
        public string HabitName { get; set; }

        [Display(Name = "Color")]
        public string HabitColor { get; set; }

        [Display(Name="Dates")]
        public List<HabitsDates> HabitsDates { get; set; }

        public DateTime HabitDate { get; set; }

    }
}
