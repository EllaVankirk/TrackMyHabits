
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
        [Required, StringLength(20)]
        public string Name { get; set; }
        [Required]
        public string Colour { get; set; }


        public Habits(string name, string colour, string habitInitial)
        {
            Name = name;
            Colour = colour;
        }

        public Habits() { }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID);
        }
    }
}
