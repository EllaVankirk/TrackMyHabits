using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrackMyHabit.Data;
using TrackMyHabit.Models;

namespace TrackMyHabit.Controllers
{
    public class AllDatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AllDatesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.AllDates.ToListAsync());
        }

        public IActionResult Add()
        {
            AllDates dates = new AllDates();
            return View(dates);
        }

        [HttpPost]
        public IActionResult Add(AllDates dates)
        {
            if (ModelState.IsValid)
            {
                _context.AllDates.Add(dates);
                _context.SaveChanges();
                return Redirect("/AllDates");
            }
            return View("Add", dates);
        }
    }
}
