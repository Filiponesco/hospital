using System;
using AutoMapper;
using hospital.Entities;
using hospital.Models;

namespace hospital
{
    public class HospitalMappingProfile : Profile
    {
        public HospitalMappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, PatientDto>();
            CreateMap<MedicalRecord, MedicalRecordDto>();
        }
    }
}
