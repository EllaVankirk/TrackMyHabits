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

        public AddHabitsDatesViewModel (Habits habit)
        {
            Habits = habit;
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
    }
}