﻿using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace NewApplication.Controllers
{
    public class ClubsController : Controller
    {
        private DBModelContainer db = new DBModelContainer();

        // GET: Clubs
        public ActionResult Index()
        {
            return View(db.Clubs.ToList());
        }

        // GET: Clubs/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["VerifiedClub"] != null)
            {
                var sessionClub = Session["VerifiedClub"] as string;
                if (sessionClub == id.ToString())
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Club club = db.Clubs.Find(id);
                    if (club == null)
                    {
                        return HttpNotFound();
                    }
                    return View(club);
                }
            }
            return RedirectToAction("clubPassGet", "Clubs", new { id = id });
        }

        public ActionResult clubPassGet (int id)
        {
            
            if (Session["VerifiedClub"] != null) {
                var sessionClub = Session["VerifiedClub"] as string;
                if (sessionClub == id.ToString())
                {
                    return RedirectToAction("Details", "Clubs", new { id = id });
                }
            }
            ViewBag.ClubId = id;
            return View();
        }

        public string clubPassPost (string pass, int clubId)
        {
            Club club = db.Clubs.Where(model => model.ClubPass == pass).FirstOrDefault();
            if(club != null)
            {
                Session["VerifiedClub"] = club.ClubId.ToString();
                return Url.Action("Details", "Clubs", new { id = clubId }).ToString();
            }
            else
            {
                Session["VerifiedClub"] = null;
                return Url.Action("clubPassGet", "Clubs", new { id = clubId }).ToString();
            }
            
        }

        public PartialViewResult ParticipantsView(int? id)
        {
            Club club = db.Clubs.Find(id);
            List<Participant> participants = club.Participants.ToList();
            return PartialView(participants);
        }

        [HttpGet]
        public ActionResult CreateParticipant (int? id)
        {
            ViewBag.ClubId = id;
            ViewBag.AgeGroup_AgeGroupId = new SelectList(db.AgeGroups, "AgeGroupId", "AgeGroupName");
            return View();
        }

        [HttpPost]
        public ActionResult CreateParticipant ([Bind(Include = "ParticipantId,ClubClubId,ParticipantName,AgeGroup_AgeGroupId")] Participant participant)
        {
            if (ModelState.IsValid)
            {
                db.Participants.Add(participant);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = participant.ClubClubId } );
            }
            Club club = db.Clubs.Find(participant.ClubClubId);
            return View("Details", club);
        }
        // GET: Clubs/Create
        /*
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clubs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClubId,ClubName")] Club club)
        {
            if (ModelState.IsValid)
            {
                db.Clubs.Add(club);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(club);
        }

        // GET: Clubs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Club club = db.Clubs.Find(id);
            if (club == null)
            {
                return HttpNotFound();
            }
            return View(club);
        }

        // POST: Clubs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClubId,ClubName")] Club club)
        {
            if (ModelState.IsValid)
            {
                db.Entry(club).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(club);
        }

        // GET: Clubs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Club club = db.Clubs.Find(id);
            if (club == null)
            {
                return HttpNotFound();
            }
            return View(club);
        }

        // POST: Clubs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Club club = db.Clubs.Find(id);
            db.Clubs.Remove(club);
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
