using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace edusis.Models
{
    public class student
    {
        public int ogrID { get; set; }
        public string ogrName { get; set; }
        public string ogrSurname { get; set; }
        public int classID { get; set; }
        public string OgrTc { get; set; }
        public string ogrTel { get; set; }
        public string ogrMail { get; set; }
        public string ogrSehir { get; set; }
        public string ogrServis { get; set; }
        public string ogrCins { get; set; }
    }
}