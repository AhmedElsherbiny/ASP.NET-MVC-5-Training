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
    public class TeamsController : Controller
    {
        private SportClubEntities db = new SportClubEntities();

        // GET: Teams
        public ActionResult Index(string SearchString , int? page)
        {
            var teams = db.Teams.Include(t => t.Club).Include(t => t.Sport);
            teams = from t in db.Teams
                    select t;
            if (!String.IsNullOrEmpty(SearchString))
            {
                teams = teams.Where(c => c.Name.Contains(SearchString) || c.Sport.Name.Contains(SearchString) || c.Club.Name.Contains(SearchString) || c.Players.Any(p => p.FirstName == SearchString));
            }

            return View(teams.ToList().ToPagedList(page ?? 1, 3));
        }

        // GET: Teams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // GET: Teams/Create
        public ActionResult Create()
        {
            ViewBag.ClubID = new SelectList(db.Clubs, "ClubID", "Name");
            ViewBag.SportID = new SelectList(db.Sports, "SportID", "Name");
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeamID,SportID,ClubID,Name,PlayersNumber")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClubID = new SelectList(db.Clubs, "ClubID", "Name", team.ClubID);
            ViewBag.SportID = new SelectList(db.Sports, "SportID", "Name", team.SportID);
            return View(team);
        }

        // GET: Teams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClubID = new SelectList(db.Clubs, "ClubID", "Name", team.ClubID);
            ViewBag.SportID = new SelectList(db.Sports, "SportID", "Name", team.SportID);
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamID,SportID,ClubID,Name,PlayersNumber")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClubID = new SelectList(db.Clubs, "ClubID", "Name", team.ClubID);
            ViewBag.SportID = new SelectList(db.Sports, "SportID", "Name", team.SportID);
            return View(team);
        }

        // GET: Teams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Team team = db.Teams.Find(id);
            db.Teams.Remove(team);
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
