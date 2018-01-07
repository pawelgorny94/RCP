namespace RCP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Wejscia")]
    public partial class Wejscia
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(8)]
        public string Data { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(6)]
        public string Czas { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TID { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UID { get; set; }

        [StringLength(30)]
        public string Nazwa { get; set; }

        [StringLength(20)]
        public string Unikalny { get; set; }

        [StringLength(30)]
        public string Biuro { get; set; }

        [StringLength(30)]
        public string Post { get; set; }

        [StringLength(24)]
        public string Karta { get; set; }

        public int? Typ { get; set; }

        public int? Tryb { get; set; }

        public int? TypM { get; set; }

        public int? Resultat { get; set; }
        
        [NotMapped]
        public DateTime CzasWejscia { get; set; }

        [NotMapped]
        public bool Status { get; set; }

        [NotMapped]
        public string Pracownik { get; set; }
    }
}
