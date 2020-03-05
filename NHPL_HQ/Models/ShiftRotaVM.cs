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
        public File File { get; set; }
        public List<Shift> ShiftViewModel { get; set; }
        public List<Rota> RotaViewModel { get; set; }

        public List<Practice> PracticeList { get; set; }

        public IEnumerable<Shift> ShiftDatesMonday { get; set; }
        public IEnumerable<Shift> ShiftDatesTeusday { get; set; }
        public IEnumerable<Shift> ShiftDatesWednesday { get; set; }
        public IEnumerable<Shift> ShiftDatesThursday { get; set; }
        public IEnumerable<Shift> ShiftDatesFriday {get; set; }
        public IEnumerable<Shift> ShiftDatesSaturday { get; set; }
        public IEnumerable<Shift> ShiftDatesSunday { get; set; }

        




    }
}