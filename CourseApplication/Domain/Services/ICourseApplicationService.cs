using CourseApplication.Domain.Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApplication.Domain.Services
{
    public interface ICourseApplicationService
    {
        Task<SubmitApplicationResponse> SubmitApplicationAsync(DTO.CourseApplication application);
    }
}
