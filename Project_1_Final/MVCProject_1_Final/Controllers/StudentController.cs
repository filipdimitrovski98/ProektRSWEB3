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
using System.IO;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Project_1_Final.Controllers
{
    public class StudentController : Controller
    {
        private readonly Project_1_FinalContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public StudentController(Project_1_FinalContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Student
        public async Task<IActionResult> Index(int? id)
        {
            var student = await _context.Student.Where(m => m.Id == id).Include(m => m.Courses).FirstOrDefaultAsync();
            var enrollments = _context.Enrollment.Where(m => m.StudentId == id).Include(m => m.Course).Include(m => m.Student);
            var studentlistVM = new StudentListViewModel
            {
                Student = student,
                Enrollments = enrollments.ToList()
            };

            return View(studentlistVM);
        }

        
        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollment.FindAsync(id);
            if (enrollment == null )
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Title", enrollment.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FirstName", enrollment.StudentId);
            return View(enrollment);
        }
        
        // POST: Teacher/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile iff , [Bind("Id,CourseId,StudentId,Semester,Year,Grade,SeminalUrl,ProjectUrl,ExamPoints,SeminalPoints,ProjectPoints,AdditionalPoints,FinishDate")] Enrollment enrollment)
        {
            if (id != enrollment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Title", enrollment.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FirstName", enrollment.StudentId);
            return View(enrollment);
        }
        
        
        private bool StudentExists(double id)
        {
            return _context.Student.Any(e => e.Id == id);
        }
        private bool EnrollmentExists(int id)
        {
            return _context.Enrollment.Any(e => e.Id == id);
        }
        private string UploadedFile(IFormFile model)
        {
            string uniqueFileName = null;

            if (model != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        public IActionResult Attach(int? id)
        {
            var vm = new EnrollmentAttachmentViewModel
            {
                Enrollment = null,
                Attachment = null
            };
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Attach(int? id, IFormFile iff)
        {
            var vm = new EnrollmentAttachmentViewModel
            {
                Enrollment = await _context.Enrollment.FindAsync(id),
                Attachment = iff
            };
            string uniqueFileName = UploadedFile(vm.Attachment);
            vm.Enrollment.SeminalAttachment = uniqueFileName;
            _context.Update(vm.Enrollment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
