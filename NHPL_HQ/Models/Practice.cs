using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdentitySample.Models;

namespace NHPL_HQ.Models
{
    public class Practice
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public IEnumerable<SelectList> List { get; set; }
    }
}