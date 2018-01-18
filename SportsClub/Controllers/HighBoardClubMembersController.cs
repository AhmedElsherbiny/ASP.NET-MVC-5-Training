using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SportsClub.DataAccess;

namespace SportsClub.Controllers
{
    public class HighBoardClubMembersController : Controller
    {
        private SportClubEntities db = new SportClubEntities();

        // GET: HighBoardClubMembers
        public ActionResult Index()
        {
            var highBoardClubMembers = db.HighBoardClubMembers.Include(h => h.Club);
            return View(highBoardClubMembers.ToList());
        }

        // GET: HighBoardClubMembers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HighBoardClubMember highBoardClubMember = db.HighBoardClubMembers.Find(id);
            if (highBoardClubMember == null)
            {
                return HttpNotFound();
            }
            return View(highBoardClubMember);
        }

        // GET: HighBoardClubMembers/Create
        public ActionResult Create()
        {
            ViewBag.ClubID = new SelectList(db.Clubs, "ClubID", "Name");
            return View();
        }

        // POST: HighBoardClubMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HighBoardMembers,ClubID,Name,Role")] HighBoardClubMember highBoardClubMember)
        {
            if (ModelState.IsValid)
            {
                db.HighBoardClubMembers.Add(highBoardClubMember);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClubID = new SelectList(db.Clubs, "ClubID", "Name", highBoardClubMember.ClubID);
            return View(highBoardClubMember);
        }

        // GET: HighBoardClubMembers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HighBoardClubMember highBoardClubMember = db.HighBoardClubMembers.Find(id);
            if (highBoardClubMember == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClubID = new SelectList(db.Clubs, "ClubID", "Name", highBoardClubMember.ClubID);
            return View(highBoardClubMember);
        }

        // POST: HighBoardClubMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HighBoardMembers,ClubID,Name,Role")] HighBoardClubMember highBoardClubMember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(highBoardClubMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClubID = new SelectList(db.Clubs, "ClubID", "Name", highBoardClubMember.ClubID);
            return View(highBoardClubMember);
        }

        // GET: HighBoardClubMembers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HighBoardClubMember highBoardClubMember = db.HighBoardClubMembers.Find(id);
            if (highBoardClubMember == null)
            {
                return HttpNotFound();
            }
            return View(highBoardClubMember);
        }

        // POST: HighBoardClubMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HighBoardClubMember highBoardClubMember = db.HighBoardClubMembers.Find(id);
            db.HighBoardClubMembers.Remove(highBoardClubMember);
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
    }
}
