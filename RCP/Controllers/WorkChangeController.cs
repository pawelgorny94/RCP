using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RCP.Models;

namespace RCP.Controllers
{
    public class WorkChangeController : Controller
    {
        private RcpContext db = new RcpContext();

        // GET: WorkChange
        public ActionResult Index()
        {
            return View(db.Zmiany.ToList());
        }

        public ActionResult ZmianyPartial(bool? edit)
        {
            ViewBag.Edit = edit;

            return PartialView("_WorkChange",db.Zmiany.ToList());
        }

        // GET: WorkChange/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zmiany zmiany = db.Zmiany.Find(id);
            if (zmiany == null)
            {
                return HttpNotFound();
            }
            return View(zmiany);
        }

        // GET: WorkChange/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkChange/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Symbol,Nazwa,Od,Do,Stawka,Visible,Ikona,Kolor,TypZmiany,Nadgodziny,NadgodzinyDzien,NadgodzinyNoc,Margines,ZgodaNadg,Kolejnosc,NowaLinia,Widoczna,HideZgoda,Typ,ObetnijOdGory,MarginesNadgodzin,Par1,Par2")] Zmiany zmiany)
        {
            if (ModelState.IsValid)
            {
                db.Zmiany.Add(zmiany);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(zmiany);
        }

        // GET: WorkChange/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zmiany zmiany = db.Zmiany.Find(id);
            if (zmiany == null)
            {
                return HttpNotFound();
            }
            return View(zmiany);
        }

        // POST: WorkChange/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Symbol,Nazwa,Od,Do,Stawka,Visible,Ikona,Kolor,TypZmiany,Nadgodziny,NadgodzinyDzien,NadgodzinyNoc,Margines,ZgodaNadg,Kolejnosc,NowaLinia,Widoczna,HideZgoda,Typ,ObetnijOdGory,MarginesNadgodzin,Par1,Par2")] Zmiany zmiany)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zmiany).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(zmiany);
        }

        // GET: WorkChange/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zmiany zmiany = db.Zmiany.Find(id);
            if (zmiany == null)
            {
                return HttpNotFound();
            }
            return View(zmiany);
        }

        // POST: WorkChange/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Zmiany zmiany = db.Zmiany.Find(id);
            db.Zmiany.Remove(zmiany);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult NewWorkChange()
        {

            return PartialView("_WorkChangeFormModal", new Zmiany());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddWorkChange([Bind(Include = "Symbol,Nazwa,Od,Do")]Zmiany zmiana, string nadgodziny)
        {

            zmiana.Nadgodziny = nadgodziny;
            db.Zmiany.Add(zmiana);
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
