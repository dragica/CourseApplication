using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApplication.Domain.Models
{
    public class CourseDate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int CourseId { get; set; }

        public DateTime Date { get; set; }

        public Course Course { get; set; }

        public List<CourseApplication> CourseApplications { get; set; } = new List<CourseApplication>();
    }
}
