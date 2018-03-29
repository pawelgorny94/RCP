using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RCP.Models;
using System.Net.Mail;
using System.Text;
using System.Web.Security;
using System.Security.Cryptography;


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

        public ActionResult Settlement()
        {

            IEnumerable<Pracownicy> kierownicy = db.Pracownicy.Where(x => x.Status == 1 && x.Kierownik == true).ToList();
            RedirectToAction("SelectPrac");

            var kierownicys = kierownicy.OrderByDescending(x => x.Imie).Select(s => new SelectListItem
            { Value = s.Id.ToString(), Text = s.Imie + "   " + s.Nazwisko + " - " + s.KadryId });
            ViewBag.KierList = new SelectList(kierownicys, "Value", "Text", null);

            return View();
        }

        public JsonResult CalculateWorkTime(string idPrac, string wej, string wyj)
        {
            int idTo = int.Parse(idPrac);
            //DateTime wejj = DateTime.Parse(wej);
            //DateTime wyjj = DateTime.Parse(wyj);
            IEnumerable<Settlement> empl = db.Database.SqlQuery<Settlement>(String.Format("EXEC GetSettlementEmployye @IdPrac={0},@TimeFrom='{1}',@TimeTo='{2}'", idTo, wej, wyj)).ToList();
            int count = 0;
            foreach(var item in empl)
            {
                count += item.CountTime;
            }
            return Json(count.ToString(), JsonRequestBehavior.AllowGet);

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

        public ActionResult SendDataToMobile(string id)
        {
            string password = Membership.GeneratePassword(8, 2);
            string login = db.Pracownicy.Where(x => x.Id.ToString().Equals(id)).FirstOrDefault().Login;

            string hash = string.Empty;
            using (MD5 md5Hash = MD5.Create())
            {
                hash = GetMd5Hash(md5Hash, password);
                //if (VerifyMd5Hash(md5Hash, source, hash))
                //{
                //    //The hashes are the same
                //}
                //else
                //{
                //    //The hashes are not same
                //}
            }

            Pracownicy prac = db.Pracownicy.Where(x => x.Id.ToString().Equals(id)).FirstOrDefault();
            prac.Pass = hash;
            db.SaveChanges();

            string body = "Login: " + login + "\n" +
                          "Hasło: " + password + "\n";

            MailMessage mm = new MailMessage("rcpwcf001@gmail.com","pawelgorny94@gmail.com","Dane do logowania RCP Mobile",body);
            SendMail(mm);
            //mm.From = new MailAddress("rcpwcf001@gmail.com");
            //mm.To = new MailAddress("pawelgorny94@gmail.com");

           return RedirectToAction("Employee");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public static void SendMail(MailMessage Message)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("rcpwcf001@gmail.com", "P@ssw0rd_");
                client.Send(Message);
            }
            catch(Exception ex)
            {
                string m = ex.Message;
            }
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
