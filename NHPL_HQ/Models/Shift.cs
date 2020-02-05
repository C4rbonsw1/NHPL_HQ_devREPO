using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdentitySample.Models;

namespace NHPL_HQ.Models
{
    public class Shift
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public bool ShiftAvailability { get; set; }
        public DateTime ShiftDate { get; set; }
        public DateTime WeekStart { get; set; }
        public DateTime WeekEnd { get; set; }
        public virtual ApplicationUser Employee { get; set; }
        public virtual Practice Practice { get; set; }

    }
}