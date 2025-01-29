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
    public partial class KayitOl : Form
    {
        public KayitOl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnKayıtOl_Click(object sender, EventArgs e)
        {
            try
            {
                var customer = new Customer
                {
                    isim = txtIsim.Text,
                    soyisim = txtSoyisim.Text,
                    dogumtarihi = dtpDogumTarihi.Value, // DateTimePicker kontrolünden tarih alıyoruz
                    cinsiyet = rbtnErkek.Checked ? "E" : "K", // RadioButton ile cinsiyet seçimi
                    telno = txtTelNo.Text,
                    sifre = txtSifre.Text
                };

                var customerService = new CustomerService();
                customerService.RegisterCustomer(customer);

                MessageBox.Show("Kayıt başarılı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Login formuna geri dön
                this.Hide();
                Form1 loginForm = new Form1();
                loginForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 frm = new Form1();
            frm.Show();
        }
    }
}
