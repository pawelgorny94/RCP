namespace RCP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;


    [Table("PlanPracy")]
    public partial class PlanPracy
    {
        private RcpContext db = new RcpContext();
        public int Id { get; set; }

        public int IdPracownika { get; set; }

        [Display(Name = "Data przypisania planu pracy")]
        public DateTime Data { get; set; }

        public int? IdZmiany { get; set; }

        public DateTime? DataZm { get; set; }

        public int? IdKierownikaZm { get; set; }

        [Display(Name = "Data obowi¹zywania od")]
        public DateTime? CzasIn { get; set; }

        [Display(Name = "Data obowi¹zywania do")]
        public DateTime? CzasOut { get; set; }

        public int? Czas { get; set; }

        public int? CzasZm { get; set; }

        public int? NadgodzinyDzien { get; set; }

        public int? Nocne { get; set; }

        public int? Absencja { get; set; }

        public bool Akceptacja { get; set; }

        public DateTime? DataAcc { get; set; }

        public int? IdKierownikaAcc { get; set; }

        public bool? k_CzasIn { get; set; }

        public bool? k_CzasOut { get; set; }

        public bool? k_CzasZm { get; set; }

        public bool? k_NadgodzinyDzien { get; set; }

        public bool? k_Nocne { get; set; }

        [StringLength(200)]
        public string Uwagi { get; set; }

        public int? IdZmianyKorekta { get; set; }

        public int? NadgodzinyNoc { get; set; }

        public bool? k_NadgodzinyNoc { get; set; }

        public int? Alerty { get; set; }

        public int? n50 { get; set; }

        public int? n100 { get; set; }

        public int? d50 { get; set; }

        public int? d100 { get; set; }

        public int? WymiarKorekta { get; set; }

        public int? Wymiar { get; set; }

        public string Pracownik { get
            {
                return db.Pracownicy.Where(x => x.Id.Equals(IdPracownika)).FirstOrDefault().Imie +" "+
                    db.Pracownicy.Where(x => x.Id.Equals(IdPracownika)).FirstOrDefault().Nazwisko;
            } }

        public string NazwaZmiany
        {
            get
            {
                return db.Zmiany.Where(x => x.Id==IdZmiany).FirstOrDefault().Nazwa + " " +
                    db.Zmiany.Where(x => x.Id == IdZmiany).FirstOrDefault().Od.ToShortTimeString() +" "+
                    db.Zmiany.Where(x => x.Id == IdZmiany).FirstOrDefault().Do.ToShortTimeString();
            }
        }
    }
}
