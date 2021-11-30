using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hospital.Entities;
using hospital.Exceptions;
using hospital.Models;
using hospital.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace hospital.Controllers
{
    [Route("patient")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;


        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }
        [Authorize(Roles = "Doctor")]
        [HttpPost("create")]
        public ActionResult Register([FromBody] CreatePatientDto dto)
        {
            int id = _patientService.Create(dto);
            return Created($"/Patient/{id}", null);
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            LoginResponseDto response = _patientService.GenerateJwt(dto);
            return Ok(response);
        }
        [HttpGet]
        [Authorize(Roles = "Doctor")]
        public ActionResult<IEnumerable<PatientDto>> GetAllFromDoctor()
        {
            var patientDtos = _patientService.GetAllFromDoctor();

            return Ok(patientDtos);
        }
    }
}
