using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_1_Final.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project_1_Final.ViewModels
{
    public class TeacherSearchViewModel
    {
        public IList<Teacher> Teachers { get; set; }
        public string FName { get; set; }
        public string degree { get; set; }
        public string academicRank { get; set; }
    }
}
