using System;
using System.ComponentModel.DataAnnotations;

namespace hospital.Models
{
    public class CreateDoctorDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(250)]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        [MaxLength(250)]
        [MinLength(1)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(250)]
        [MinLength(1)]
        public string LastName { get; set; }
    }

}
