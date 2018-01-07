namespace RCP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Przypisania")]
    public partial class Przypisania
    {
        public int Id { get; set; }

        public int IdPracownika { get; set; }

        public DateTime Od { get; set; }

        public DateTime? Do { get; set; }

        public int IdKierownika { get; set; }

        public int? IdCC { get; set; }

        public int? IdCommodity { get; set; }

        public int? IdArea { get; set; }

        public int? IdPosition { get; set; }

        public int? IdKierownikaRq { get; set; }

        public DateTime? DataRq { get; set; }

        [StringLength(200)]
        public string UwagiRq { get; set; }

        public int? IdKierownikaAcc { get; set; }

        public DateTime? DataAcc { get; set; }

        [StringLength(200)]
        public string UwagiAcc { get; set; }

        public int Status { get; set; }

        public int Typ { get; set; }

        public DateTime? DoMonit { get; set; }

        public int? RcpStrefaId { get; set; }

        public int? IdKierownikaRqZast { get; set; }

        public int? IdKierownikaAccZast { get; set; }
    }
}
