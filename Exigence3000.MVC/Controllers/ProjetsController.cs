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
    public class ProjetsController : Controller
    {
        private tities db = new tities();

        // GET: Projets
        public ActionResult Index()
        {
            var projet = db.Projet.Include(p => p.Responsable);
            return View(projet.ToList());
        }

        // GET: Projets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projet projet = db.Projet.Find(id);
            if (projet == null)
            {
                return HttpNotFound();
            }
            return View(projet);
        }

        // GET: Projets/Create
        public ActionResult Create()
        {
            ViewBag.id_responsable = new SelectList(db.Responsable, "id", "nom");
            return View();
        }

        // POST: Projets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nom,id_responsable")] Projet projet)
        {
            if (ModelState.IsValid)
            {
                db.Projet.Add(projet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_responsable = new SelectList(db.Responsable, "id", "nom", projet.id_responsable);
            return View(projet);
        }

        // GET: Projets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projet projet = db.Projet.Find(id);
            if (projet == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_responsable = new SelectList(db.Responsable, "id", "nom", projet.id_responsable);
            return View(projet);
        }

        // POST: Projets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nom,id_responsable")] Projet projet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_responsable = new SelectList(db.Responsable, "id", "nom", projet.id_responsable);
            return View(projet);
        }

        // GET: Projets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projet projet = db.Projet.Find(id);
            if (projet == null)
            {
                return HttpNotFound();
            }
            return View(projet);
        }

        // POST: Projets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Projet projet = db.Projet.Find(id);
            db.Projet.Remove(projet);
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

        public static string Progress(int id)
        {
            tities db = new tities();
            Projet projet = db.Projet.Find(id);
            int completion = 0;
            int total = projet.Jalon.Count;
            foreach (Jalon jalon in projet.Jalon)
            {
                if (TachesController.isComplete(jalon.id)) completion++;
            }
            if (completion == 0) return "0";
            completion = (completion / total * 100);
            return completion.ToString();


        }
    }
}
