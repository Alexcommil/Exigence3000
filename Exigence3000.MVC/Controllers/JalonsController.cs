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
    public class JalonsController : Controller
    {
        private tities db = new tities();

        // GET: Jalons
        public ActionResult Index()
        {
            var jalon = db.Jalon.Include(j => j.Projet).Include(j => j.Responsable);
            return View(jalon.ToList());
        }

        // GET: Jalons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jalon jalon = db.Jalon.Find(id);
            if (jalon == null)
            {
                return HttpNotFound();
            }

            return View(jalon);
        }

        // GET: Jalons/Create
        public ActionResult Create()
        {
            ViewBag.id_projet = new SelectList(db.Projet, "id", "nom");
            ViewBag.id_responsable = new SelectList(db.Responsable, "id", "nom");
            return View();
        }

        // POST: Jalons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_projet,id_responsable,date_prevue,date_livraison")] Jalon jalon)
        {
            if (ModelState.IsValid)
            {
                db.Jalon.Add(jalon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_projet = new SelectList(db.Projet, "id", "nom", jalon.id_projet);
            ViewBag.id_responsable = new SelectList(db.Responsable, "id", "nom", jalon.id_responsable);
            return View(jalon);
        }

        // GET: Jalons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jalon jalon = db.Jalon.Find(id);
            if (jalon == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_projet = new SelectList(db.Projet, "id", "nom", jalon.id_projet);
            ViewBag.id_responsable = new SelectList(db.Responsable, "id", "nom", jalon.id_responsable);
            return View(jalon);
        }

        // POST: Jalons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_projet,id_responsable,date_prevue,date_livraison")] Jalon jalon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jalon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_projet = new SelectList(db.Projet, "id", "nom", jalon.id_projet);
            ViewBag.id_responsable = new SelectList(db.Responsable, "id", "nom", jalon.id_responsable);
            return View(jalon);
        }

        // GET: Jalons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jalon jalon = db.Jalon.Find(id);
            if (jalon == null)
            {
                return HttpNotFound();
            }
            return View(jalon);
        }

        // POST: Jalons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Jalon jalon = db.Jalon.Find(id);
            db.Jalon.Remove(jalon);
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
            Jalon jalon = db.Jalon.Find(id);

            foreach (Tache tache in jalon.Tache)
            {
                if (!TachesController.isComplete(tache.id)) return false;
            }

            return true;


        }
    }
}
