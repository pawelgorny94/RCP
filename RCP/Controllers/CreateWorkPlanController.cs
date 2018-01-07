using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RCP.Models;
using System.Globalization;

namespace RCP.Controllers
{
    public class CreateWorkPlanController : Controller
    {
        private RcpContext db = new RcpContext();

        // GET: CreateWorkPlan
        public ActionResult Index()
        {
            IEnumerable<Pracownicy> kierownicy = db.Pracownicy.Where(x => x.Status == 1 && x.Kierownik == true).ToList();
            RedirectToAction("SelectPrac");

            var kierownicys = kierownicy.OrderByDescending(x => x.Imie).Select(s => new SelectListItem
            { Value = s.Id.ToString(), Text = s.Imie + "   " + s.Nazwisko + " - " + s.KadryId });
            ViewBag.KierList = new SelectList(kierownicys, "Value", "Text", null);

            

            return View();
        }

        public JsonResult SelectPrac(string id)
        {            
            int kier = Int32.Parse(id);
            IEnumerable<Pracownicy> pracownicy = db.Pracownicy.Where(x => x.Status == 1).ToList();
            IEnumerable<Przypisania> przypisania = db.Przypisania.Where(x => x.Status == 1 && x.IdKierownika==kier && x.Do==null).ToList();

            List<Pracownicy> pracResult = new List<Pracownicy>();
            foreach (var item in przypisania)
            {
                Pracownicy pracAdd = pracownicy.Where(x => x.Id == item.IdPracownika).FirstOrDefault();
                if (pracAdd != null)
                {
                    pracResult.Add(pracAdd);
                }
               

            }
            
                return Json(pracResult, JsonRequestBehavior.AllowGet);
        }

        // GET: CreateWorkPlan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pracownicy pracownicy = db.Pracownicy.Find(id);
            if (pracownicy == null)
            {
                return HttpNotFound();
            }
            return View(pracownicy);
        }

        // GET: CreateWorkPlan/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Calendar(string id)
        {

            IEnumerable<Pracownicy> pracownicy = db.Pracownicy.Where(x => x.Status == 1).ToList();            

            var pracownicys = pracownicy.OrderByDescending(x => x.Imie).Select(s => new SelectListItem
            { Value = s.Id.ToString(), Text = s.Imie + "   " + s.Nazwisko + " - " + s.KadryId });
            ViewBag.PracList = new SelectList(pracownicys, "Value", "Text", null);





            return View();
        }


        public ActionResult CalendarResult(string id,string lateness)
        {

            List<CalendarEntry> calendarEntry = new List<CalendarEntry>();

            if (!String.IsNullOrEmpty(id))
            {
                calendarEntry = db.Database.SqlQuery<CalendarEntry>(String.Format("Exec GetEntryToReport {0}",id)).ToList();
            }

            List<CalendarJSON> result = new List<CalendarJSON>();

            int i = 1;
            if (calendarEntry != null)
            {
                foreach (var item in calendarEntry)
                {
                    CalendarJSON cj = new CalendarJSON();
                    cj.allDay = false;
                    cj.className = Int32.Parse(item.EntryTime.Substring(0, 4)) > 2100 && lateness.Equals("1") ? "important" : "info" ; 
                    cj.id = i++;
                    cj.title = "Od: "+ item.EntryTime.Substring(0, 2) + ":"
                                     + item.EntryTime.Substring(2, 2) + ":"
                                     + item.EntryTime.Substring(4, 2)+ "</br>" +"Do: ";
                    cj.start = new DateTime(Int32.Parse(item.EntryDate.Substring(0, 4)),
                                            Int32.Parse(item.EntryDate.Substring(4, 2)),
                                            Int32.Parse(item.EntryDate.Substring(6, 2)));

                    result.Add(cj);

                }

            }

            //CalendarJSON cls = new CalendarJSON();
            //cls.allDay = false;
            //cls.className = "info";
            //cls.id = 1;
            //cls.title = "Sprawdzenie ewidencji!!!!";
            //cls.start = new DateTime(2018, 01, 06);

            //CalendarJSON cls2 = new CalendarJSON();
            //cls2.allDay = false;
            //cls2.className = "info";
            //cls2.id = 999;
            //cls2.title = "End Time";
            //cls2.start = new DateTime(2018, 01, 07);

            //CalendarJSON cls3 = new CalendarJSON();
            //cls3.allDay = false;
            //cls3.className = "info";
            //cls3.id = 3;
            //cls3.title = "Sprawdzenie ewidencji!!!!";
            //cls3.start = new DateTime(2018, 01, 04);




            //CalendarJSON cls2 = new CalendarJSON();
            //cls2.allDay = false;
            //cls2.className = "info";
            //cls2.id = 2;
            //cls2.title = "Sprawdzenie ewidencji!!!!";
            //cls2.start = DateTime.Today.AddDays(1);

            //List<CalendarJSON> result = new List<CalendarJSON>();
            //result.Add(cls);
           // result.Add(cls2);
            //result.Add(cls3);
            //// result.Add(cls2);
            ViewBag.Event = result;

            return PartialView("_CalendarResult");
        }

        // POST: CreateWorkPlan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Login,Imie,Nazwisko,Opis,Email,Mailing,IdDzialu,IdStanowiska,IdKierownika,IdProjektu,Admin,Kierownik,Raporty,KadryId,Stawka,RcpId,Status,RcpStrefaId,RcpAlgorytm,Created,DataZatr,EtatL,EtatM,Info,CCInfo,Rights,GrSplitu,IdLinii,Nick,Pass,NrKarty1,NrKarty2,DataZwol,KadryId2,KadryId3,PassExpire,OkresProbnyDo,Id2,Pass4")] Pracownicy pracownicy)
        {
            if (ModelState.IsValid)
            {
                db.Pracownicy.Add(pracownicy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pracownicy);
        }

        // GET: CreateWorkPlan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pracownicy pracownicy = db.Pracownicy.Find(id);
            if (pracownicy == null)
            {
                return HttpNotFound();
            }
            return View(pracownicy);
        }

        // POST: CreateWorkPlan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Login,Imie,Nazwisko,Opis,Email,Mailing,IdDzialu,IdStanowiska,IdKierownika,IdProjektu,Admin,Kierownik,Raporty,KadryId,Stawka,RcpId,Status,RcpStrefaId,RcpAlgorytm,Created,DataZatr,EtatL,EtatM,Info,CCInfo,Rights,GrSplitu,IdLinii,Nick,Pass,NrKarty1,NrKarty2,DataZwol,KadryId2,KadryId3,PassExpire,OkresProbnyDo,Id2,Pass4")] Pracownicy pracownicy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pracownicy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pracownicy);
        }

        // GET: CreateWorkPlan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pracownicy pracownicy = db.Pracownicy.Find(id);
            if (pracownicy == null)
            {
                return HttpNotFound();
            }
            return View(pracownicy);
        }

        // POST: CreateWorkPlan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pracownicy pracownicy = db.Pracownicy.Find(id);
            db.Pracownicy.Remove(pracownicy);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveWorkPlan(string idPrac, string idZmiany, string dataOd, string dataDo)
        {

            PlanPracy pp = new PlanPracy();
            pp.Data = DateTime.Now;
            pp.IdPracownika = int.Parse(idPrac);
            pp.IdZmiany = int.Parse(idZmiany);
            pp.CzasIn = DateTime.Parse(dataOd);
            pp.CzasOut = DateTime.Parse(dataDo);
            db.PlanPracy.Add(pp);
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
