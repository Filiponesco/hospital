using System;
using System.ComponentModel.DataAnnotations;

namespace hospital.Models
{
    public class RegisterPatientDto
    {
        [Required]
        [MaxLength(250)]
        [MinLength(3)]
        public string Email { get; set; }
        [Required]
        [MaxLength(250)]
        [MinLength(6)]
        public string Password { get; set; }

    }

}
