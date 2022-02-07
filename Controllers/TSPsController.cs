using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OctoberProjectCodeFirst.Models;

namespace OctoberProjectCodeFirst.Controllers
{
    public class TSPsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TSPs
        public ActionResult Index()
        {
            var tSPs = db.TSPs.Include(t => t.Course).Include(t => t.Trainer);
            return View(tSPs.ToList());
        }

        // GET: TSPs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TSP tSP = db.TSPs.Find(id);
            if (tSP == null)
            {
                return HttpNotFound();
            }
            return View(tSP);
        }

        // GET: TSPs/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            ViewBag.TrainerID = new SelectList(db.Trainers, "TrainerID", "TrainerName");
            return View();
        }

        // POST: TSPs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TspID,TspName,TspAddress,TspContact,TspEmail,TrainerID,CourseID")] TSP tSP)
        {
            if (ModelState.IsValid)
            {
                db.TSPs.Add(tSP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", tSP.CourseID);
            ViewBag.TrainerID = new SelectList(db.Trainers, "TrainerID", "TrainerName", tSP.TrainerID);
            return View(tSP);
        }

        // GET: TSPs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TSP tSP = db.TSPs.Find(id);
            if (tSP == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", tSP.CourseID);
            ViewBag.TrainerID = new SelectList(db.Trainers, "TrainerID", "TrainerName", tSP.TrainerID);
            return View(tSP);
        }

        // POST: TSPs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TspID,TspName,TspAddress,TspContact,TspEmail,TrainerID,CourseID")] TSP tSP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tSP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", tSP.CourseID);
            ViewBag.TrainerID = new SelectList(db.Trainers, "TrainerID", "TrainerName", tSP.TrainerID);
            return View(tSP);
        }

        // GET: TSPs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TSP tSP = db.TSPs.Find(id);
            if (tSP == null)
            {
                return HttpNotFound();
            }
            return View(tSP);
        }

        // POST: TSPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TSP tSP = db.TSPs.Find(id);
            db.TSPs.Remove(tSP);
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
