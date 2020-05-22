using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_1_Final.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project_1_Final.ViewModels
{
    public class CourseStudentEditViewModel
    {
        public Course Course { get; set; }
        public IEnumerable<double> SelectedStudents { get; set; }
        public IEnumerable<SelectListItem> StudentList { get; set; }    
    }
}
