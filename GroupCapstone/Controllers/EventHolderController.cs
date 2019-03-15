using GroupCapstone.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupCapstone.Controllers
{
    public class EventHolderController : Controller
    {
        private ApplicationDbContext db;
        public EventHolderController()
        {
            db = new ApplicationDbContext();
        }
        // GET: EventHolder
        public ActionResult MyEvents()
        {
            var CurrentUser = User.Identity.GetUserId();
            var FoundEventHolder = db.eventHolders.Where(e => e.ApplicationUserId == CurrentUser).SingleOrDefault();
            var FoundEvent = db.events.Where(e => e.HolderId == FoundEventHolder.HolderId).ToList();

            return View(FoundEvent);
        }

        // GET: EventHolder/Details/5
        public ActionResult Details(int id)
        {
            var foundEvent = db.events.Find(id);
            return View(foundEvent);
        }

        // GET: EventHolder/Create
        public ActionResult CreateEventHolder()
        {
            EventHolder eventHolder = new EventHolder();
            return View("CreateEventHolder", eventHolder);
        }

        // POST: EventHolder/Create
        [HttpPost]
        public ActionResult CreateEventHolder(EventHolder eventHolder)
        {

            try
            {
                // TODO: Add insert logic here
                db.eventHolders.Add(eventHolder);
                eventHolder.ApplicationUserId = User.Identity.GetUserId();
                db.SaveChanges();
                return RedirectToAction("MyEvents");
            }
            catch
            {
                return View();
            }
        }

        // GET: EventHolder/CreateNewEvent
        public ActionResult CreateNewEvent(string id)
        {
            Event newEvent = new Event();
            return View(newEvent);
        }

        [HttpPost]
        public ActionResult CreateNewEvent(Event newEvent)
        {
            var CurrentUser = User.Identity.GetUserId();

            var eventHolderFound = db.eventHolders.Where(e => e.ApplicationUserId == CurrentUser).SingleOrDefault();
            try
            {
      


                var NewCreatedEvent = new Event
                {
                    EventName = newEvent.EventName,
                    EventDate = newEvent.EventDate,
                    Street = newEvent.Street,
                    City = newEvent.City,
                    State = newEvent.State,
                    Zip = newEvent.Zip,
                    TicketsAvailable = newEvent.TicketsAvailable,
                    TicketPrice = newEvent.TicketPrice,
                    EventId = eventHolderFound.HolderId,
                    Category = newEvent.Category,
                    EventHolders = eventHolderFound,
                    HolderId = eventHolderFound.HolderId
                };


                db.events.Add(NewCreatedEvent);
                db.SaveChanges();
                return RedirectToAction("MyEvents");
            }

            catch
            {
                return View("MyEvents");
            }

        }
        // GET: EventHolder/Edit/5
        public ActionResult EditEventHolder(int id)
        {
            var editedEventHolder = db.eventHolders.Where(e => e.HolderId == id).SingleOrDefault();
            return View(editedEventHolder);
        }

        // POST: EventHolder/Edit/5
        [HttpPost]
        public ActionResult EditEventHolder(int id, EventHolder eventHolder)
        {

            try
            {
                // TODO: Add update logic here
                var editedEventHolder = db.eventHolders.Where(c => c.HolderId == id).SingleOrDefault();
                editedEventHolder.FirstName = eventHolder.FirstName;
                editedEventHolder.LastName = eventHolder.LastName;
                editedEventHolder.CompanyName = eventHolder.CompanyName;
                db.SaveChanges();
                return RedirectToAction("MyEvents");
            }
            catch
            {
                return View();
            }
        }

        // GET: EventHolder/Delete/5
        public ActionResult Delete(int id)
        {
            var deleteEvent = db.events.Where(e => e.EventId == id).SingleOrDefault();
            return View(deleteEvent);
        }

        // POST: EventHolder/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Event events)
        {
            try
            {
                var deleteEvent = db.events.Where(e => e.EventId == id).SingleOrDefault();
                db.events.Remove(deleteEvent);
                db.SaveChanges();

                return RedirectToAction("MyEvents");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult EditEvent(int id)
        {
            var editedEvent = db.events.Find(id);
            return View(editedEvent);
        }

        // POST: EventHolder/Edit/5
        [HttpPost]
        public ActionResult EditEvent(Event events)
        {
            var CurrentUser = User.Identity.GetUserId();
 
            var FoundEvent = db.events.Where(e => e.EventId == events.EventId).SingleOrDefault();
            try
            {
                FoundEvent.EventName = events.EventName;
                FoundEvent.EventDate = events.EventDate;
                FoundEvent.Street = events.Street;
                FoundEvent.City = events.City;
                FoundEvent.State = events.State;
                FoundEvent.Zip = events.Zip;
                FoundEvent.TicketsAvailable = events.TicketsAvailable;
                FoundEvent.TicketPrice = events.TicketPrice;
                db.SaveChanges();

                return RedirectToAction("MyEvents", "EventHolder");
            }
            catch
            {
                return View();
            }
        }
    }
}
