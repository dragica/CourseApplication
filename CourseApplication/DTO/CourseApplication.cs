using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApplication.DTO
{
    public class CourseApplication
    {
        public int SelectedCourse { get; set; }

        public DateTime SelectedDate { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        [MaxLength(20)]
        public string CompanyPhone { get; set; }

        [Required]
        public string CompanyEmail { get; set; }

        public IEnumerable<Participant> Participants { get; set; }
    }
}
