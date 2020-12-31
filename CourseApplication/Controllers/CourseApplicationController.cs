using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseApplication.Domain.Models;
using CourseApplication.Domain.Services;
using CourseApplication.Domain.Services.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseApplicationController : ControllerBase
    {

        private readonly ICourseApplicationService _courseApplicationService;
        public CourseApplicationController(ICourseApplicationService courseApplicationService)
        {
            _courseApplicationService = courseApplicationService;
        }
        [HttpPost]
        public async Task<SubmitApplicationResponse> SubmitApplication([FromBody] DTO.CourseApplication courseApplication)
        {
            return await _courseApplicationService.SubmitApplicationAsync(courseApplication);
        }

    }
}
