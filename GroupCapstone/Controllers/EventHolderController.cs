﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupCapstone.Controllers
{
    public class EventHolderController : Controller
    {
        // GET: EventHolder
        public ActionResult Index()
        {
            return View();
        }

        // GET: EventHolder/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EventHolder/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventHolder/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EventHolder/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EventHolder/Edit/5
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

        // GET: EventHolder/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EventHolder/Delete/5
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