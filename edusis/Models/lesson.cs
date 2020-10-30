using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace edusis.Models
{
    public class lesson
    {
        public int lessonID { get; set; }
        public string lessonName { get; set; }
        public string lessonDescription { get; set; }
        public string lessonActivite { get; set; }
    }
}