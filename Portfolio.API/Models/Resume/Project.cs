using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portfolio.API.Resume
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}