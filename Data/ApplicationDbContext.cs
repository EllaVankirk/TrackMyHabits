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
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Habits> Habits { get; set; }
        public DbSet<AllDates> AllDates { get; set; }
        public DbSet<HabitsDates> HabitsDates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<HabitUser>();
            modelBuilder.Entity<HabitsDates>().HasKey(hd => new { hd.AllDatesID, hd.HabitsID });
        }

    }
}
