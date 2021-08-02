using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackMyHabit.Models
{
    public class HabitUser : IdentityUser
    {
        public int HabitUserID { get; set; }
        [PersonalData]
        public string FirstName{ get; set; }
        [PersonalData]
        public string LastName { get; set; }
    }
}
