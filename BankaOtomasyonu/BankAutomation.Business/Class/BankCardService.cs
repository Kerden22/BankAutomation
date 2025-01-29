using BankAutomation.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAutomation.DataAccess.Repositories;

namespace BankAutomation.Business.Class
{
    public class BankCardService
    {
        private readonly BankCardRepository _repository;

        public BankCardService()
        {
            _repository = new BankCardRepository();
        }

        public void AddBankCard(BankCard bankCard)
        {
            _repository.AddBankCard(bankCard);
        }
    }
}
