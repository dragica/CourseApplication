using CourseApplication.Domain.Repositories;
using CourseApplication.Domain.Models;
using CourseApplication.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApplication.Persistance.Repositories
{
    public class CourseApplicationRepository : BaseRepository, ICourseApplicationRepository
    {
        public CourseApplicationRepository(AppDbContext context) : base(context) { }

        public async Task<Domain.Models.CourseApplication> AddAsync(Domain.Models.CourseApplication courseApplication)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {

                await _context.CourseApplication.AddAsync(courseApplication);
                await _context.SaveChangesAsync();

                transaction.Commit();

                return courseApplication;
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}
