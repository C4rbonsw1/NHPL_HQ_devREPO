using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHPL_HQ.Models
{
    public class UploadedRota
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string StaffName { get; set; }
        public DateTime ShiftDate { get; set; }
        public bool ShiftAvailability { get; set; }
    }
}