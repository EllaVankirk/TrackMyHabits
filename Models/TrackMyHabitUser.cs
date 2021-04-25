using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrackMyHabit.Models
{
    public class TrackMyHabitUser : IdentityUser<int>
    {
        [PersonalData, Required, StringLength(20)]
        public string Username { get; set; }
    }
}
