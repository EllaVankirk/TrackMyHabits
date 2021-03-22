using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackMyHabit.Models
{
    public class Habits
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public Habits()
        {

        }

        public Habits(string name, DateTime date, string type)
        {
            Name = name;
            Date = date;
            Type = type;

        }

        public override string ToString()
        {
            return Name;
        }
    }
}
