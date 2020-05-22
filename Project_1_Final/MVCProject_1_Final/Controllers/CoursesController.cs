using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

using Project_1_Final.Data;
using Project_1_Final.Models;
using Project_1_Final.ViewModels;

namespace Project_1_Final.Controllers
{
    public class CoursesController : Controller
    {
        private readonly Project_1_FinalContext _context;

        public CoursesController(Project_1_FinalContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index(string title, int semesster, string programme)
        {
            IQueryable<Course> courses = _context.Course.AsQueryable();
            courses = _context.Course.Include(c => c.FirstTeacher).Include(c => c.SecondTeacher).Include(m => m.Students).ThenInclude(m => m.Student);
            if (!string.IsNullOrEmpty(title))
            {
                courses = courses.Where(s => s.Title.Contains(title));
            }
            if (semesster != 0)
            {
                courses = courses.Where(x => x.Semester.Equals(semesster));
            }
            if (!string.IsNullOrEmpty(programme))
            {
                courses = courses.Where(p => p.Programme.Contains(programme));
            }

            var courseSearchVM = new CourseSearchViewModel
            {
                Courses = await courses.ToListAsync(),
                searchtitle = title,
                searchsemmestar = semesster,
                searchprogramme = programme
            };

            return View(courseSearchVM);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .Include(m => m.FirstTeacher)
                .Include(m => m.SecondTeacher)
                .Include(m => m.Students).ThenInclude(m => m.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Credits,Semester,Programme,EducationLevel,FirstTeacherId,SecondTeacherId")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = _context.Course.Where(m => m.Id == id).Include(m => m.Students).First();
            course = await _context.Course.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            CourseStudentEditViewModel viewmodel = new CourseStudentEditViewModel
            {
                Course = course,
                StudentList = new MultiSelectList(_context.Student.OrderBy(s => s.Id), "Id", "FirstName"),
                SelectedStudents = course.Students.Select(sa => sa.StudentId)
            };
            if (viewmodel == null)
            {
                return NotFound();
            }
            return View(viewmodel);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseStudentEditViewModel viewmodel)
        {
            if (id != viewmodel.Course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewmodel.Course);
                    await _context.SaveChangesAsync();
                    IEnumerable<double> listStudents = viewmodel.SelectedStudents;
                    IQueryable<Enrollment> toBeRemoved = _context.Enrollment.Where(s => !listStudents.Contains(s.StudentId) && s.CourseId == id);
                    _context.Enrollment.RemoveRange(toBeRemoved);
                    IEnumerable<double> existStudents = _context.Enrollment.Where(s => listStudents.Contains(s.StudentId) && s.CourseId == id).Select(s => s.StudentId);
                    IEnumerable<double> newActors = listStudents.Where(s => !existStudents.Contains(s));
                    foreach (int actorId in newActors)
                        _context.Enrollment.Add(new Enrollment { StudentId = actorId, CourseId = id });

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(viewmodel.Course.Id))
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
            return View(viewmodel);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Course.FindAsync(id);
            _context.Course.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.Id == id);
        }
    }
}
