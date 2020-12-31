using CourseApplication.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApplication.Domain.Repositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllAsync();

        Task<IEnumerable<CourseDate>> GetCourseDatesAsync(int id);

        Task<CourseDate> GetCourseDateAsync(int id, DateTime date);
    }
}
