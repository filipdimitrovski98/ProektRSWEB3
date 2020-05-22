using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Project_1_Final.Models
{
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Course Id")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
        [Display(Name = "Student Id")]
        public double StudentId { get; set; }
        public Student Student { get; set; }
        public string Semester { get; set; }
        public int Year { get; set; }
        public int Grade { get; set; }
        [Display(Name = "Seminal Url")]
        public string SeminalUrl { get; set; }
        [Display(Name = "Project Url")]
        public string ProjectUrl { get; set; }
        [Display(Name = "Exam Points")]
        public int ExamPoints { get; set; }
        [Display(Name = "Seminal Points")]
        public int SeminalPoints { get; set; }
        [Display(Name = "Project Points")]
        public int ProjectPoints { get; set; }
        [Display(Name = "Additional Points")]
        public int AdditionalPoints { get; set; }
        [Display(Name = "Finish Date")]
        public DateTime FinishDate { get; set; }
        [NotMapped]
        public string SeminalAttachment { get; set; }
    }
}
