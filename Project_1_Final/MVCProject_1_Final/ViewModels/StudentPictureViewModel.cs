using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_1_Final.Models;
using Microsoft.AspNetCore.Http;

namespace Project_1_Final.ViewModels
{
    public class StudentPictureViewModel
    {
        public Student Student { get; set;  }
        public IFormFile ProfileImage { get; set; }
    }
}
