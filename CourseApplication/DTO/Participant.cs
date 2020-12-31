using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApplication.DTO
{
    public class Participant
    {
        [MaxLength(50)]
        public string FullName { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }
        
        public string Email { get; set; }
    }
}
