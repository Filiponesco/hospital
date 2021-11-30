using System;
namespace hospital.Entities
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public int? Pregnancies { get; set; }
        public int? Glucose { get; set; }
        public int? BloodPresure { get; set; }
        public int? SkinThickness { get; set; }
        public int? Insulin { get; set; }
        public float? Bmi { get; set; }
        public float? DiabetesPedigreeFunction { get; set; }
        public int? Age { get; set; }
        public bool? Outcome { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public bool Filled { get; set; }
        public virtual User Patient { get; set; }
    }
}
