using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management.Model
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }

        // Constructors
        public Patient() { }

        public Patient(int patientId, string firstName, string lastName, DateTime dateOfBirth, string gender, string contactNumber, string address)
        {
            PatientId = patientId;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            ContactNumber = contactNumber;
            Address = address;
        }

        // ToString method
        public override string ToString()
        {
            return $"Patient ID: {PatientId}, Name: {FirstName} {LastName}, DOB: {DateOfBirth.ToShortDateString()}, Gender: {Gender}, Contact: {ContactNumber}, Address: {Address}";
        }
    }
}

