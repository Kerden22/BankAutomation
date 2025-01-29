using BankAutomation.Business;
using BankAutomation.DataAccess.Entities;
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
    public partial class BankaKartBasvuru : Form
    {
        public BankaKartBasvuru()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBasvuruGonder_Click(object sender, EventArgs e)
        {
            try
            {
                var basvuru = new BankaKartBasvurulari
                {
                    MusteriNo = Session.CurrentCustomerId, 
                    AdSoyad = txtAdSoyad.Text,
                    DogumTarihi = dtpDogumTarihi.Value,
                    Cinsiyet = rbtnErkek.Checked ? "E" : "K", 
                    TelefonNo = txtTelefonNo.Text,
                    Adres = txtAdres.Text
                };

                // Başvuru servisini kullanarak tabloya ekle
                var basvuruService = new BankaKartBasvurulariService();
                basvuruService.AddBasvuru(basvuru);

                MessageBox.Show("Banka Kartı Başvurunuz alınmıştır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
