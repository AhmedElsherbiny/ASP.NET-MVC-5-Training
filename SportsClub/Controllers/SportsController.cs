using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SportsClub.DataAccess;
using PagedList;
using PagedList.Mvc;
namespace SportsClub.Controllers
{
    public class SportsController : Controller
    {
        private SportClubEntities db = new SportClubEntities();

        // GET: Sports
        public ActionResult Index(string searchString , int? page)
        {
            var sports = from s in db.Sports
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                sports = sports.Where(s => s.Name.Contains(searchString));
            }
            return View(sports.ToList().ToPagedList(page ?? 1, 3));
        }

        // GET: Sports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sport sport = db.Sports.Find(id);
            if (sport == null)
            {
                return HttpNotFound();
            }
            return View(sport);
        }

        // GET: Sports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SportID,Name")] Sport sport)
        {
            if (ModelState.IsValid)
            {
                db.Sports.Add(sport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sport);
        }

        // GET: Sports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sport sport = db.Sports.Find(id);
            if (sport == null)
            {
                return HttpNotFound();
            }
            return View(sport);
        }

        // POST: Sports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SportID,Name")] Sport sport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sport);
        }

        // GET: Sports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sport sport = db.Sports.Find(id);
            if (sport == null)
            {
                return HttpNotFound();
            }
            return View(sport);
        }

        // POST: Sports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sport sport = db.Sports.Find(id);
            db.Sports.Remove(sport);
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
