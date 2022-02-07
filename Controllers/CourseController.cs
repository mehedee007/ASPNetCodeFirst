using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data.Entity;
using OctoberProjectCodeFirst.Models;

namespace OctoberProjectCodeFirst.Controllers
{
    public class CourseController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Course
        public ActionResult Index()
        {
            return View(db.Courses.ToList());
        }

        public ActionResult Details(int id)
        {
            Course course = db.Courses.Find(id);
            return View(course);
        }
        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Course course = db.Courses.Find(id);
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Course course = db.Courses.Find(id);
            return View(course);
        }
        [ActionName("Delete")]
        [HttpPost]
        public ActionResult DeleteConfirm(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}