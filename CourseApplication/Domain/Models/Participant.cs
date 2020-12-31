using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApplication.Domain.Models
{
    public class Participant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public int CompanyId { get; set; }

        public Company Company { get; set; }
    }
}
