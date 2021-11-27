using System;
using AutoMapper;
using hospital.Entities;
using hospital.Helpers;
using hospital.Models;

namespace hospital
{
    public class HospitalMappingProfile : Profile
    {
        readonly IHashPassword _hashPassword;
        public HospitalMappingProfile()
        {

            CreateMap<CreateDoctorDto, User>()
               .ForMember(m => m.RoleId, c => c.MapFrom(s => 7)) // id from database
               .ForMember(m => m.Password, c => c.MapFrom(s => s.Password)); // TODO encypt password
        }
    }
}
