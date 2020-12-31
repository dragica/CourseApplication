using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApplication.Domain.Models
{
    public class CourseApplication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CourseDateId { get; set; }

        public CourseDate CourseDate { get; set; }

        public int CompanyId { get; set; }

        public Company Company { get; set; }

    }
}
