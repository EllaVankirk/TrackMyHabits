﻿using Microsoft.AspNetCore.Mvc.Rendering;
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
        public DateTime Day { get; set; } = DateTime.Now;
        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime DisplayDate { get; set; }
        public DateTime FirstOfMonth { get; set; }
        public DateTime StartDate { get; set; }
        public IEnumerable<DateTime> RangeOfDates { get; set; }

        public Calendars () { }
        public Calendars (DateTime displayDate)
        {
            DisplayDate = displayDate;
            Month = DisplayDate.Month;
            Year = DisplayDate.Year;
            FirstOfMonth = new DateTime(DisplayDate.Year, DisplayDate.Month, 1);
            StartDate = FirstOfMonth.AddDays(-(int)FirstOfMonth.DayOfWeek);
            RangeOfDates = Enumerable.Range(0, 42).Select(i => StartDate.AddDays(i));
        }

    }
}
