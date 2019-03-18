using GroupCapstone.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GroupCapstone.Controllers
{
    public class GuestController : Controller
    {
        ApplicationDbContext db;
        // GET: Guest

        public GuestController()
        {
            db = new ApplicationDbContext();
        }
        public ActionResult GuestHome()
        {
            var userLoggedin = User.Identity.GetUserId();
            var currentGuest = db.guests.Where(g => g.ApplicationUserId == userLoggedin).Single();
            var currentDate = DateTime.Now;
            int currentWeek = GetWeekNumber(currentDate);
            var eventsInZip = db.events.Where(e => e.Zip == currentGuest.Zip).ToList();        
            List<Event> eventsThisWeek = new List<Event> { };

            foreach (var foundEvent in eventsInZip)
            {
                int eventWeek = GetWeekNumber(foundEvent.EventDate);
                if (eventWeek == currentWeek)
                {
                    eventsThisWeek.Add(foundEvent);
                }
            }

            return View(eventsThisWeek);
        }

        public int GetWeekNumber(DateTime date)
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            int weekNumber = currentCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
            return weekNumber;
        }
            
        private bool CheckIfDatesAreSameWeek(DateTime firstDate, DateTime secondDate)
        {
            var calendar = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
            var d1 = firstDate.Date.AddDays(-1 * (int)calendar.GetDayOfWeek(firstDate));
            var d2 = secondDate.Date.AddDays(-1 * (int)calendar.GetDayOfWeek(secondDate));

            return d1 == d2;
        }

        
        // GET: Guest/Details/5
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guest guest = db.guests.Find(id);
            if (guest == null)
            {
                return HttpNotFound();
            }
            return View(guest);
        }

        public ActionResult EventDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event foundEvent = db.events.Find(id);
            if (foundEvent == null)
            {
                return HttpNotFound();
            }
            return View(foundEvent);
        }

        // GET: Guest/Create
        public ActionResult Create()
        {
            Guest guest = new Guest();
            return View(guest);
        }

        // POST: Guest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GuestId,FirstName,LastName,Zip")] Guest guest)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {                   
                    db.guests.Add(guest);
                    guest.ApplicationUserId = User.Identity.GetUserId();
                    db.SaveChanges();
                    return RedirectToAction("GuestHome");
                }

            }
            catch
            {
                //ModelState.AddModelError();
            }
            return View(guest);
        }

        // GET: Guest/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var editedGuest = db.guests.Where(g => g.GuestId == id).SingleOrDefault();
                return View(editedGuest);
            }
            catch
            {
                return View();
            }
        }

        // POST: Guest/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Guest guest)
        {
            try
            {
                // TODO: Add update logic here
                Guest thisGuest = db.guests.Find(id);

                thisGuest.FirstName = guest.FirstName;
                thisGuest.LastName = guest.LastName;
                thisGuest.Zip = guest.Zip;

                db.SaveChanges();

                return RedirectToAction("GuestHome");
            }
            catch
            {
                return View(guest);
            }
        }

        // GET: Guest/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Guest/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("GuestHome");
            }
            catch
            {
                return View();
            }
        }
    }
}
