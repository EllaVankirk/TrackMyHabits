using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrackMyHabit.Models;

namespace TrackMyHabit.Data
{
    public class TrackMyHabitContext : IdentityDbContext<IdentityUser>
    {
        public TrackMyHabitContext(DbContextOptions<TrackMyHabitContext> options)
            : base(options)
        {
        }
        public DbSet<Habits> Habits { get; set; }
        public DbSet<AllDates> AllDates { get; set; }
        public DbSet<HabitsDates> HabitsDates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<HabitUser>();
            builder.Entity<HabitsDates>().HasKey(hd => new { hd.AllDatesID, hd.HabitsID });
        }
    }
}
