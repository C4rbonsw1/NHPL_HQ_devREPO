using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using IdentitySample.Models;

namespace NHPL_HQ.Models
{
    public class File
    {
        public int FileId { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public FileType FileType { get; set; }
        public string PracticeName { get; set; }
        //public DateTime RotaDate { get; set; }
        //public DateTime UploadTimeStamp { get; set; }
        public virtual Practice Practice { get; set; }
    }
}