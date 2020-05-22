using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_1_Final.Data;
using Project_1_Final.Models;
using Project_1_Final.ViewModels;

namespace Project_1_Final.Controllers
{
    public class AdminController : Controller
    {
        private readonly Project_1_FinalContext _context;

        public AdminController(Project_1_FinalContext context)
        {
            _context = context;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
                IList<Course> courses = await _context.Course.ToListAsync();
                IList<Teacher> teachers = await _context.Teacher.ToListAsync();
                IList<Student> students = await _context.Student.ToListAsync();
                var adminVM = new AdminViewModel
                {
                    Courses = courses,
                    Teachers = teachers,
                    Students = students
                };
            return View(adminVM);
        }
    }
}
