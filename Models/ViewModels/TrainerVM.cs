using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OctoberProjectCodeFirst.Models.ViewModels
{
    public class TrainerVM
    {
        public int TrainerID { get; set; }
        public string TrainerName { get; set; }
        public string TrainerContact { get; set; }
        public string TrainerEmail { get; set; }

        public bool IsActive { get; set; }

        public int CourseID { get; set; }
        public virtual Course Course { get; set; }
    }
}