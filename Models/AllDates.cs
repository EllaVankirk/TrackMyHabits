using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackMyHabit.Models
{
    public class AllDates
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }

        public AllDates (DateTime date)
        {
            Date = date;
        }

        public AllDates() { }
    }
}
