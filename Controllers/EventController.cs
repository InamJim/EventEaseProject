using Microsoft.AspNetCore.Mvc;
using EventEase.Models;
using EventEase.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using EventEase.ViewModels;
using EventEase.Data;

namespace EventEase.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly AppDbContext _context;

        public EventController(IEventService eventService, AppDbContext context)
        {
            _eventService = eventService;
            _context = context;
        }

        public IActionResult Index (
    int? eventTypeId,
    bool? availability,
    DateTime? startDate,
    DateTime? endDate )
        {
            var model =
                new EventFilterViewModel();

            model.EventTypes =
                _context.EventType.ToList();

            model.Events =
                _eventService.Search(
                    eventTypeId,
                    availability,
                    startDate,
                    endDate
                ).ToList();

            model.EventTypeId = eventTypeId;
            model.Availability = availability;
            model.StartDate = startDate;
            model.EndDate = endDate;

            return View(model);
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