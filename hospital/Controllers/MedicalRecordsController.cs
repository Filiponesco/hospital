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
    [Route("medical")]
    [ApiController]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly IMedicalService _medicalService;


        public MedicalRecordsController(IMedicalService medicalService)
        {
            _medicalService = medicalService;
        }
        [HttpGet("{patientId}")]
        [Authorize(Roles = "Doctor")] // TODO authorize: only for doctor for specified patient
        public ActionResult<PatientDto> Get([FromRoute] int patientId)
        {
            var patientDto = _medicalService.Get(patientId);

            return Ok(patientDto);
        }
    }
}
