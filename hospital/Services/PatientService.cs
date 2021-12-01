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
        LoginResponseDto GenerateJwt(LoginDto dto);
        int Register(RegisterPatientDto dto);
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

        public LoginResponseDto GenerateJwt(LoginDto dto) {
            return _jwtService.GenerateJwt(dto);
        }

        public int Register(RegisterPatientDto dto)
        {
            var patient = _dbContext.Users.FirstOrDefault(u => u.Email == dto.Email && u.RoleId == 2 && u.Password == null);

            if (patient is null) {

                throw new NotFoundException("Not found patient. Before register, doctor has to create an account for patient with this email.");
            }

            var hashedPassword = _passwordHasher.HashPassword(patient, dto.Password);

            patient.Password = hashedPassword;
            _dbContext.SaveChanges();
            return patient.Id;
        }
    }
}
