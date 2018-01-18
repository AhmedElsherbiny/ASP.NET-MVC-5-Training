using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SportsClub.DataAccess;
using System.IO;
using PagedList;
using PagedList.Mvc;
namespace SportsClub.Controllers
{
    public class ClubsController : Controller
    {
        private SportClubEntities db = new SportClubEntities();

        // GET: Clubs
        public ActionResult Index(string SearchString , int? page)
        {
            var clubs = from c in db.Clubs
                        select c;
            if (!String.IsNullOrEmpty(SearchString))
            {
                clubs = clubs.Where(c => c.Name.Contains(SearchString) || c.Sports.Any(s => s.Name.Contains(SearchString)));
            }
            return View(clubs.ToList().ToPagedList(page ?? 1 , 3));
        }

        // GET: Clubs/Details/5
        public ActionResult Details(int? id)
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

        // GET: Clubs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clubs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Club club , HttpPostedFileBase file)
        {
            string path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(file.FileName));
            file.SaveAs(path);
            if (ModelState.IsValid)
            {
                db.Clubs.Add(new Club { Name = club.Name, Age = club.Age, EstablishDate = club.EstablishDate, Logo = "~/Images/" + file.FileName });
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClubID,Name,Age,EstablishDate,Logo")] Club club)
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
    }
}
