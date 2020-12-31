using CourseApplication.Domain.Models;
using CourseApplication.Domain.Repositories;
using CourseApplication.Domain.Services;
using CourseApplication.Domain.Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApplication.Services
{
    public class CourseApplicationService : ICourseApplicationService
    {
        private readonly ICourseApplicationRepository _courseApplicationRepository;
        private readonly ICourseRepository _courseRepository;
        public CourseApplicationService(ICourseApplicationRepository courseApplicationRepository, ICourseRepository courseRepository)
        {
            _courseApplicationRepository = courseApplicationRepository;
            _courseRepository = courseRepository;
        }
        public async Task<SubmitApplicationResponse> SubmitApplicationAsync(DTO.CourseApplication application)
        {
            var (success, message) = ValidateApplication(application);
            if (!success)
                return new SubmitApplicationResponse($"{message}");

            try
            {
                var courseDate = await _courseRepository.GetCourseDateAsync(application.SelectedCourse, application.SelectedDate);

                var courseApplication = new Domain.Models.CourseApplication
                {

                    CourseDateId = courseDate.Id,
                    Company = new Company
                    {
                        Name = application.CompanyName,
                        Phone = application.CompanyPhone,
                        Email = application.CompanyEmail,
                        Participants = application.Participants.Select(p => new Participant
                        {
                            FullName = p.FullName,
                            Phone = p.Phone,
                            Email = p.Email
                        }).ToList()
                    }
                };

                var result = await _courseApplicationRepository.AddAsync(courseApplication);

                return new SubmitApplicationResponse(result.Id);
            }
            catch (Exception ex)
            {
                return new SubmitApplicationResponse($"An error occurred when submiting the application: {ex.Message}");
            }
        }

        private (bool success, string message) ValidateApplication(DTO.CourseApplication application)
        {
            if (string.IsNullOrEmpty(application.CompanyName)) return (false, "Invalid company name");
            if (string.IsNullOrEmpty(application.CompanyPhone)) return (false, "Invalid company phone");
            if (string.IsNullOrEmpty(application.CompanyEmail)) return (false, "Invalid company email");
            foreach (var participant in application.Participants)
                if (string.IsNullOrEmpty(participant.FullName)) return (false, "Invalid participant name");
            return (true, "");
        }
    }
}
