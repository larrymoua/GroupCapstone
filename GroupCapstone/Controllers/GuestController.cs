using GroupCapstone.Models;
using Microsoft.AspNet.Identity;
using Stripe;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Net.Http;
using Newtonsoft.Json;

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

            List<Models.Event> eventsThisWeek = new List<Models.Event> { };

            foreach (var foundEvent in eventsInZip)
            {
                int eventWeek = GetWeekNumber(foundEvent.EventDate);
                if (eventWeek == currentWeek)
                {
                    eventsThisWeek.Add(foundEvent);
                }
            }
            var typeList = Enum.GetValues(typeof(Category))
            .Cast<Category>()
            .Select(t => new AcessClass
            {
                Category = ((Category)t),
            });

            ViewBag.ListData = typeList;

            return View(eventsThisWeek);

            
        }
        public ActionResult Filter(string id)
        {
            var CurrentUser = User.Identity.GetUserId();
            var guestFound = db.guests.Where(g => g.ApplicationUserId == CurrentUser).SingleOrDefault();

            var filteredEvents = db.events.Where(e => e.Category.ToString() == id && e.Zip == guestFound.Zip).ToList();
            return View(filteredEvents);
        }

        public ActionResult FilterHighRating()
        {            
            var allEvents = db.events.ToList();
            var newList = allEvents.OrderByDescending(a => a.Rating).ToList();
            return View(newList);
        }

        public int GetWeekNumber(DateTime date)
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            int weekNumber = currentCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
            return weekNumber;
        }

        public ActionResult BookMarker(int id)
        {
            try
            {
                var CurrentUser = User.Identity.GetUserId();
                var guestFound = db.guests.Where(g => g.ApplicationUserId == CurrentUser).SingleOrDefault();
                var newBookMark = new Bookmarks { EventId = id, GuestId = guestFound.GuestId };
                db.bookmarks.Add(newBookMark);
                db.SaveChanges();

                return RedirectToAction("GuestHome");
            }
            catch
            {

                return RedirectToAction("GuestHome");
            }

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
            var foundEvent = db.events.Find(id);

            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                var states = Extensions.GetDescription(foundEvent.State);
                client.BaseAddress = new Uri("Https://maps.googleapis.com/maps/api/geocode/");
                HttpResponseMessage response = client.GetAsync($"json?address={foundEvent.Street}+{foundEvent.Zip},+{foundEvent.City},+{states}&key=AIzaSyBBA-VL6jTbTGJNW77AsuCuLRVwXB2wKGo").Result;
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync().Result;
                RootObject root = JsonConvert.DeserializeObject<RootObject>(result);

                double Latitude = 0.0;
                double Longitude = 0.0;
                foreach (var item in root.results)
                {
                    Latitude = item.geometry.location.lat;
                    Longitude = item.geometry.location.lng;
                    ViewBag.Lat = Latitude.ToString();
                    ViewBag.Long = Longitude.ToString();
                }

            }


            return View(foundEvent);
        }

        public ActionResult MyPurchasedTickets()
        {
            var userId = User.Identity.GetUserId();
            Guest guest = db.guests.Where(g => g.ApplicationUserId == userId).Single();
            var tickets = db.tickets.Where(t => t.GuestId == guest.GuestId).ToList();
            List<Models.Event> eventTix = new List<Models.Event>();
            foreach (var tix in tickets)
            {
                var tempEvent = db.events.Where(e => e.EventId == tix.EventId).SingleOrDefault();
                eventTix.Add(tempEvent);
            }
            
            return View(eventTix);
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

        public ActionResult GiveRating(int id)
        {
            try
            {
                var ratedEvent = db.events.Where(e => e.EventId == id).SingleOrDefault();
                return View(ratedEvent);
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult GiveRating(int id, Models.Event ratedEvent)
        {
            var thisEvent = db.events.Where(e => e.EventId == id).SingleOrDefault();
            if (DateTime.Now > thisEvent.EventDate)
            {
                Ratings rating = new Ratings();
                rating.Rating = ratedEvent.Rating;
                rating.EventId = thisEvent.EventId;
                db.ratings.Add(rating);
                db.SaveChanges();
                var eventsRatings = db.ratings.Where(r => r.EventId == id).ToList();
                List<int> selectedRatings = new List<int>();
                foreach (var filteredRating in eventsRatings)
                {
                    selectedRatings.Add(filteredRating.Rating);
                }
                int sum = selectedRatings.Sum();
                int averageRating = sum / selectedRatings.Count;
                thisEvent.Rating = averageRating;
                
             
                ratedEvent.Rating = thisEvent.Rating;

                db.SaveChanges();

            }
            return RedirectToAction("GuestHome");
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
