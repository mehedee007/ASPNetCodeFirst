using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OctoberProjectCodeFirst.Models;
using OctoberProjectCodeFirst.Models.ViewModels;

namespace OctoberProjectCodeFirst.Controllers
{
    public class TrainersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Trainers
        public ActionResult Index()
        {
            var trainers = db.Trainers.Include(t => t.Course);
            return View(trainers.ToList());
        }

        // GET: Trainers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = db.Trainers.Find(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }

        // GET: Trainers/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            return View();
        }

        // POST: Trainers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TrainerVM tvm)
        {
            Trainer trainer = new Trainer();
            if (ModelState.IsValid)
            {
                trainer.TrainerName = tvm.TrainerName;
                trainer.TrainerContact = tvm.TrainerContact;
                trainer.TrainerEmail = tvm.TrainerEmail;

                trainer.IsActive = tvm.IsActive;
                trainer.CourseID = tvm.CourseID;
                db.Trainers.Add(trainer);
                db.SaveChanges();
                //if(trainer.IsActive == true)
                //{
                //    trainer.IsActive.ToString() = "Active";
                //    return
                //}
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", trainer.CourseID);
            return View(trainer);
        }

        // GET: Trainers/Edit/5
        public ActionResult Edit(int id)
        {
            Trainer tr = db.Trainers.SingleOrDefault(d => d.TrainerID == id);
            var tvm = new TrainerVM();
            tvm.TrainerName = tr.TrainerName;
            tvm.TrainerContact = tr.TrainerContact;
            tvm.TrainerEmail = tr.TrainerEmail;
  
            tvm.IsActive = tr.IsActive;
            tvm.CourseID = tr.CourseID;
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", tr.CourseID);
            return View(tr);
        }

        // POST: Trainers/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TrainerVM tvm, int id)
        {
            Trainer tr = db.Trainers.Find(id);
            if (ModelState.IsValid)
            {
                tr.TrainerName = tvm.TrainerName;
                tr.TrainerContact = tvm.TrainerContact;
                tr.TrainerEmail = tvm.TrainerEmail;

                tr.IsActive = tvm.IsActive;
                tr.CourseID = tvm.CourseID;

                db.Entry(tr).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", tr.CourseID);
            return View(tr);
        }

        public ActionResult Delete(int id)
        {
            Trainer tr = db.Trainers.SingleOrDefault(d => d.TrainerID == id);
            var tvm = new TrainerVM();
            tvm.TrainerName = tr.TrainerName;
            tvm.TrainerContact = tr.TrainerContact;
            tvm.TrainerEmail = tr.TrainerEmail;

            tvm.IsActive = tr.IsActive;
            tvm.CourseID = tr.CourseID;
            return View(tvm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmedDelete(int id)
        {
            Trainer tr = db.Trainers.Find(id);
            if (tr != null)
            {
                db.Trainers.Remove(tr);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}

