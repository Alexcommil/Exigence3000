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
    public class TachesController : Controller
    {
        private tities db = new tities();

        // GET: Taches
        public ActionResult Index()
        {
            var tache = db.Tache.Include(t => t.Jalon).Include(t => t.Responsable);
            return View(tache.ToList());
        }

        // GET: Taches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tache tache = db.Tache.Find(id);
            if (tache == null)
            {
                return HttpNotFound();
            }
            return View(tache);
        }

        // GET: Taches/Create
        public ActionResult Create()
        {
            ViewBag.id_jalon = new SelectList(db.Jalon, "id", "id");
            ViewBag.id_responsable = new SelectList(db.Responsable, "id", "nom");
            return View();
        }

        // POST: Taches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nom,description,id_responsable,date_debut,id_jalon,date_fin,date__debut_prevue")] Tache tache)
        {
            if (ModelState.IsValid)
            {
                db.Tache.Add(tache);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_jalon = new SelectList(db.Jalon, "id", "id", tache.id_jalon);
            ViewBag.id_responsable = new SelectList(db.Responsable, "id", "nom", tache.id_responsable);
            return View(tache);
        }

        // GET: Taches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tache tache = db.Tache.Find(id);
            if (tache == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_jalon = new SelectList(db.Jalon, "id", "id", tache.id_jalon);
            ViewBag.id_responsable = new SelectList(db.Responsable, "id", "nom", tache.id_responsable);
            return View(tache);
        }

        // POST: Taches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nom,description,id_responsable,date_debut,id_jalon,date_fin,date__debut_prevue")] Tache tache)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tache).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_jalon = new SelectList(db.Jalon, "id", "id", tache.id_jalon);
            ViewBag.id_responsable = new SelectList(db.Responsable, "id", "nom", tache.id_responsable);
            return View(tache);
        }

        // GET: Taches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tache tache = db.Tache.Find(id);
            if (tache == null)
            {
                return HttpNotFound();
            }
            return View(tache);
        }

        // POST: Taches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tache tache = db.Tache.Find(id);
            db.Tache.Remove(tache);
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
            tities DB = new tities();
            Tache tache = DB.Tache.Find(id);

            if (tache.date_fin != null) return true;


            return false;
        }
    }
}
