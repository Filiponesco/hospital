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
    public interface IPatientService {
        int Create(CreatePatientDto dto);
        LoginResponseDto GenerateJwt(LoginDto dto);
        IEnumerable<PatientDto> GetAllFromDoctor();
    }

    public class PatientService : IPatientService
    {
        private readonly HospitalDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtService _jwtService;
            private readonly IUserContextService _userContextService;
        public PatientService(HospitalDbContext dbContext, IMapper mapper, IPasswordHasher<User> passwordHasher, IJwtService jwtService, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _userContextService = userContextService;
        }

        public int Create(CreatePatientDto dto)
        {

            var newUser = new User()
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                RoleId = dto.RoleId
            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, RandomStringGenerator.RandomString(6));

            newUser.Password = hashedPassword;
            var savedUser = _dbContext.Users.Add(newUser);

            int? doctorId = _userContextService.GetUserId;
            if (doctorId == null)
            {
                throw new ForbidException();
            }

            var savedUserId = savedUser.Property(u => u.Id).CurrentValue;

            var medicalRecord = new MedicalRecord
            {
                PatientId = savedUserId,
                DoctorId = (int)doctorId,
                LastUpdateDate = DateTime.Now,
            };

            _dbContext.MedicalRecords.Add(medicalRecord);

            _dbContext.SaveChanges();
            return savedUserId;
        }

        public LoginResponseDto GenerateJwt(LoginDto dto) {
            return _jwtService.GenerateJwt(dto);
        }

        public IEnumerable<PatientDto> GetAllFromDoctor()
        {
            int? doctorId = _userContextService.GetUserId;
            var records = _dbContext
                .MedicalRecords
                .Include(m => m.Patient)
                .Include(m => m.Patient.Role)
                .ToList().FindAll(p => p.DoctorId == doctorId);

            List<User> patients = new();
            records.ForEach(r => patients.Add(r.Patient));


            var patientDtos = _mapper.Map<List<PatientDto>>(patients);

            return patientDtos;
        }
    }
}
