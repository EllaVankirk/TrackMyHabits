
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace TrackMyHabit.Models
{

    public class Habits
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
        [Display(Name = "Initial")]
        public string HabitInitial { get; set; }

        public int UserID { get; set; }
        public TrackMyHabitUser User {get; set;}


        public Habits(string name, string colour, string habitInitial)
        {
            Name = name;
            Colour = colour;
            HabitInitial = habitInitial;
        }

        public Habits() { }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID);
        }
    }
}
