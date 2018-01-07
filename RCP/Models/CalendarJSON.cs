using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RCP.Models
{
    [Serializable]
    public class CalendarJSON
    {

        public int id { get; set; }
        public string title { get; set; }
        public  DateTime start { get; set; }
       // public string start { get; set; }
        public bool allDay { get; set; }
        public string className { get; set; }
    }
}