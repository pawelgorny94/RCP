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
    public class WorkPlanController : Controller
    {
        private RcpContext db = new RcpContext();

        // GET: WorkPlan
        public ActionResult Index()
        {
            return View(db.PlanPracy.ToList());
        }

        public ActionResult Employee()
        {

            IEnumerable<Emlopyee> empl = db.Database.SqlQuery<Emlopyee>("EXEC GetEmployeeInfo").ToList();

            return View(empl);
        }

        // GET: WorkPlan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanPracy planPracy = db.PlanPracy.Find(id);
            if (planPracy == null)
            {
                return HttpNotFound();
            }
            return View(planPracy);
        }

        // GET: WorkPlan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkPlan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdPracownika,Data,IdZmiany,DataZm,IdKierownikaZm,CzasIn,CzasOut,Czas,CzasZm,NadgodzinyDzien,Nocne,Absencja,Akceptacja,DataAcc,IdKierownikaAcc,k_CzasIn,k_CzasOut,k_CzasZm,k_NadgodzinyDzien,k_Nocne,Uwagi,IdZmianyKorekta,NadgodzinyNoc,k_NadgodzinyNoc,Alerty,n50,n100,d50,d100,WymiarKorekta,Wymiar")] PlanPracy planPracy)
        {
            if (ModelState.IsValid)
            {
                db.PlanPracy.Add(planPracy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(planPracy);
        }

        // GET: WorkPlan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanPracy planPracy = db.PlanPracy.Find(id);
            if (planPracy == null)
            {
                return HttpNotFound();
            }
            return View(planPracy);
        }

        // POST: WorkPlan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdPracownika,Data,IdZmiany,DataZm,IdKierownikaZm,CzasIn,CzasOut,Czas,CzasZm,NadgodzinyDzien,Nocne,Absencja,Akceptacja,DataAcc,IdKierownikaAcc,k_CzasIn,k_CzasOut,k_CzasZm,k_NadgodzinyDzien,k_Nocne,Uwagi,IdZmianyKorekta,NadgodzinyNoc,k_NadgodzinyNoc,Alerty,n50,n100,d50,d100,WymiarKorekta,Wymiar")] PlanPracy planPracy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planPracy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(planPracy);
        }

        // GET: WorkPlan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanPracy planPracy = db.PlanPracy.Find(id);
            if (planPracy == null)
            {
                return HttpNotFound();
            }
            return View(planPracy);
        }

        // POST: WorkPlan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlanPracy planPracy = db.PlanPracy.Find(id);
            db.PlanPracy.Remove(planPracy);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Info()
        {
            return View("Info",db.PlanPracy.ToList());
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
