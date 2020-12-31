using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApplication.Domain.Repositories
{
    public interface ICourseApplicationRepository
    {
        Task<Models.CourseApplication> AddAsync(Models.CourseApplication courseApplication);
    }
}
