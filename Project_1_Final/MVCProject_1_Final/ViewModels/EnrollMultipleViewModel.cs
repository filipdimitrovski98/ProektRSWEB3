using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_1_Final.Models;

namespace Project_1_Final.ViewModels
{
    public class EnrollMultipleViewModel
    {
        public Enrollment NewEnrollment { get; set; }
        public IEnumerable<double> SelectedStudents { get; set; }
        public IEnumerable<SelectListItem> StudentsList { get; set; }

    }
}
