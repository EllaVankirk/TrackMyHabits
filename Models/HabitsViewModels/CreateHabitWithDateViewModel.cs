using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackMyHabit.Data.Base;

namespace TrackMyHabit.Models.HabitsViewModels
{
    public class CreateHabitWithDateViewModel
    {
        public string HabitName { get; set; }
        public string HabitColor { get; set; }
        public DateTime HabitDate { get; set;}
    }
}
