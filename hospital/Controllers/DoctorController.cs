using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hospital.Entities;
using hospital.Models;
using hospital.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace hospital.Controllers
{
    [Route("doctor")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;


        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody] CreateDoctorDto dto)
        {
            int id = _doctorService.Create(dto);
            return Created($"/Doctor/{id}", null);
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            LoginResponseDto response = _doctorService.GenerateJwt(dto);
            return Ok(response);
        }
    }
}
