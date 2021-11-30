using System;
using System.ComponentModel.DataAnnotations;
using hospital.Entities;

namespace hospital.Models
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual Role Role { get; set; }
    }

}
