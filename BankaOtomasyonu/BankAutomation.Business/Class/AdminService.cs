using BankAutomation.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAutomation.Business.Class
{
   public class AdminService
    {
        private readonly EmployeeRepository _employeeRepository;

        public AdminService()
        {
            _employeeRepository = new EmployeeRepository();
        }

        public bool ValidateLogin(string username, string password)
        {
            // İş kuralları eklenebilir (örneğin, sadece aktif kullanıcıları kontrol etmek)
            return _employeeRepository.ValidateEmployee(username, password);
        }
    }
}
