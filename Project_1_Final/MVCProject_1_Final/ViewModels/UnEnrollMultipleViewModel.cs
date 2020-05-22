using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_1_Final.Models;

namespace Project_1_Final.ViewModels
{
    public class UnEnrollMultipleViewModel
    {
        public IEnumerable<SelectListItem> StudentsList { get; set; }
        public Enrollment NewEnrollment { get; set; }
        public IEnumerable<double> SelectedStudents { get; set; }
    }
}
