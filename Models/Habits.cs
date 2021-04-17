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
        public string HabitInitial { get; set; }

        public Habits(string name, string colour, string habitInitial)
        {
            Name = name;
            colour = Colour;
            habitInitial = HabitInitial;
        }

        public Habits() { }

    }
}
