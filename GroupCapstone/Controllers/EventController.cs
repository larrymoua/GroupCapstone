using GroupCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace GroupCapstone.Controllers
{
    public class EventController : Controller
    {
        private ApplicationDbContext _context;
        public EventController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Event

        public ActionResult Index()
        {
            var events = _context.events.Include(e => e.Comment).ToList();
            return View(events);
        }

        // GET: Event/Details/5
        public ActionResult Details(int id)
        {
            var events = _context.events.Include(e => e.Comment).SingleOrDefault(e => e.EventId == id);
            return View(events);
        }

        // GET: Event/Create
        [HttpGet]
        public ActionResult Create()
        {
            var comment = _context.events.ToList();
            Event events = new Event()
            {
                Comment = comment
            };
            return View(events);
        }

        // POST: Event/Create
        [HttpPost]
        public ActionResult Create(Event events)
        {
            if(events.EventId == 0)
            {
                _context.events.Add(events);
            }
            else
            {
                // TODO: Add insert logic here
                var eventInDB = _context.events.Single(e => e.EventId == events.EventId);

                eventInDB.CommentId = events.CommentId;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Events");
        }

        // GET: Event/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var events = _context.events.SingleOrDefault(e => e.EventId == id);
            events.Comment = _context.Comments.ToList();
            if (events == null)
            {
                return HttpNotFound();
            }

            return View(events);
        }

        [HttpGet]
        public ActionResult Edit(Event events)
        {
            var eventInDB = _context.events.Single(e => e.EventId == events.EventId);
            eventInDB.CommentId = events.CommentId;
            eventInDB.Comment = _context.Comments.ToList();
            _context.SaveChanges();
            return RedirectToAction("Index", "Events");
        }

        // POST: Event/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Event/Delete/5
        public ActionResult Delete(int id)
        {
            var events = _context.events.SingleOrDefault(e => e.EventId == id);
            _context.events.Remove(events);
            _context.SaveChanges();
            //var  = _context.events.Include(e => e.Comment).ToList();
            return View("Index", events);
        }

        // POST: Event/Delete/5
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
