using GroupCapstone.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNet.Identity;
using MimeKit;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupCapstone.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext db;

        public CartController()
        {
            db = new ApplicationDbContext();
        }
  
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cart/Create
        [HttpPost]
        public ActionResult Create(string stripeToken)
        {
            StripeConfiguration.SetApiKey("sk_test_xUz5aOBDwQSi8S61VVen5E37");
     
            var options = new ChargeCreateOptions
            {
                Amount = 999,
                Currency = "usd",
                Description = "Example charge for larrymoua24@yahoo.com",
                SourceId = stripeToken
            };
            var service = new ChargeService();
            Charge charge = service.Create(options);

            var model = new ChargeViewModel();
            model.ChargeId = charge.Id;

            var CurrentUser = User.Identity.GetUserId();
            
            var guestFound = db.guests.Where(e => e.ApplicationUserId == CurrentUser).SingleOrDefault();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress($"Group CapStone", "sweepsstackproject@gmail.com"));
            message.To.Add(new MailboxAddress($"{guestFound.FirstName} {guestFound.LastName}", "sweepsstackproject@gmail.com"));
            message.Subject = "Order Confirmation";

            message.Body = new TextPart("plain")
            {
                Text = $@"Hello {guestFound.FirstName},
                        Thanks for purchase. Here is your confirmation number.
                        {charge.Id}
                        -- GroupCapStone"
            };
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
