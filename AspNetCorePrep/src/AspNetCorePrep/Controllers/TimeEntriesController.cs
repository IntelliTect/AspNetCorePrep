using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using AspNetCorePrep.Models;
using Microsoft.AspNet.Authorization;

namespace AspNetCorePrep.Controllers
{
    [Authorize]
    public class TimeEntriesController : Controller
    {
        private ApplicationDbContext _context;

        public TimeEntriesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: TimeEntries
        public IActionResult Index()
        {
            var applicationDbContext = _context.TimeEntries.Include(t => t.ApplicationUser).Include(t => t.Customer);
            return View(applicationDbContext.ToList());
        }

        // GET: TimeEntries/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TimeEntry timeEntry = _context.TimeEntries.Single(m => m.TimeEntryId == id);
            if (timeEntry == null)
            {
                return HttpNotFound();
            }

            return View(timeEntry);
        }

        // GET: TimeEntries/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Name");
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Name");
            return View();
        }

        // POST: TimeEntries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TimeEntry timeEntry)
        {
            if (ModelState.IsValid)
            {
                _context.TimeEntries.Add(timeEntry);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Name", timeEntry.ApplicationUserId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Name", timeEntry.CustomerId);
            return View(timeEntry);
        }

        // GET: TimeEntries/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TimeEntry timeEntry = _context.TimeEntries.Single(m => m.TimeEntryId == id);
            if (timeEntry == null)
            {
                return HttpNotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Name", timeEntry.ApplicationUserId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Name", timeEntry.CustomerId);
            return View(timeEntry);
        }

        // POST: TimeEntries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TimeEntry timeEntry)
        {
            if (ModelState.IsValid)
            {
                _context.Update(timeEntry);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Name", timeEntry.ApplicationUserId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Name", timeEntry.CustomerId);
            return View(timeEntry);
        }

        // GET: TimeEntries/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TimeEntry timeEntry = _context.TimeEntries.Single(m => m.TimeEntryId == id);
            if (timeEntry == null)
            {
                return HttpNotFound();
            }

            return View(timeEntry);
        }

        // POST: TimeEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            TimeEntry timeEntry = _context.TimeEntries.Single(m => m.TimeEntryId == id);
            _context.TimeEntries.Remove(timeEntry);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
