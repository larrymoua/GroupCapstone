﻿using GroupCapstone.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNet.Identity;
using MimeKit;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using unirest_net.http;

namespace GroupCapstone.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext db;
        public CartController()
        {
            db = new ApplicationDbContext();

        }
  
        public ActionResult Create(int id)
        {
            Models.Event events = db.events.Where(e => e.EventId == id).SingleOrDefault();
           
            return View(events);
        }

        // POST: Cart/Create
        [HttpPost]
        public ActionResult Create(string stripeToken, Models.Event events)
        {

            StripeConfiguration.SetApiKey("sk_test_xUz5aOBDwQSi8S61VVen5E37");
            var CurrentUser = User.Identity.GetUserId();
            var foundEvent = db.events.Where(e => e.EventId == events.EventId).SingleOrDefault();
            var guestFound = db.guests.Where(e => e.ApplicationUserId == CurrentUser).SingleOrDefault();
            long cost = (long)Convert.ToDouble(foundEvent.TicketPrice);

            var options = new ChargeCreateOptions
            {
                Amount = cost*100,
                Currency = "usd",
                Description = "Example charge for larrymoua24@yahoo.com",
                SourceId = stripeToken
            };
            var service = new ChargeService();
            Charge charge = service.Create(options);

            var model = new ChargeViewModel();
            model.ChargeId = charge.Id;

            var purchasedTicket = new Ticket { EventId = foundEvent.EventId, GuestId = guestFound.GuestId};
            db.tickets.Add(purchasedTicket);
            db.SaveChanges();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress($"{foundEvent.EventName}", "sweepsstackproject@gmail.com"));
            message.To.Add(new MailboxAddress($"{guestFound.FirstName} {guestFound.LastName}", "sweepsstackproject@gmail.com"));
            message.Subject = "Order Confirmation";

            message.Body = new TextPart("plain")
            {
                Text = $@"Hello {guestFound.FirstName},
                        Thanks for purchase. Here is your confirmation number.
                        {charge.Id}
                        -- GroupCapStone"
            };

            //HttpResponse response = Unirest.post("https://neutrinoapi-qr-code.p.rapidapi.com/qr-code")
            //.header("X-RapidAPI-Key", "42686f9288msh669750bfe57d0e8p14b94ejsnda982c981f0e")
            //.header("Content-Type", $"{guestFound.FirstName} {guestFound.LastName} is confirmed! Confirmation is {charge.Id}")
            //.field("bg-color", "#ffffff")
            //.field("width", 128)
            //.field("fg-color", "#000000")
            //.field("height", 128)
            //.field("content", "http://www.neutrinoapi.com")
            //.asJson();

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("sweepsstackproject", "sweep12!!");
                client.Send(message);
                client.Disconnect(true);

                return View("OrderStatus", model);
            }


        }

    }
}
