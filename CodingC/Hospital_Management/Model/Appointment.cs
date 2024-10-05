using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management.Model
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Description { get; set; }

        // Constructors
        public Appointment() { }

        public Appointment(int appointmentId, int patientId, int doctorId, DateTime appointmentDate, string description)
        {
            AppointmentId = appointmentId;
            PatientId = patientId;
            DoctorId = doctorId;
            AppointmentDate = appointmentDate;
            Description = description;
        }

        
        public override string ToString()
        {
            return $"Appointment ID: {AppointmentId}, Patient ID: {PatientId}, Doctor ID: {DoctorId}, Date: {AppointmentDate.ToShortDateString()}, Description: {Description}";
        }
    }
}
