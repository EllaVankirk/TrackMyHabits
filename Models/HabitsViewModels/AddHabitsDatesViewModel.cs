using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrackMyHabit.Models.HabitsViewModels
{
    public class AddHabitsDatesViewModel
    {
        public Habits Habits { get; set; }
        public AllDates AllDates { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public AddHabitsDatesViewModel (Habits habit)
        {
            Habits = habit;
        }

        public AddHabitsDatesViewModel (DateTime date)
        {
            Date = date;
        }
        public AddHabitsDatesViewModel(AllDates allDates)
        {
            AllDates = allDates;
        }

        public AddHabitsDatesViewModel (Habits habit, AllDates allDates)
        {
            Habits = habit;
            AllDates = allDates;
        }

        public override string ToString()
        {
            return Date.Date.ToString("MM/d/yyyy");
        }

    }
}