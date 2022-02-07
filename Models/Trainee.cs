using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctoberProjectCodeFirst.Models
{
    public class Trainee
    {

        public int TraineeID { get; set; }
        [Required(ErrorMessage = "Plase write your Name")]
        [DisplayName("Trainee Name  ")]
        public string TraineeName { get; set; }
        public string Gender { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Date of Birth  ")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[Range(typeof(DateTime), "1990/01/01 12:00 AM", "2022/01/01 12:00 AM", ErrorMessage = "Date must be between 1990 and 2021")]
        public System.DateTime DOB { get; set; }
        //[Compare()]
        [DisplayName("Email Address")]

        [EmailAddress(ErrorMessage = "The format is not right")]
        [Remote("EmailExist", "Trainee", ErrorMessage = "Email already exists!")]
        public string TraineeEmail { get; set; }

        //[System.ComponentModel.DataAnnotations.CompareAttribute("TraineeEmail", ErrorMessage = "Email didn't match")]
        //[DisplayName("Confirm Email")]
        //[NotMapped]
        //public string ConfirmEmail { get; set; }
        [DataType(DataType.PhoneNumber)]
        [DisplayName("Trainee Contact  ")]
        [Phone(ErrorMessage = "Phone Number is not valid")]
        [MaxLength(11, ErrorMessage = "Contact number must be greater than 11 digit")]
        [MinLength(11, ErrorMessage = "Contact number can't be less than 11 digit")]
        public string TraineeContact { get; set; }
        //[DisplayFormat(NullDisplayText = "Not Assigned")]
        public int CourseID { get; set; }
        //[DisplayFormat(NullDisplayText = "Not Assigned")]
        [DisplayName("Trainee Image")]

        public string TraineeImage { get; set; }


        public virtual Course Course { get; set; }
    }
}