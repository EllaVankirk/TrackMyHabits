using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TrackMyHabit.Data.Base;

namespace TrackMyHabit.Models.HabitsViewModels
{
    public class CreateHabitWithDateViewModel
    {
        [Required]
        [Display(Name = "Habit Name")]
        public string HabitName { get; set; }
        [Required]
        [Display(Name = "Color")]
        public string HabitColor { get; set; }

        [Required]
        [Display(Name = "Date")]
        public DateTime HabitDate { get; set; }
    }
}
