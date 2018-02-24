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
    public class WejsciaController : Controller
    {
        private RcpContext db = new RcpContext();

        // GET: Wejscia
        public ActionResult Index()
        {
            IEnumerable<Wejscia> wejscia = db.Wejscia;
            foreach(var item in wejscia)
            {
                item.CzasWejscia = DateTime.ParseExact(item.Data+" "+item.Czas, "yyyyMMdd HHmmss",
                                       System.Globalization.CultureInfo.InvariantCulture);
            }

            return View(wejscia.ToList());
        }

        // GET: Wejscia/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wejscia wejscia = db.Wejscia.Find(id);
            if (wejscia == null)
            {
                return HttpNotFound();
            }
            return View(wejscia);
        }

        // GET: Wejscia/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Wejscia/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Data,Czas,TID,UID,Nazwa,Unikalny,Biuro,Post,Karta,Typ,Tryb,TypM,Resultat")] Wejscia wejscia)
        {
            if (ModelState.IsValid)
            {
                db.Wejscia.Add(wejscia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wejscia);
        }

        // GET: Wejscia/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wejscia wejscia = db.Wejscia.Find(id);
            if (wejscia == null)
            {
                return HttpNotFound();
            }
            return View(wejscia);
        }

        // POST: Wejscia/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Data,Czas,TID,UID,Nazwa,Unikalny,Biuro,Post,Karta,Typ,Tryb,TypM,Resultat")] Wejscia wejscia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wejscia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wejscia);
        }

        // GET: Wejscia/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wejscia wejscia = db.Wejscia.Find(id);
            if (wejscia == null)
            {
                return HttpNotFound();
            }
            return View(wejscia);
        }

        // POST: Wejscia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Wejscia wejscia = db.Wejscia.Find(id);
            db.Wejscia.Remove(wejscia);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //public JsonResult GetData()
        //{
        //    IEnumerable<Wejscia> wejscia = db.Wejscia.ToList();
        //    return Json(wejscia, JsonRequestBehavior.AllowGet);
        //}


        public PartialViewResult GetData()
        {

            IEnumerable<Wejscia> wejscia = db.Wejscia;
            IEnumerable<Pracownicy> pracownicy = db.Pracownicy;
            IEnumerable<Wejscia> wejsciaAll = db.Wejscia;

            foreach (var item in wejscia)
            {
                item.CzasWejscia = DateTime.ParseExact(item.Data + " " + item.Czas, "yyyyMMdd HHmmss",
                                       System.Globalization.CultureInfo.InvariantCulture);
            }

            wejscia = wejscia.Where(x => x.CzasWejscia.ToShortDateString().Equals(DateTime.Today.ToShortDateString()) && x.Tryb != null);
            foreach (var item in wejscia)
            {
                item.Status = wejscia.Where(x => x.Karta == item.Karta).Count()/2==0 ?false:true;
            }


            foreach(var itemWej in wejscia)
            {
                foreach(var itemPrac in pracownicy)
                {
                    if (itemPrac.Pass4 != null) {
                        if (itemWej.Karta.Equals(itemPrac.Pass4))
                        {
                            itemWej.Pracownik = itemPrac.Imie + " " + itemPrac.Nazwisko;
                        }

                     }

                }
            }

            foreach(var itemTryb in wejscia)
            {
                foreach(var item in wejsciaAll)
                {
                    int roznica = Int32.Parse(item.Czas) - Int32.Parse(itemTryb.Czas) > 0 && Int32.Parse(item.Czas) - Int32.Parse(itemTryb.Czas) < 15 ? (int)item.Typ : 0;
                    if (roznica != 0)
                    {
                        itemTryb.Typ = roznica;
                    }
                }
            }


            return PartialView("_List",wejscia.ToList());
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
