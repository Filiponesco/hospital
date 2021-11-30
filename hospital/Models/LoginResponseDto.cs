using System;
using System.ComponentModel.DataAnnotations;

namespace hospital.Models
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public UserDto User { get; set; }
    }

}
