using System;
using AutoMapper;
using hospital.Entities;
using hospital.Models;
using Microsoft.AspNetCore.Identity;

namespace hospital.Services
{
    public interface IDoctorService {
        int Create(CreateDoctorDto dto);
        CreateDoctorResponseDto GenerateJwt(LoginDoctorDto dto);
    }

    public class DoctorService : IDoctorService
    {
        private readonly HospitalDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtService _jwtService;
        public DoctorService(HospitalDbContext dbContext, IMapper mapper, IPasswordHasher<User> passwordHasher, IJwtService jwtService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public int Create(CreateDoctorDto dto)
        {

            var newUser = new User()
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                RoleId = dto.RoleId
            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);

            newUser.Password = hashedPassword;
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
            return newUser.Id;
        }

        public CreateDoctorResponseDto GenerateJwt(LoginDoctorDto dto) {
            return _jwtService.GenerateJwt(dto);
        }

    }
}
