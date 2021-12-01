using System;
using System.ComponentModel.DataAnnotations;
using hospital.Entities;

namespace hospital.Models
{
    public class UpdateMedicalRecordDto
    {
        public int? Pregnancies { get; set; }
        public int? Glucose { get; set; }
        public int? BloodPresure { get; set; }
        public int? SkinThickness { get; set; }
        public int? Insulin { get; set; }
        public float? Bmi { get; set; }
        public float? DiabetesPedigreeFunction { get; set; }
        public int? Age { get; set; }
    }

}
