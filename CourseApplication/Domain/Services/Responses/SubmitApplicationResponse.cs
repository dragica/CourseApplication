using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApplication.Domain.Services.Responses
{
    public class SubmitApplicationResponse : BaseResponse<int>
    {
        public SubmitApplicationResponse(int applicationId) : base(applicationId)
        { }

        public SubmitApplicationResponse(string message) : base(message)
        { }
    }
}
