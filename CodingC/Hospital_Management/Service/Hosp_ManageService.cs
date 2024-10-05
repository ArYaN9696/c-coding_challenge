using Hospital_Management.Exceptions;
using Hospital_Management.Model;
using Hospital_Management.Repository.Interface;
using Hospital_Management.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management.Service
{
    public class Hosp_ManageService
    {
        private readonly IHospitalService hospitalService;

        public Hosp_ManageService(IHospitalService hospitalService)
        {
            this.hospitalService = hospitalService;
        }
        public Appointment GetAppointmentById()
        {
            try
            {
                Console.WriteLine("Enter Appointment ID:");
                int appointmentId = int.Parse(Console.ReadLine());
                Appointment appointment = hospitalService.GetAppointmentById(appointmentId);
                Console.WriteLine(appointment); 
                return appointment;
            }
            catch (PatientNumberNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return null; 
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving appointment details: " + ex.Message);
                return null; 
            }
        }
        public void DisplayAppointmentsForPatient()
        {
            try
            {
                Console.WriteLine("Enter Patient ID:");
                int patientId = int.Parse(Console.ReadLine());
                List<Appointment> appointments = hospitalService.GetAppointmentsForPatient(patientId);
                foreach (var appointment in appointments)
                {
                    Console.WriteLine(appointment);
                }
            }
            catch (PatientNumberNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving appointments: " + ex.Message);
            }
        }

        public void DisplayAppointmentsForDoctor()
        {
            try
            {
                Console.WriteLine("Enter Doctor ID:");
                int doctorId = int.Parse(Console.ReadLine());
                List<Appointment> appointments = hospitalService.GetAppointmentsForDoctor(doctorId);
                foreach (var appointment in appointments)
                {
                    Console.WriteLine(appointment);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving appointments: " + ex.Message);
            }
        }

        public void ScheduleNewAppointment()
        {
            try
            {
                FetchAndDisplayDoctors();
                Console.WriteLine("Enter Patient ID, Doctor ID, Appointment Date (YYYY-MM-DD), Description:");
                int newPatientId = int.Parse(Console.ReadLine());
                if (!CheckPatientExists(newPatientId))
                {
                    Console.WriteLine($"No patient found for ID: {newPatientId}");
                    return;
                }
                int newDoctorId = int.Parse(Console.ReadLine());
                if (!CheckDoctorExists(newDoctorId))
                {
                    Console.WriteLine($"No doctor found for ID: {newDoctorId}");
                    return;
                }
                DateTime appointmentDate = DateTime.Parse(Console.ReadLine());
                string description = Console.ReadLine();

                Appointment newAppointment = new Appointment
                {
                    PatientId = newPatientId,
                    DoctorId = newDoctorId,
                    AppointmentDate = appointmentDate,
                    Description = description
                };

                bool result = hospitalService.ScheduleAppointment(newAppointment);
                Console.WriteLine(result ? "Appointment scheduled successfully." : "Failed to schedule appointment.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while scheduling the appointment: " + ex.Message);
            }
        }

        public void UpdateExistingAppointment()
        {
            Console.WriteLine("Enter Appointment ID to update:");
            int appointmentId = int.Parse(Console.ReadLine());

            Appointment existingAppointment = hospitalService.GetAppointmentById(appointmentId);
            if (existingAppointment == null)
            {
                Console.WriteLine($"No appointment found for ID: {appointmentId}");
                return; 
            }

            Console.WriteLine("Enter new Patient ID:");
            int patientId = int.Parse(Console.ReadLine());

            if (!CheckPatientExists(patientId))
            {
                Console.WriteLine($"No patient found for ID: {patientId}");
                return; 
            }
            FetchAndDisplayDoctors();
            Console.WriteLine("Enter new Doctor ID:");
            int doctorId = int.Parse(Console.ReadLine());

            if (!CheckDoctorExists(doctorId))
            {
                Console.WriteLine($"No doctor found for ID: {doctorId}");
                return; 
            }

            Console.WriteLine("Enter new Appointment Date (YYYY-MM-DD):");
            DateTime appointmentDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Enter new Description:");
            string description = Console.ReadLine();

            Appointment updatedAppointment = new Appointment
            {
                AppointmentId = appointmentId, 
                PatientId = patientId,
                DoctorId = doctorId,
                AppointmentDate = appointmentDate,
                Description = description
            };

            bool result = hospitalService.UpdateAppointment(updatedAppointment);
            if (result)
            {
                Console.WriteLine("Appointment updated successfully.");
            }
            else
            {
                Console.WriteLine("Failed to update appointment.");
            }
        }


        public void CancelExistingAppointment()
        {
            try
            {
                Console.WriteLine("Enter Appointment ID to cancel:");
                int appointmentId = int.Parse(Console.ReadLine());
                bool result = hospitalService.CancelAppointment(appointmentId);
                Console.WriteLine(result ? "Appointment canceled successfully." : "Failed to cancel appointment,appointment id not found!.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while canceling the appointment: " + ex.Message);
            }
        }

        public void FetchAndDisplayDoctors()
        {
            List<Doctor> doctors = new List<Doctor>();
            SqlConnection connection = new SqlConnection(DbConnUtil.GetConnString());
            SqlCommand command = new SqlCommand();
            SqlDataReader reader = null;

           
            connection.Open();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM doctor";
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                doctors.Add(new Doctor
                {
                    DoctorId = (int)reader["doctorid"],
                    FirstName = (string)reader["firstname"],
                    LastName = (string)reader["lastname"],
                    Specialization = (string)reader["specialization"],
                    ContactNumber = (string)reader["contactnumber"]
                });
            }

            reader.Close();

          
                Console.WriteLine("Available Doctors:");
                foreach (var doctor in doctors)
                {
                    Console.WriteLine($"ID: {doctor.DoctorId}, Name: {doctor.FirstName} {doctor.LastName}, Specialization: {doctor.Specialization}, Contact: {doctor.ContactNumber}");
                }
            

            
            reader.Close();
            connection.Close();
        }
        public bool CheckPatientExists(int patientId)
        {
            SqlConnection connection = new SqlConnection(DbConnUtil.GetConnString());
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT COUNT(*) FROM patient WHERE patientid = @patientId";
            command.Parameters.AddWithValue("@patientId", patientId);

            connection.Open();
            int count = (int)command.ExecuteScalar();
            connection.Close();

            return count > 0; 
        }

        public bool CheckDoctorExists(int doctorId)
        {
            SqlConnection connection = new SqlConnection(DbConnUtil.GetConnString());
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT COUNT(*) FROM doctor WHERE doctorid = @doctorId";
            command.Parameters.AddWithValue("@doctorId", doctorId);

            connection.Open();
            int count = (int)command.ExecuteScalar();
            connection.Close();

            return count > 0; 
        }

        public void AddPatient()
        {
            Console.WriteLine("Enter Patient First Name:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Enter Patient Last Name:");
            string lastName = Console.ReadLine();

            Console.WriteLine("Enter Patient Date of Birth (YYYY-MM-DD):");
            DateTime dateOfBirth = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Enter Patient Gender:");
            string gender = Console.ReadLine();

            Console.WriteLine("Enter Patient Contact Number:");
            string contactNumber = Console.ReadLine();

            Console.WriteLine("Enter Patient Address:");
            string address = Console.ReadLine();

            Patient newPatient = new Patient
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                Gender = gender,
                ContactNumber = contactNumber,
                Address = address
            };

            bool result = hospitalService.AddPatient(newPatient);
            if (result)
            {
                Console.WriteLine("Patient added successfully.");
            }
            else
            {
                Console.WriteLine("Failed to add patient.");
            }
        }

        public void AddDoctor()
        {
            Console.WriteLine("Enter Doctor First Name:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Enter Doctor Last Name:");
            string lastName = Console.ReadLine();

            Console.WriteLine("Enter Doctor Specialization:");
            string specialization = Console.ReadLine();

            Console.WriteLine("Enter Doctor Contact Number:");
            string contactNumber = Console.ReadLine();

            Doctor newDoctor = new Doctor
            {
                FirstName = firstName,
                LastName = lastName,
                Specialization = specialization,
                ContactNumber = contactNumber
            };

            bool result = hospitalService.AddDoctor(newDoctor);
            if (result)
            {
                Console.WriteLine("Doctor added successfully.");
            }
            else
            {
                Console.WriteLine("Failed to add doctor.");
            }
        }

    }
}
