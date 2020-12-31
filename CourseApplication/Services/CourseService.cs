using CourseApplication.Domain.Models;
using CourseApplication.Domain.Repositories;
using CourseApplication.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApplication.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            this._courseRepository = courseRepository;
        }

        public async Task<IEnumerable<DTO.Course>> GetAllAsync()
        {
            var courses = await _courseRepository.GetAllAsync();

            return courses.Select(c => new DTO.Course
            {
                Id = c.Id,
                Name = c.Name
            });
        }

        public async Task<IEnumerable<DTO.CourseDate>> GetCourseDatesAsync(int id)
        {
            var courseDates = await _courseRepository.GetCourseDatesAsync(id);

            return courseDates.Select(c => new DTO.CourseDate
            {
                CourseId = c.CourseId,
                Date = c.Date
            });
        }

        public async Task<DTO.CourseDate> GetCourseDateAsync(int id, DateTime date)
        {
            var courseDate = await _courseRepository.GetCourseDateAsync(id, date);

            return new DTO.CourseDate
            {
                CourseId = courseDate.CourseId,
                Date = courseDate.Date
            };
        }
    }
}
