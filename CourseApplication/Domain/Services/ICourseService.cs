using CourseApplication.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApplication.Domain.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<DTO.Course>> GetAllAsync();

        Task<IEnumerable<DTO.CourseDate>> GetCourseDatesAsync(int id);

        Task<DTO.CourseDate> GetCourseDateAsync(int id, DateTime date);
    }
}
