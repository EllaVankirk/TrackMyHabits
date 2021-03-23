using System;
using System.Collections;
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

        public DateTime Day { get; set; } = DateTime.Now;

        public DateTime FirstOfMonth { get; set; }

        public DateTime StartDate { get; set; }

        public IEnumerable<DateTime> Dates { get; set; }

        public Calendars()
        {
            FirstOfMonth = new DateTime(Year, Month, 1);
            StartDate = FirstOfMonth.AddDays(-(int)FirstOfMonth.DayOfWeek);
            Dates = Enumerable.Range(0, 42).Select(i => StartDate.AddDays(i));
        }

    }
}
