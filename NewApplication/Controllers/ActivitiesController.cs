﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewApplication;
using Rotativa;

namespace NewApplication.Controllers
{
    public class ActivitiesController : Controller
    {
        private DBModelContainer db = new DBModelContainer();

        // GET: Activities
        public ActionResult Index()
        {
            var activities = db.Activities.Include(a => a.AgeGroup);
            return View(activities.ToList());
        }

        // GET: Activities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

       public ActionResult PrintActivity(int ActivityId)
        {
            Activity activity = db.Activities.Find(ActivityId);
            return new ActionAsPdf("Details", new { id = ActivityId }) { FileName = (activity.ActivityName + "ParticipantList.pdf") };
        }
        /*
        // GET: Activities/Create
        public ActionResult Create()
        {
            ViewBag.AgeGroupAgeGroupId = new SelectList(db.AgeGroups, "AgeGroupId", "AgeGroupName");
            ViewBag.Zone_ZoneId = new SelectList(db.Zones, "ZoneId", "ZoneName");
            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActivityId,AgeGroupAgeGroupId,ActivityName,Zone_ZoneId")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Activities.Add(activity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AgeGroupAgeGroupId = new SelectList(db.AgeGroups, "AgeGroupId", "AgeGroupName", activity.AgeGroupAgeGroupId);
            ViewBag.Zone_ZoneId = new SelectList(db.Zones, "ZoneId", "ZoneName", activity.Zone_ZoneId);
            return View(activity);
        }

        // GET: Activities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgeGroupAgeGroupId = new SelectList(db.AgeGroups, "AgeGroupId", "AgeGroupName", activity.AgeGroupAgeGroupId);
            ViewBag.Zone_ZoneId = new SelectList(db.Zones, "ZoneId", "ZoneName", activity.Zone_ZoneId);
            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActivityId,AgeGroupAgeGroupId,ActivityName")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AgeGroupAgeGroupId = new SelectList(db.AgeGroups, "AgeGroupId", "AgeGroupName", activity.AgeGroupAgeGroupId);
            ViewBag.Zone_ZoneId = new SelectList(db.Zones, "ZoneId", "ZoneName", activity.Zone_ZoneId);
            return View(activity);
        }

        // GET: Activities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity activity = db.Activities.Find(id);
            db.Activities.Remove(activity);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        */
    }
}
