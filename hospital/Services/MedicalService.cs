﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using hospital.Entities;
using hospital.Exceptions;
using hospital.Helpers;
using hospital.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace hospital.Services
{
    public interface IMedicalService
    {
        MedicalRecordDto Get(int id);
        MedicalRecordDto Get();
        Task<MedicalRecordDto> UpdateAsync(int id, UpdateMedicalRecordDto updateMedicalRecord);
    }

    public class MedicalService : IMedicalService
    {
        private readonly HospitalDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtService _jwtService;
        private readonly IUserContextService _userContextService;
        private readonly HttpClient _client;
        public MedicalService(HospitalDbContext dbContext, IMapper mapper, IPasswordHasher<User> passwordHasher, IJwtService jwtService, IUserContextService userContextService,
            HttpClient client)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _userContextService = userContextService;
            _client = client;
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

        public MedicalRecordDto Get()
        {
            var patientId = _userContextService.GetUserId;
            var record = _dbContext.MedicalRecords
                .Include(m => m.Patient)
                .Include(m => m.Patient.Role)
                .FirstOrDefault(m => m.Patient.Id == patientId);
            return _mapper.Map<MedicalRecordDto>(record);
        }

        public async Task<MedicalRecordDto> UpdateAsync(int id, UpdateMedicalRecordDto updateMedicalRecord)
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

            record.Pregnancies = updateMedicalRecord.Pregnancies;
            record.Glucose = updateMedicalRecord.Glucose;
            record.BloodPresure = updateMedicalRecord.BloodPresure;
            record.SkinThickness = updateMedicalRecord.SkinThickness;
            record.Insulin = updateMedicalRecord.Insulin;
            record.Bmi = updateMedicalRecord.Bmi;
            record.DiabetesPedigreeFunction = updateMedicalRecord.DiabetesPedigreeFunction;
            record.Age = updateMedicalRecord.Age;
            record.LastUpdateDate = DateTime.Now;
            var values = new Dictionary<string, string>
{

               { "Pregnancies", record.Pregnancies.ToString()},
                { "Glucose", record.Glucose.ToString() },
                { "SkinThickness", record.SkinThickness.ToString() },
                { "BMI", record.Bmi.ToString() },
                { "Age", record.Age.ToString() }
};

            var content = new FormUrlEncodedContent(values);

            var response = await _client.PostAsync("https://predictorknnpolsl.azurewebsites.net/predict", content);

            var responseString = await response.Content.ReadAsStringAsync();

            record.Outcome = responseString == "1";
            record.Filled = true;

            _dbContext.SaveChanges();

            return _mapper.Map<MedicalRecordDto>(record);
        }
    }
}
