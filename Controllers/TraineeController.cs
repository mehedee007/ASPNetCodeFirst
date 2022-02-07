using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data.Entity; 
using OctoberProjectCodeFirst.Models;
using System.Net;
using System.IO;
using PagedList;
namespace OctoberProjectCodeFirst.Controllers
{
    

    public class TraineeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Trainees
        //[AllowAnonymous]
        public JsonResult EmailExist(string TraineeEmail)
        {
            return Json(!db.Trainees.Any(t => t.TraineeEmail == TraineeEmail), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            // Pagination
            ViewBag.CurrentSort = sortOrder;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            //int pageSize = 3;
            int pageNumber = (page ?? 1);
            ViewBag.CurrentFilter = currentFilter;

            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            var trainees = from t in db.Trainees
                            select t;
            switch (sortOrder)
            {
                case "name_desc":
                    {
                        trainees = trainees.OrderByDescending(t => t.TraineeName);
                        break;
                    }
                default:
                    {
                        trainees = trainees.OrderBy(e => e.TraineeName);
                        break;
                    }
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                trainees = trainees.Where(e => e.TraineeName.ToLower().Contains(searchString.ToLower()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    {
                        trainees = trainees.OrderByDescending(e => e.TraineeName);
                        break;
                    }
                default:
                    {
                        trainees = trainees.OrderBy(e => e.TraineeName);
                        break;
                    }
            }
            //return View(employees.ToList());
            //return View(employees.ToPagedList(pageNumber, pageSize));
            var trainee = db.Trainees.Include(t => t.Course);
            return View(trainees.ToPagedList((page ?? 1), 5));

        }

        //public ActionResult Index()
        //{
        //    var trainee = db.Trainees.Include(t => t.Course);
        //    return View(db.Trainees.ToList());
        //}

        // GET: Trainees/Details/5
        //[AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainee trainee = db.Trainees.Find(id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            return PartialView(trainee);
        }

        //Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Trainee trainee, HttpPostedFileBase fileBase)
        {
            if (ModelState.IsValid)
            {
                if (fileBase.ContentLength > 0)
                {
                    string img = Path.GetFileName(fileBase.FileName);
                    fileBase.SaveAs(Server.MapPath("~/Images/Trainees/" + img));
                    trainee.TraineeImage = "~/Images/Trainees/" + img;
                }
                db.Trainees.Add(trainee);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", trainee.CourseID);
            //return View(trainee);
            return View(trainee);
        }

        // GET: Trainees/Edit/5
        public ActionResult Edit(int? id)
        {
            Trainee trainee = db.Trainees.Find(id);
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", trainee.CourseID);
            Session["TraineeImage"] = trainee.TraineeImage;
            return PartialView(trainee);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Trainee trainee, HttpPostedFileBase fileBase)
        {
            if (ModelState.IsValid)
            {           
                if (fileBase.ContentLength > 0)
                {
                    string img = Path.GetFileName(fileBase.FileName);
                    fileBase.SaveAs(Server.MapPath("~/Images/Trainees/" + img));
                    trainee.TraineeImage = "~/Images/Trainees/" + img;
                }
                else
                {
                    trainee.TraineeImage = Session["TraineeImage"].ToString();
                }
                db.Entry(trainee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", trainee.CourseID);
            //return View(trainee);
            return RedirectToAction("Index");
            //return PartialView("_Trainee", db.Trainees.ToList());
        }

        // GET: Trainees/Delete/5

        public ActionResult Delete(int? id)
        {
            Trainee trainee = db.Trainees.Find(id);
            db.Trainees.Remove(trainee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}