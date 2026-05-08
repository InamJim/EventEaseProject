using Microsoft.AspNetCore.Mvc;
using EventEase.Models;
using EventEase.Services.Interfaces;

namespace EventEase.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        public IActionResult Index()
        {
            var events = _eventService.GetAll();
            return View(events);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Event ev)
        {
            if (!ModelState.IsValid)
                return View(ev);

            _eventService.Create(ev);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var ev = _eventService.GetById(id);
            return View(ev);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Event ev)
        {
            _eventService.Update(ev);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            try
            {
                _eventService.Delete(id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}