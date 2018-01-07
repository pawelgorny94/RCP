using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RCP.Models
{
    public class Emlopyee
    {
       // public int Id { get; set; }
       
        public string Imie { get; set; }
        
        public string Nazwisko { get; set; }

        public Nullable<DateTime> Data { get; set; }

        public string Dzial  { get; set; }

        public string Stanowisko { get; set; }
    }
}