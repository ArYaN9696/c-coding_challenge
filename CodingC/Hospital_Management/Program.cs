using Hospital_Management.Main;
using Hospital_Management.Repository;
using Hospital_Management.Repository.Interface;
using Hospital_Management.Service;

namespace Hospital_Management
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IHospitalService orderProcessor = new HospitalServiceImpl();
            Hosp_ManageService orderService = new Hosp_ManageService(orderProcessor);


            MainModule menu = new MainModule(orderService);
            menu.Run();
        }
    }
}
