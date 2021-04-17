using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackMyHabit.Controllers
{
    public class AllDatesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
