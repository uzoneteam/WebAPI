using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class SchedulerFormatted
    {
        public long SchedulerID { get; set; }
        public DateTime? EventStart { get; set; }
        public DateTime? EventEnd { get; set; }
        public string EventDescription { get; set; }
        public string EventSubject { get; set; }
        public int? EventLocationID { get; set; }
        public long SchoolID { get; set; }
        public string EventStartFormatted { get; set; }       
        
    }
}