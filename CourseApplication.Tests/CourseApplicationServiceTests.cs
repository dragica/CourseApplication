using Moq;
using CourseApplication.Domain.Models;
using CourseApplication.Domain.Repositories;
using CourseApplication.Domain.Services;
using CourseApplication.Services;
using System;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using CourseApplication.Persistance.Repositories;

namespace CourseApplication.Tests
{
    public class CourseApplicationTests
    {
        private readonly CourseApplicationService _sut;
        private readonly Mock<ICourseApplicationRepository> _courseApplicationRepoMock = new Mock<ICourseApplicationRepository>();
        private readonly Mock<ICourseRepository> _courseRepoMock = new Mock<ICourseRepository>();

        public CourseApplicationTests()
        {
            _sut = new CourseApplicationService(_courseApplicationRepoMock.Object, _courseRepoMock.Object);
        }


        [Fact]
        public async Task SubmitApplication_ShouldReturnNewApplication_WhenApplicationIsValid()
        {
            // Arrange
            var applicationDTO = new DTO.CourseApplication
            {
                SelectedCourse = 3,
                SelectedDate = DateTime.Parse("08/18/2018"),
                CompanyName = "Pro IT",
                CompanyPhone = "45235678",
                CompanyEmail = "test@mail.com",
                Participants = new List<DTO.Participant>()
            };

            var application = new Domain.Models.CourseApplication()
            {
                Id = 1,
                CourseDateId = 3,
                CompanyId = 3
            };

            var cd = new CourseDate { Id = 3, CourseId = 3, Date = DateTime.Parse("08/18/2018") };

            var submitApplicationResponse = new Domain.Services.Responses.SubmitApplicationResponse(1);


            _courseApplicationRepoMock.Setup(repo => repo.AddAsync(It.IsAny<Domain.Models.CourseApplication>())).ReturnsAsync(application);
            _courseRepoMock.Setup(repo => repo.GetCourseDateAsync(It.IsAny<int>(), It.IsAny<DateTime>())).ReturnsAsync(cd);

            // Act
            var result = await _sut.SubmitApplicationAsync(applicationDTO);

            // Assert
            _courseRepoMock.Verify(x => x.GetCourseDateAsync(applicationDTO.SelectedCourse, applicationDTO.SelectedDate));
            Assert.True(result.Success);
            Assert.Equal(submitApplicationResponse.Resource, result.Resource);
        }

        [Fact]
        public async Task SubmitApplication_ShouldReturnNewApplication_WhenApplicationIsInvalid()
        {
            // Arrange
            var applicationDTO = new DTO.CourseApplication
            {
                SelectedCourse = 3,
                SelectedDate = DateTime.Parse("08/18/2018"),
                CompanyName = "",
                CompanyPhone = "45235678",
                CompanyEmail = "test@mail.com"
            };

            var application = new Domain.Models.CourseApplication
            {
                Id = 1,
                CompanyId = 3
            };

            var submitApplicationResponse = new Domain.Services.Responses.SubmitApplicationResponse("Invalid company name");

            _courseApplicationRepoMock.Setup(repo => repo.AddAsync(It.IsAny<Domain.Models.CourseApplication>())).ReturnsAsync(() => null);

            // Act
            var result = await _sut.SubmitApplicationAsync(applicationDTO);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(submitApplicationResponse.Resource, result.Resource);
        }
    }
}