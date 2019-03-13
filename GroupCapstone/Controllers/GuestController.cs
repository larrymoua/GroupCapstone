using GroupCapstone.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
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
            ApplicationDbContext db = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var userLoggedin = User.Identity.GetUserId();

            var guests = db.guests.Where(g => g.ApplicationUserId == userLoggedin);
            return View(guests.ToList());
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
                    //db.Guest.Add(guest);
                    db.guests.Add(guest);
                    guest.ApplicationUserId = User.Identity.GetUserId();
                    db.SaveChanges();
                    return RedirectToAction("Index");
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

                return RedirectToAction("Index");
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

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
