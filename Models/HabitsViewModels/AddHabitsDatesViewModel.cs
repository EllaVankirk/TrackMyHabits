using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackMyHabit.Models.HabitsViewModels
{
    public class AddHabitsDatesViewModel
    {
        public int HabitID { get; set; }
       public Habits Habit { get; set; }

        public int AllDatesID { get; set; }
        public AllDates AllDates { get; set; }

        public List<SelectListItem> Dates { get; set; }

        public AddHabitsDatesViewModel(Habits theHabit, List<AllDates> possibleDates)
        {
            Dates = new List<SelectListItem>();
            foreach (var date in possibleDates)
            {
                Dates.Add(new SelectListItem
                {
                    Value = date.ID.ToString(),
                    Text = date.Date.ToString("MM/d/yyyy")
                });

            }
            Habit = theHabit;
        }

        public AddHabitsDatesViewModel() { }
    }
}
