using AspNetCorePrep.Models;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCorePrep.ViewComponents
{
    public class HoursViewComponent: ViewComponent
    {
        private ApplicationDbContext _context;

        public HoursViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            
            return View(_context.TimeEntries.Where(f=>f.ApplicationUserId == HttpContext.User.GetUserId()).Sum(f=>f.Hours));
        }
    }
}
