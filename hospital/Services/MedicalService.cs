using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using hospital.Entities;
using hospital.Exceptions;
using hospital.Helpers;
using hospital.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace hospital.Services
{
    public interface IMedicalService {
        MedicalRecordDto Get(int id);
    }

    public class MedicalService : IMedicalService
    {
        private readonly HospitalDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtService _jwtService;
            private readonly IUserContextService _userContextService;
        public MedicalService(HospitalDbContext dbContext, IMapper mapper, IPasswordHasher<User> passwordHasher, IJwtService jwtService, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _userContextService = userContextService;
        }

     
        public MedicalRecordDto Get(int id)
        {
            var patient = _dbContext.Users.FirstOrDefault(u => u.Id == id && u.Role.Name == "Patient");
            if (patient is null)
                throw new NotFoundException("Patient not found");

            var patientId = id;
            var doctorId = _userContextService.GetUserId;

            var record = _dbContext.MedicalRecords
                .Include(m => m.Patient)
                .Include(m => m.Patient.Role)
                .FirstOrDefault(m => m.DoctorId == doctorId && m.PatientId == patientId);

            return _mapper.Map<MedicalRecordDto>(record);
        }
    }
}
