using System.Collections.Generic;
using BankAutomation.DataAccess.Repositories;
using BankAutomation.DataAccess.Entities;

namespace BankAutomation.Business
{
    public class BankaKartBasvurulariService
    {
        private readonly BankaKartBasvurulariRepository _repository;

        public BankaKartBasvurulariService()
        {
            _repository = new BankaKartBasvurulariRepository();
        }

        public void AddBasvuru(BankaKartBasvurulari basvuru)
        {
            _repository.AddBankaKartBasvurusu(basvuru);
        }

        public List<BankaKartBasvurulari> GetAllBasvurular()
        {
            return _repository.GetAllBasvurular();
        }

        public void DeleteBasvuru(int basvuruId)
        {
            _repository.DeleteBasvuru(basvuruId);
        }
    }
}
