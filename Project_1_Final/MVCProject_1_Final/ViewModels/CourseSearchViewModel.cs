using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_1_Final.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Project_1_Final.ViewModels
{
    public class CourseSearchViewModel
    {
        public IList<Course> Courses { get; set; }
        public string searchtitle { get; set; }
        public int searchsemmestar { get; set; }
        public string searchprogramme { get; set; }

    }
}
