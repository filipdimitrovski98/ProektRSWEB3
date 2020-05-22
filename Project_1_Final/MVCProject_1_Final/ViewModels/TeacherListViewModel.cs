using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_1_Final.Models;

namespace Project_1_Final.ViewModels
{
    public class TeacherListViewModel
    {
        public Teacher Teacher { get; set; }
        public IList<Course> Courses { get; set; }
    }
}
