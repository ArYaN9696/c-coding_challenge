using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management.Model
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }
        public string ContactNumber { get; set; }

        // Constructors
        public Doctor() { }

        public Doctor(int doctorId, string firstName, string lastName, string specialization, string contactNumber)
        {
            DoctorId = doctorId;
            FirstName = firstName;
            LastName = lastName;
            Specialization = specialization;
            ContactNumber = contactNumber;
        }

        // ToString method
        public override string ToString()
        {
            return $"Doctor ID: {DoctorId}, Name: {FirstName} {LastName}, Specialization: {Specialization}, Contact: {ContactNumber}";
        }
    }
}
