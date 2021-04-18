using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TrackMyHabit.Models.HabitsViewModels
{
    public class HabitDetailsViewModel
    {
        public int HabitID { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
        public string HabitInitial { get; set; }
        [DisplayName("Date")]
        public string DateText { get; set; }

        public HabitDetailsViewModel() { }

        public HabitDetailsViewModel(Habits theHabit, List<HabitsDates> habitDates)
        {
            HabitID = theHabit.ID;
            Name = theHabit.Name;
            Colour = theHabit.Colour;
            HabitInitial = theHabit.HabitInitial;

            DateText = "";

            for(var i = 0; i < habitDates.Count; i++)
            {
                DateText += habitDates[i].AllDates.Date.ToString("MM/d/yyyy");

                if (i < habitDates.Count - 1)
                {
                    DateText += ", ";
                }
            }
        }

    }
}
