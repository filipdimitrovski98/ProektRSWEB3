using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_1_Final.Data;
using Project_1_Final.Models;
using Project_1_Final.ViewModels;

namespace Project_1_Final.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly Project_1_FinalContext _context;

        public EnrollmentsController(Project_1_FinalContext context)
        {
            _context = context;
        }

        // GET: Enrollments
        public async Task<IActionResult> Index()
        {
            var Project_1_FinalContext = _context.Enrollment.Include(e => e.Course).Include(e => e.Student);
            return View(await Project_1_FinalContext.ToListAsync());
        }

        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollment
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: Enrollments/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Title");
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FirstName");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseId,StudentId,Semester,Year,Grade,SeminalUrl,ProjectUrl,ExamPoints,SeminalPoints,ProjectPoints,AdditionalPoints,FinishDate")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Title", enrollment.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FirstName", enrollment.StudentId);
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollment.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Title", enrollment.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FirstName", enrollment.StudentId);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseId,StudentId,Semester,Year,Grade,SeminalUrl,ProjectUrl,ExamPoints,SeminalPoints,ProjectPoints,AdditionalPoints,FinishDate")] Enrollment enrollment)
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

        // GET: Enrollments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollment
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollment = await _context.Enrollment.FindAsync(id);
            _context.Enrollment.Remove(enrollment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollment.Any(e => e.Id == id);
        }
        public async Task<IActionResult> ListAllCourses()
        {
            return View(await _context.Course.ToListAsync());
        }
        public async Task<IActionResult> EnrollMultiple(int? id)
        {
            var course = await _context.Course.Where(m => m.Id == id).Include(m => m.Students).FirstOrDefaultAsync();
            var vm = new EnrollMultipleViewModel
            {
                StudentsList = new MultiSelectList(_context.Student.OrderBy(s => s.FirstName), "Id", "FullName"),
                SelectedStudents = course.Students.Select(sa => sa.StudentId)
            };
            ViewData["CourseName"] = _context.Course.Where(c => c.Id == id).Select(c => c.Title).FirstOrDefault();
            ViewData["chosenId"] = id;
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> EnrollMultiple(int id,EnrollMultipleViewModel vm)
        {
            if(id != vm.NewEnrollment.CourseId)
            {
                return NotFound();
            }
            IEnumerable<double> listStudents = vm.SelectedStudents;
            IEnumerable<double> existStudents = _context.Enrollment.Where(s => listStudents.Contains(s.StudentId) && s.CourseId == id).Select(s => s.StudentId);
            IEnumerable<double> newStudents = listStudents.Where(s => !existStudents.Contains(s));
            foreach (double sid in newStudents)
            {
                _context.Enrollment.Add(new Enrollment { StudentId = sid, CourseId = id, Year = vm.NewEnrollment.Year, Semester = vm.NewEnrollment.Semester });
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
         }
        public async Task<IActionResult> UnEnrollMultiple(int? id)
        {
            var course = await _context.Course.Where(m => m.Id == id).Include(m => m.Students).FirstOrDefaultAsync();
            IEnumerable<Student> students = _context.Enrollment.Where(m => m.CourseId == id).Select(sa => sa.Student);
            var vm = new UnEnrollMultipleViewModel
            {
                StudentsList = new MultiSelectList(students.OrderBy(s => s.Id), "Id", "FullName"),
                SelectedStudents = course.Students.Select(sa => sa.StudentId)
            };
            return View(vm);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnEnrollMultiple(int id, UnEnrollMultipleViewModel vm)
        {
            IEnumerable<double> listStudents = vm.SelectedStudents;
            IEnumerable<Enrollment> toBeRemoved = _context.Enrollment.Where(s => listStudents.Contains(s.StudentId) && s.CourseId == id);
            foreach (var enr in toBeRemoved)
            {
                _context.Enrollment.Remove(enr);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        


    }
}