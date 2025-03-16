using System;
using System.Net.Http;
using EventSphereApp.Data;
using EventSphereApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EventSphereApp.Controllers
{
    public class EventsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Events()
        {
            var events = _context.EventsFormed.ToList(); // Fetch only required columns 
            return View(events);
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var eventToDelete = _context.EventsFormed.Find(Id);
            if (eventToDelete != null)
            {
                _context.EventsFormed.Remove(eventToDelete);
                _context.SaveChanges();
            }
            return RedirectToAction("Events");
        }

    }
}
