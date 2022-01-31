using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackMyHabit.Models;

namespace TrackMyHabit.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //Table Names
        public DbSet<Habits> Habits { get; set; }
        public DbSet<AllDates> AllDates { get; set; }
        public DbSet<HabitsDates> HabitsDates { get; set; }

        //How to build the DB
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Creates the join table.
            modelBuilder.Entity<HabitsDates>()
                .HasKey(hd => new { hd.AllDatesId, hd.HabitsId });


            //modelBuilder.Entity<HabitsDates>().HasOne(h => h.Habit).WithMany(hd => hd.HabitsDates).HasForeignKey(h => h.HabitsId);
            //modelBuilder.Entity<HabitsDates>().HasOne(d => d.AllDates).WithMany(hd => hd.HabitsDates).HasForeignKey(d => d.AllDatesId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
