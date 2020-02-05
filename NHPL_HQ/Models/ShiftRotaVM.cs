using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHPL_HQ.Models
{
    public class ShiftRotaVM
    {
        public Shift Shift { get; set; }
        public Rota Rota { get; set; }
        public Practice Practice { get; set; }
        public List<Shift> ShiftViewModel { get; set; }
        public List<Rota> RotaViewModel { get; set; }

        public List<Shift> ShiftDatesMonday { get; set; }
        public List<Shift> ShiftDatesTesuday { get; set; }
        public List<Shift> ShiftDatesWednesday { get; set; }
        public List<Shift> ShiftDatesThursday { get; set; }
        public List<Shift> ShiftDatesFriday {get; set; }
        public List<Shift> ShiftDatesSaturday { get; set; }
        public List<Shift> ShiftDatesSunday { get; set; }




    }
}