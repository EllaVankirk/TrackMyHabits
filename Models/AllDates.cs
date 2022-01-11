using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TrackMyHabit.Data.Base;

namespace TrackMyHabit.Models
{
    public class AllDates : IEntityBase
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime Date { get; set; }

        public List<HabitsDates> HabitsDates { get; set; }
        public AllDates (DateTime date)
        {
            Date = date;
        }
        public override string ToString()
        {
            return Date.Date.ToString("MM/d/yyyy");
        }

        public AllDates() { }
    }
}
