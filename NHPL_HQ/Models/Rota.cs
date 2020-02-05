using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdentitySample.Models;

namespace NHPL_HQ.Models
{
    public class Rota
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public DateTime UploadedTimeStamp { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}