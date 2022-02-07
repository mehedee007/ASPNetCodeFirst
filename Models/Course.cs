using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OctoberProjectCodeFirst.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string CourseDuration { get; set; }
        [DataType(DataType.Currency)]
        public Nullable<int> CourseFee { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal FeeWithVat 
        {
            get { return (decimal)(CourseFee + (CourseFee * .15)); }
            set { } 
        }

        public virtual ICollection<Trainee> Trainees { get; set; }
        public virtual ICollection<Trainer> Trainers { get; set; }
        public virtual ICollection<TSP> TSPs { get; set; }
    }
}