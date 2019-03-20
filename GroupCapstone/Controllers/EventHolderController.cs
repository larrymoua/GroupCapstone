using GroupCapstone.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using static System.Net.WebRequestMethods;
using System.ComponentModel;
using System.Net.Mail;
using MimeKit;
using MailKit;
using MailKit.Net.Smtp;

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
            var sumOfRating = FoundEvent.Average(e => e.Rating);
            //var avg = sumOfRating / FoundEvent.Count;
            //FoundEventHolder.AvgRating = avg;
            ViewBag.HolderAvg = sumOfRating;

            return View(FoundEvent);
        }

        // GET: EventHolder/Details/5
        public ActionResult Details(int id)
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
            var ticketsBought = db.tickets.Where(e => e.EventId == foundEvent.EventId).ToList();
            var bookmarks = db.bookmarks.Where(b => b.EventId == foundEvent.EventId);
            ViewBag.PurchasedAdmission = ticketsBought.Count();
            ViewBag.BookMarkerForEvents = bookmarks.Count();
            ViewBag.TicketsAvailable = foundEvent.TicketsAvailable - ticketsBought.Count;


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
                    HolderId = eventHolderFound.HolderId,
                    ImagePath = newEvent.ImagePath

                };


                db.events.Add(NewCreatedEvent);
                db.SaveChanges();
                return View("MyEvents");
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
                return View("MyEvents");
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
                var foundbookmarkers = db.bookmarks.Where(b => b.EventId == FoundEvent.EventId).ToList();
                List<Guest> foundGuest = new List<Guest>();
                foreach (var item in foundbookmarkers)
                {
                    var guestFound = db.guests.Where(g => g.GuestId == item.GuestId).Single();
                    foundGuest.Add(guestFound);
                }

                foreach (Guest guest in foundGuest)
                {
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress($"{FoundEvent.EventName}", "sweepsstackproject@gmail.com"));
                    message.To.Add(new MailboxAddress($"{guest.FirstName}, {guest.LastName}", "sweepsstackproject@gmail.com"));
                    message.Subject = "Event Update";

                    message.Body = new TextPart("plain")
                    {
                        Text = $@"Hello ,
                        We would like to notify you on our updates to our event.               
                        -- GroupCapStone"
                    };
                    using (var client = new MailKit.Net.Smtp.SmtpClient())
                    {
                        client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                        client.Connect("smtp.gmail.com", 587, false);
                        client.Authenticate("sweepsstackproject", "sweep12!!");
                        client.Send(message);
                        client.Disconnect(true);


                    }
                
                }

                return RedirectToAction("MyEvents");
            }
                      
            catch
            {
                return RedirectToAction("MyEvents");
            }
        }

       
    }
}
