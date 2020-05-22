using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Project_1_Final.Models;

namespace Project_1_Final.ViewModels
{
    public class EnrollmentAttachmentViewModel
    {
        public Enrollment Enrollment { get; set; }
        public IFormFile Attachment { get; set; }
    }
}
