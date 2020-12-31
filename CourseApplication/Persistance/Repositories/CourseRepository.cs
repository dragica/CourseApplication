using CourseApplication.Domain.Models;
using CourseApplication.Domain.Repositories;
using CourseApplication.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApplication.Persistance.Repositories
{
    public class CourseRepository : BaseRepository, ICourseRepository
    {
        public CourseRepository(AppDbContext context) : base(context) { }
        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Course.ToListAsync();
        }

        public async Task<IEnumerable<CourseDate>> GetCourseDatesAsync(int id)
        {
            return await _context.CourseDate.Where(c => c.CourseId == id).ToListAsync();
        }

        public async Task<CourseDate> GetCourseDateAsync(int id, DateTime date)
        {
            return await _context.CourseDate.FirstOrDefaultAsync(c => c.CourseId == id && c.Date == date);
        }
    }
}
