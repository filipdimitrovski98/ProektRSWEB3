using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_1_Final.Models;

namespace Project_1_Final.ViewModels
{
    public class StudentSearchViewModel
    {
        public IList<Student> Students { get; set; }
        public string FName { get; set; }
        public string Studentid { get; set; }
    }
}
