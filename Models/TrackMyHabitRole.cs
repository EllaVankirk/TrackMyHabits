using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackMyHabit.Models
{
    public class TrackMyHabitRole : IdentityRole<int>
    {
        public TrackMyHabitRole() { }

        public TrackMyHabitRole (string name)
        {
            Name = name;
        }
    }
}
