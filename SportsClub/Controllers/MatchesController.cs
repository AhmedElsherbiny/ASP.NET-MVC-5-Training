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
    public class MatchesController : Controller
    {
        private SportClubEntities db = new SportClubEntities();
        protected bool CheckDate(String date)
        {

            try
            {

                DateTime dt = DateTime.Parse(date);

                return true;

            }
            catch
            {

                return false;

            }

        }
        // GET: Matches
        public ActionResult Index(string SearchString)
        {
            var matches = db.Matches.Include(m => m.Team).Include(m => m.Team1);
             matches = from m in db.Matches
                        select m;
             DateTime dt = DateTime.MinValue;
            if (!String.IsNullOrEmpty(SearchString))
            {
                if(CheckDate(SearchString)){
                     dt = DateTime.Parse(SearchString);
                }
                matches = matches.Where(m => m.Team.Club.Sports.Any(s => s.Name.Contains(SearchString)) || m.Team.Club.Name.Contains(SearchString) || m.Date == dt);
            }
    
            return View(matches.ToList());
        }

        // GET: Matches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        // GET: Matches/Create
        public ActionResult Create()
        {
            ViewBag.HostedTeamID = new SelectList(db.Teams, "TeamID", "Name");
            ViewBag.VisitorTeamID = new SelectList(db.Teams, "TeamID", "Name");
            return View();
        }

        // POST: Matches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MatchID,HostedTeamID,VisitorTeamID,Date,Location")] Match match)
        {
            if (ModelState.IsValid)
            {
                db.Matches.Add(match);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HostedTeamID = new SelectList(db.Teams, "TeamID", "Name", match.HostedTeamID);
            ViewBag.VisitorTeamID = new SelectList(db.Teams, "TeamID", "Name", match.VisitorTeamID);
            return View(match);
        }

        // GET: Matches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            ViewBag.HostedTeamID = new SelectList(db.Teams, "TeamID", "Name", match.HostedTeamID);
            ViewBag.VisitorTeamID = new SelectList(db.Teams, "TeamID", "Name", match.VisitorTeamID);
            return View(match);
        }

        // POST: Matches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MatchID,HostedTeamID,VisitorTeamID,Date,Location")] Match match)
        {
            if (ModelState.IsValid)
            {
                db.Entry(match).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HostedTeamID = new SelectList(db.Teams, "TeamID", "Name", match.HostedTeamID);
            ViewBag.VisitorTeamID = new SelectList(db.Teams, "TeamID", "Name", match.VisitorTeamID);
            return View(match);
        }

        // GET: Matches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Match match = db.Matches.Find(id);
            db.Matches.Remove(match);
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
