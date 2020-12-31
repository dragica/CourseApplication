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

namespace CourseApplication.Tests
{
    public class CategoryServiceTests
    {
        private readonly CourseService _sut;
        private readonly Mock<ICourseRepository> _courseRepoMock = new Mock<ICourseRepository>();
        public CategoryServiceTests()
        {
            _sut = new CourseService(_courseRepoMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnCourses_WhenCoursesExist()
        {
            // Arrange
            var courses = new List<Course>()
            {
                new Course(){
                    Id = 3,
                    Name = "OOP"
                }
            };

            var coursesDTO = courses.Select(c => new CourseApplication.DTO.Course
            {
                Id = c.Id,
                Name = c.Name
            });

            _courseRepoMock.Setup(c => c.GetAllAsync()).ReturnsAsync(courses);

            // Act
            var result = await _sut.GetAllAsync();

            // Assert
            Assert.Equal(coursesDTO.Count(), result.Count());
            Assert.Equal(coursesDTO.First().Id, result.First().Id);
            Assert.Equal(coursesDTO.First().Name, result.First().Name);
        }

        [Fact]
        public async Task GetAll_ShouldReturnNothing_WhenCoursesDontExist()
        {
            // Arrange
            var courses = new List<Course>();

            _courseRepoMock.Setup(c => c.GetAllAsync()).ReturnsAsync(courses);

            // Act
            var result = await _sut.GetAllAsync();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetCourseDates_ShouldReturnAllCourseDates()
        {
            // Arrange
            var courseDates = new List<CourseDate>(){
                new CourseDate{ CourseId = 3, Date = DateTime.Parse("08/18/2018")}
            };

            var courseDatesDTO = courseDates.Select(c => new CourseApplication.DTO.CourseDate
            {
                CourseId = c.Id,
                Date = c.Date
            });

            var courseId = 3;

            _courseRepoMock.Setup(repo => repo.GetCourseDatesAsync(It.IsAny<int>())).ReturnsAsync(courseDates);

            // Act
            var result = await _sut.GetCourseDatesAsync(courseId);

            // Assert
            Assert.Equal(courseDatesDTO.Count(), result.Count());
            Assert.Equal(courseDatesDTO.First().Date, result.First().Date);
        }
    }
}