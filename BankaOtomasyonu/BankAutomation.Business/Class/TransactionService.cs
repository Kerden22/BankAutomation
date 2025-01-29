using BankAutomation.DataAccess.Repositories;
using System.Collections.Generic;
using BankAutomation.DataAccess.Entities;
using System;

namespace BankAutomation.Business
{
    public class TransactionService
    {
        private readonly TransactionRepositories _transactionRepository;
        private readonly AccountsRepositories _accountsRepository;

        public TransactionService()
        {
            _transactionRepository = new TransactionRepositories();
            _accountsRepository = new AccountsRepositories();
        }

        public void ExecuteHavale(int gonderenHesapNo, int alanHesapNo, decimal tutar)
        {
            // İş mantığı: Giriş yapan müşterinin hesap kontrolü
            if (!_accountsRepository.IsAccountBelongsToCustomer(gonderenHesapNo, Session.CurrentCustomerId))
            {
                throw new Exception("Bu hesap size ait değildir. İşlem gerçekleştirilemez.");
            }

            if (tutar <= 0)
            {
                throw new Exception("Gönderilecek tutar sıfırdan büyük olmalıdır.");
            }

            // Havale işlemini repository'e yönlendir
            _transactionRepository.ExecuteHavale(gonderenHesapNo, alanHesapNo, tutar);
        }

        public List<Transactions> GetCustomerTransactions(int customerId)
        {
            // İş mantığı: İşlemleri sadece giriş yapan müşteriye göster
            return _transactionRepository.GetTransactionsByCustomerId(customerId);
        }
    }
}
