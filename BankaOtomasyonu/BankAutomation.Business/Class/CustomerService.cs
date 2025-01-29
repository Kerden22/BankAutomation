using BankAutomation.DataAccess.Entities;
using BankAutomation.DataAccess.Repositories;
using System;

namespace BankAutomation.Business
{
    public class CustomerService
    {
        private readonly CustomerRepositoryControl _customerRepositoryControl;

        public CustomerService()
        {
            _customerRepositoryControl = new CustomerRepositoryControl();
        }

        public bool ValidateLogin(string username, string password)
        {
            // İş kuralları eklenebilir, gerekirse
            return _customerRepositoryControl.ValidateCustomer(username, password);
        }
        public void RegisterCustomer(Customer customer)
        {
            // Validasyon kuralları
            if (string.IsNullOrEmpty(customer.isim) || string.IsNullOrEmpty(customer.soyisim) ||
                string.IsNullOrEmpty(customer.telno) || string.IsNullOrEmpty(customer.sifre))
            {
                throw new Exception("Tüm alanlar doldurulmalıdır.");
            }

            if (customer.telno.Length != 10)
            {
                throw new Exception("Telefon numarası 10 haneli olmalıdır.");
            }

            // Veritabanına ekleme işlemi
            var customerRepository = new CustomerRepository();
            customerRepository.AddCustomer(customer);
        }

        public int GetCustomerId(string username)
        {
            var customerRepositoryControl = new CustomerRepositoryControl(); // RepositoryControl üzerinden çağır
            var customer = customerRepositoryControl.GetCustomerByUsername(username); // Kullanıcıyı getir
            if (customer == null)
            {
                throw new Exception("Kullanıcı bulunamadı!");
            }
            return customer.Musterino; // Müşteri numarasını döndür
        }



    }
}
