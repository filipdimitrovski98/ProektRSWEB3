using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
namespace Project_1_Final.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Degree { get; set; }
        [Display(Name = "Academic Rank")]
        public string AcademicRank { get; set; }
        [Display(Name = "Office Number")]
        public string OfficeNumber { get; set; }
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }
        [Display(Name = "First Courses")]
        public ICollection<Course> Courses { get; set; }
        [Display(Name = "Second Courses")]
        public ICollection<Course> CoursesSec { get; set; }
        public string FullName
        {
            get { return String.Format("{0} {1}", FirstName, LastName); }
        }
        [NotMapped]
        public string ProfilePicture { get; set; }
    }
}
