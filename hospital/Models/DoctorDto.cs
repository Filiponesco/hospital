using System;
using System.ComponentModel.DataAnnotations;
using hospital.Entities;

namespace hospital.Models
{
    public class DoctorDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }
    }

}
