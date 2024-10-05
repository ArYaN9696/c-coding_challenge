using Hospital_Management.Repository.Interface;
using Hospital_Management.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management.Main
{
    internal class MainModule
    {
        private readonly Hosp_ManageService hospitalService;

        public MainModule(Hosp_ManageService hospitalService)
        {
            this.hospitalService = hospitalService;
        }
        public void Run()
        {
            while (true)
            {
                Console.WriteLine("Hospital Management System Menu:");
                Console.WriteLine("1. View Appointments for Patient");
                Console.WriteLine("2. View Appointments for Doctor");
                Console.WriteLine("3. Schedule Appointment");
                Console.WriteLine("4. Update Appointment");
                Console.WriteLine("5. Cancel Appointment");
                Console.WriteLine("6. Get Appointment Details");
                Console.WriteLine("7. Add Patient");
                Console.WriteLine("8. Add Doctor");
                Console.WriteLine("9. Exit");
                Console.WriteLine("Enter your choice:");


                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                      
                        hospitalService.DisplayAppointmentsForPatient();
                        break;

                    case "2":
                        hospitalService.DisplayAppointmentsForDoctor();
                        break;

                    case "3":
                        hospitalService.ScheduleNewAppointment();
                        break;

                    case "4":
                        hospitalService.UpdateExistingAppointment();
                        break;

                    case "5":
                        hospitalService.CancelExistingAppointment();
                        break;

                    case "6":
                        hospitalService.GetAppointmentById(); 
                        break;

                    case "7":hospitalService.AddPatient();
                        break;

                    case "8":hospitalService.AddDoctor();
                        break;

                    case "9":
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

    }
}
