using BankAutomation.Business;
using BankAutomation.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankaOtomasyonu.Forms
{
    public partial class KullanıcıMenu : Form
    {
        public KullanıcıMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnHesap_Click(object sender, EventArgs e)
        {
            try
            {
                // Giriş yapan müşterinin ID'sini al
                int customerId = Session.CurrentCustomerId;

                // Kredi kartı ve banka kartı bilgilerini sorgula
                var creditCardRepo = new CreditCardRepositories();
                var bankCardRepo = new BankCardRepositories();
                var accountsRepo = new AccountsRepositories();

                var creditCards = creditCardRepo.GetAllBankCards().Where(x => x.MusteriNo == customerId).ToList();
                var bankCards = bankCardRepo.GetAllBankCards().Where(x => x.MusteriNo == customerId).ToList();
                var accounts = accountsRepo.GetAllAccounts().Where(x => x.MusteriNo == customerId).ToList();

                // Yeni form aç ve bilgileri gönder
                HesapBilgileri hesapForm = new HesapBilgileri(creditCards, bankCards, accounts);
                hesapForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void KullanıcıMenu_Load(object sender, EventArgs e)
        {

        }

        private void btnHavale_Click(object sender, EventArgs e)
        {
            HavaleForm havaleForm = new HavaleForm();
            havaleForm.Show();
        }

        private void btnIslemlerim_Click(object sender, EventArgs e)
        {
            IslemOzet islemForm = new IslemOzet();
            islemForm.Show();
        }
        private void lblCikisYap_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Hesaptan çıkmak istediğinize emin misiniz?", "Çıkış Yap",
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Restart();
            }
        }
        private void lblCikisYap_MouseEnter(object sender, EventArgs e)
        {
            lblCikisYap.ForeColor = Color.Blue;
        }

        private void lblCikisYap_MouseLeave(object sender, EventArgs e)
        {
            lblCikisYap.ForeColor = Color.Red;
        }
    }
}
