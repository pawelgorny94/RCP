namespace RCP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Zmiany")]
    public partial class Zmiany
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Symbol { get; set; }

        [StringLength(200)]
        public string Nazwa { get; set; }

        public DateTime Od { get; set; }

        public DateTime Do { get; set; }

        public int Stawka { get; set; }

        public bool Visible { get; set; }

        [StringLength(50)]
        public string Ikona { get; set; }

        [StringLength(50)]
        public string Kolor { get; set; }

        public int TypZmiany { get; set; }

        [StringLength(100)]
        public string Nadgodziny { get; set; }

        public int? NadgodzinyDzien { get; set; }

        public int? NadgodzinyNoc { get; set; }

        public int Margines { get; set; }

        public bool ZgodaNadg { get; set; }

        public int? Kolejnosc { get; set; }

        public bool NowaLinia { get; set; }

        public bool Widoczna { get; set; }

        public bool HideZgoda { get; set; }

        public int Typ { get; set; }

        public bool? ObetnijOdGory { get; set; }

        public int? MarginesNadgodzin { get; set; }

        public int? Par1 { get; set; }

        public int? Par2 { get; set; }
    }
}
