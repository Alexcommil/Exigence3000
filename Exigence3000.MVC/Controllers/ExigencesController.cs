using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Exigence3000.MVC.Service;

namespace Exigence3000.MVC.Controllers
{
    public class ExigencesController : Controller
    {
        private tities db = new tities();

        // GET: Exigences
        public ActionResult Index()
        {
            var exigence = db.Exigence.Include(e => e.Projet);
            return View(exigence.ToList());
        }

        // GET: Exigences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exigence exigence = db.Exigence.Find(id);
            if (exigence == null)
            {
                return HttpNotFound();
            }
            return View(exigence);
        }

        // GET: Exigences/Create
        public ActionResult Create()
        {
            ViewBag.id_projet = new SelectList(db.Projet, "id", "nom");
            return View();
        }

        // POST: Exigences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nom,type,description,id_projet")] Exigence exigence)
        {
            if (ModelState.IsValid)
            {
                db.Exigence.Add(exigence);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_projet = new SelectList(db.Projet, "id", "nom", exigence.id_projet);
            return View(exigence);
        }

        // GET: Exigences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exigence exigence = db.Exigence.Find(id);
            if (exigence == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_projet = new SelectList(db.Projet, "id", "nom", exigence.id_projet);
            return View(exigence);
        }

        // POST: Exigences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nom,type,description,id_projet")] Exigence exigence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exigence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_projet = new SelectList(db.Projet, "id", "nom", exigence.id_projet);
            return View(exigence);
        }

        // GET: Exigences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exigence exigence = db.Exigence.Find(id);
            if (exigence == null)
            {
                return HttpNotFound();
            }
            return View(exigence);
        }

        // POST: Exigences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Exigence exigence = db.Exigence.Find(id);
            db.Exigence.Remove(exigence);
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

        public static bool isComplete(int id)
        {
            tities db = new tities();
            Exigence exigence = db.Exigence.Find(id);

            foreach(Tache tache in exigence.Tache)
            {
                if (!TachesController.isComplete(tache.id)) return false;
            }

            return true;
            
        }
    }
}
