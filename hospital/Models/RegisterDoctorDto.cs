using System;
using System.ComponentModel.DataAnnotations;

namespace hospital.Models
{
    public class RegisterDoctorDto
    {
        [Required]
        [MaxLength(250)]
        [MinLength(3)]
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
        public readonly int RoleId = 1;

    }

}
