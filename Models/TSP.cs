using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace OctoberProjectCodeFirst.Models
{
    public class TSP
    {
        public int TspID { get; set; }
        public string TspName { get; set; }
        public string TspAddress { get; set; }
        public string TspContact { get; set; }
        public string TspEmail { get; set; }
        public int TrainerID { get; set; }

        public int CourseID { get; set; }

        public virtual Course Course { get; set; }
        public virtual Trainer Trainer { get; set; }
    }
}