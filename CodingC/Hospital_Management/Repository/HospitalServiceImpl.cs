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

namespace Hospital_Management.Repository
{
    public class HospitalServiceImpl:IHospitalService
    {
        
            private readonly string connectionString;

            public HospitalServiceImpl()
            {
                connectionString = DbConnUtil.GetConnString();
             }

            public Appointment GetAppointmentById(int appointmentId)
            {
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand();
                SqlDataReader reader = null;

                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM appointment WHERE appointmentid = @appointmentId";
                command.Parameters.AddWithValue("@appointmentId", appointmentId);
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new Appointment
                    {
                        AppointmentId = (int)reader["appointmentid"],
                        PatientId = (int)reader["patientid"],
                        DoctorId = (int)reader["doctorid"],
                        AppointmentDate = (DateTime)reader["appointmentdate"],
                        Description = (string)reader["description"]
                    };
                }

                reader.Close(); 
                connection.Close(); 
                throw new PatientNumberNotFoundException("Appointment not found for ID: " + appointmentId);
            }

            public List<Appointment> GetAppointmentsForPatient(int patientId)
            {
                List<Appointment> appointments = new List<Appointment>();
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand();
                SqlDataReader reader = null;

                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM appointment WHERE patientid = @patientId";
                command.Parameters.AddWithValue("@patientId", patientId);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    appointments.Add(new Appointment
                    {
                        AppointmentId = (int)reader["appointmentid"],
                        PatientId = (int)reader["patientid"],
                        DoctorId = (int)reader["doctorid"],
                        AppointmentDate = (DateTime)reader["appointmentdate"],
                        Description = (string)reader["description"]
                    });
                }

                reader.Close(); 
                connection.Close();
            if (appointments.Count == 0)
            {
                throw new PatientNumberNotFoundException($"No appointments found for patient ID: {patientId}");
            }
            return appointments;
            }

            public List<Appointment> GetAppointmentsForDoctor(int doctorId)
            {
                List<Appointment> appointments = new List<Appointment>();
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand();
                SqlDataReader reader = null;

                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM appointment WHERE doctorid = @doctorId";
                command.Parameters.AddWithValue("@doctorId", doctorId);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    appointments.Add(new Appointment
                    {
                        AppointmentId = (int)reader["appointmentid"],
                        PatientId = (int)reader["patientid"],
                        DoctorId = (int)reader["doctorid"],
                        AppointmentDate = (DateTime)reader["appointmentdate"],
                        Description = (string)reader["description"]
                    });
                }

                reader.Close(); 
                connection.Close();

                if (appointments.Count == 0)
                {
                throw new Exception($"No appointments found for doctor ID: {doctorId}");
                }
                return appointments;
            }

            public bool ScheduleAppointment(Appointment appointment)
            {
               
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand();

                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO appointment (patientid, doctorid, appointmentdate, description) VALUES (@patientId, @doctorId, @appointmentDate, @description)";
                command.Parameters.AddWithValue("@patientId", appointment.PatientId);
                command.Parameters.AddWithValue("@doctorId", appointment.DoctorId);
                command.Parameters.AddWithValue("@appointmentDate", appointment.AppointmentDate);
                command.Parameters.AddWithValue("@description", appointment.Description);

                int rowsAffected = command.ExecuteNonQuery();

               
                connection.Close(); 
                return rowsAffected > 0;
            }

            public bool UpdateAppointment(Appointment appointment)
            {
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand();

                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE appointment SET patientid = @patientId, doctorid = @doctorId, appointmentdate = @appointmentDate, description = @description WHERE appointmentid = @appointmentId";
                command.Parameters.AddWithValue("@patientId", appointment.PatientId);
                command.Parameters.AddWithValue("@doctorId", appointment.DoctorId);
                command.Parameters.AddWithValue("@appointmentDate", appointment.AppointmentDate);
                command.Parameters.AddWithValue("@description", appointment.Description);
                command.Parameters.AddWithValue("@appointmentId", appointment.AppointmentId);

                int rowsAffected = command.ExecuteNonQuery();

                
                connection.Close(); 
                return rowsAffected > 0;
            }

            public bool CancelAppointment(int appointmentId)
            {
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand();

                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM appointment WHERE appointmentid = @appointmentId";
                command.Parameters.AddWithValue("@appointmentId", appointmentId);

                int rowsAffected = command.ExecuteNonQuery();

               
                connection.Close(); 
                return rowsAffected > 0;
            }

        public bool AddPatient(Patient patient)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();

          
                string query = "INSERT INTO patient (firstname, lastname, dateofbirth, gender, contactnumber, address) VALUES (@firstName, @lastName, @dateOfBirth, @gender, @contactNumber, @address)";
                command.Connection = connection;
                command.CommandText = query;

                command.Parameters.AddWithValue("@firstName", patient.FirstName);
                command.Parameters.AddWithValue("@lastName", patient.LastName);
                command.Parameters.AddWithValue("@dateOfBirth", patient.DateOfBirth);
                command.Parameters.AddWithValue("@gender", patient.Gender);
                command.Parameters.AddWithValue("@contactNumber", patient.ContactNumber);
                command.Parameters.AddWithValue("@address", patient.Address);

                connection.Open();
                return command.ExecuteNonQuery() > 0; 
            
                connection.Close(); 
            
        }

        public bool AddDoctor(Doctor doctor)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();

           
                string query = "INSERT INTO doctor (firstname, lastname, specialization, contactnumber) VALUES (@firstName, @lastName, @specialization, @contactNumber)";
                command.Connection = connection;
                command.CommandText = query;

                command.Parameters.AddWithValue("@firstName", doctor.FirstName);
                command.Parameters.AddWithValue("@lastName", doctor.LastName);
                command.Parameters.AddWithValue("@specialization", doctor.Specialization);
                command.Parameters.AddWithValue("@contactNumber", doctor.ContactNumber);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            
           
                connection.Close(); 
            
        }



    }
}
