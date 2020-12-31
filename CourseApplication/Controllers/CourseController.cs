using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseApplication.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IEnumerable<DTO.Course>> GetAllAsync()
        {
            var courses = await _courseService.GetAllAsync();
            return courses;
        }

        [HttpGet("dates/{id}")]
        public async Task<IEnumerable<DTO.CourseDate>> GetCourseDatesAsync(int id)
        {
            var coursedates = await _courseService.GetCourseDatesAsync(id);
            return coursedates;
        }
    }
}
