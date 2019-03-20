using GroupCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace GroupCapstone.Controllers
{
    public class EventController : Controller
    {
        private ApplicationDbContext _context;
        public EventController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public ActionResult AddComment(int id)
        {
            CommentVM commentView = new CommentVM();
            commentView.Event = _context.events.Where(e => e.EventId == id).Single();
            return View(commentView);
        }

        [HttpPost]
        public ActionResult AddComment(CommentVM commentView)
        {
            var user = User.Identity.GetUserId();
            var guest = _context.guests.Where(g => g.ApplicationUserId == user).SingleOrDefault();
            Comment comment = new Comment();
            comment.Date = DateTime.Now;
            comment.EventId = commentView.Event.EventId;
            comment.User = guest.FirstName;
            comment.Description = commentView.Comment.Description;
            _context.Comments.Add(comment);

            _context.SaveChanges();
            return RedirectToAction("EventDetails","Guest", new { id = commentView.Event.EventId });
        }

    }
}
