using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrackMyHabit.Models
{
    public class Calendars
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int Day { get; set; }
        public List<Habits> Events { get; set; }


    }
}
