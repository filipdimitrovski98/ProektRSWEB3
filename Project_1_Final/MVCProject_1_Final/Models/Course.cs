using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_1_Final.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int Credits { get; set; }
        [Required]
        public int Semester { get; set; }
        public string Programme { get; set; }
        [Display(Name = "Education Level")]
        public string EducationLevel { get; set; }
        [Display(Name = "First Teacher Id")]
        [ForeignKey("Standard")]
        public int FirstTeacherId { get; set; }
        public Teacher FirstTeacher { get; set; }
        [Display(Name = "Second Teacher Id")]
        [ForeignKey("Standard")]
        public int SecondTeacherId { get; set; }
        public Teacher SecondTeacher { get; set; }
        public ICollection<Enrollment> Students { get; set; }
    }
}
