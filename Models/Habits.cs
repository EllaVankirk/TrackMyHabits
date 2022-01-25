
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TrackMyHabit.Data.Base;

namespace TrackMyHabit.Models
{

    public class Habits: IEntityBase
    {
        public int Id { get; set; }


        [Required, StringLength(20)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Color")]
        public string Colour { get; set; }

        public List<HabitsDates> HabitsDates { get; set; }


        [Display(Name = "Dates")]
        public List<AllDates> AllDates { get; set; }


    }
}
