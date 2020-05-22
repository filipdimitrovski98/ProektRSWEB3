using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_1_Final.Data;
using Project_1_Final.Models;
using Project_1_Final.ViewModels;

namespace Project_1_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesApiController : ControllerBase
    {
        private readonly Project_1_FinalContext _context;

        public CoursesApiController(Project_1_FinalContext context)
        {
            _context = context;
        }

        // GET: api/CoursesApi
        [HttpGet]
        public List<Course> GetCourse(string titleString, string programmeString, int semestarInt = 0)
        {
            IQueryable<Course> courses = _context.Course.AsQueryable();
            if (!string.IsNullOrEmpty(titleString))
            {
                courses = courses.Where(s => s.Title.Contains(titleString));
            }
            if (!string.IsNullOrEmpty(programmeString))
            {
                courses = courses.Where(s => s.Programme == programmeString);
            }
            if (semestarInt != 0)
            {
                courses = courses.Where(s => s.Semester == semestarInt);
            }
            return courses.ToList();
        }

        // GET: api/CoursesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _context.Course.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // PUT: api/CoursesApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse([FromRoute] int id,[FromBody] CourseStudentEditViewModel model  )
        {
            if (id != model.Course.Id)
            {
                return BadRequest();
            }

            _context.Entry(model.Course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                IEnumerable<double> listStudents = model.SelectedStudents;
                IQueryable<Enrollment> toBeRemoved = _context.Enrollment.Where(s => !listStudents.Contains(s.StudentId) && s.CourseId == id);
                _context.Enrollment.RemoveRange(toBeRemoved);
                IEnumerable<double> existStudents = _context.Enrollment.Where(s => listStudents.Contains(s.StudentId) && s.CourseId == id).Select(s => s.StudentId);
                IEnumerable<double> newStudents = listStudents.Where(s => !existStudents.Contains(s));
                foreach (int studentId in newStudents)
                    _context.Enrollment.Add(new Enrollment { StudentId = studentId, CourseId = id });
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CoursesApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            _context.Course.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
        }

        // DELETE: api/CoursesApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Course>> DeleteCourse(int id)
        {
            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Course.Remove(course);
            await _context.SaveChangesAsync();

            return course;
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.Id == id);
        }
    }
}
