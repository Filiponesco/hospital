using System;
using System.ComponentModel.DataAnnotations;

namespace hospital.Models
{
    public class CreateDoctorResponseDto
    {
        public string Token { get; set; }
        public DoctorDto Doctor { get; set; }
    }

}
