using System;
using AutoMapper;
using hospital.Entities;
using hospital.Models;

namespace hospital.Services
{
    public interface IDoctorService {
        int Create(CreateDoctorDto dto);
    }

    public class DoctorService : IDoctorService
    {
        private readonly HospitalDbContext _dbContext;
        private readonly IMapper _mapper;
        public DoctorService(HospitalDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public int Create(CreateDoctorDto dto)
        {
            var user = _mapper.Map<User>(dto);
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return user.Id;
        }
    }
}
