using GroupCapstone.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupCapstone.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext context;
        public HomeController()
        {
            context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var currentUser = User.Identity.GetUserId();
            if (User.IsInRole("EventHolder"))
            {
                var eventHolder = context.eventHolders.Where(e => e.ApplicationUserId == currentUser).SingleOrDefault();
                return RedirectToAction("MyEvents", "EventHolder");
            }
            else if (User.IsInRole("Guest"))
            {
                var foundGuest = context.guests.Where(e => e.ApplicationUserId == currentUser).SingleOrDefault();
                var checkGuest = context.guests.Where(c => c.GuestId == foundGuest.GuestId);
                return RedirectToAction("GuestHome", "Guest");
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}