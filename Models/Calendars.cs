using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace TrackMyHabit.Models
{
    public class Calendars
    {
        public DateTime Day { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public DateTime FirstOfMonth { get; set; }
        public DateTime StartDate { get; set; }
        public IEnumerable<DateTime> Dates { get; set; }
        public int HabitId { get; set; }
        public List<Habits> Habit { get; set; }

        public Calendars(int year = 2021, int month = 1)
        {
            Month = month;
            Year = year;
            Day = new DateTime(Year, Month, 1);
            FirstOfMonth = new DateTime(Day.Year, Day.Month, 1);
            StartDate = FirstOfMonth.AddDays(-(int)FirstOfMonth.DayOfWeek);
            Dates = Enumerable.Range(0, 42).Select(i => StartDate.AddDays(i));
        }
    }
}
