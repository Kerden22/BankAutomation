using BankAutomation.Business;
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
    public partial class HavaleForm : Form
    {
        public HavaleForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void btnGonder_Click(object sender, EventArgs e)
        {
            try
            {
                // Giriş değerlerini al
                int gonderenHesapNo = int.Parse(txtGonderenHesapNo.Text);
                int alanHesapNo = int.Parse(txtAlanHesapNo.Text);
                decimal tutar = decimal.Parse(txtTutar.Text);

                // İş mantığını çağır
                var transactionService = new TransactionService();
                transactionService.ExecuteHavale(gonderenHesapNo, alanHesapNo, tutar);

                MessageBox.Show("Havale işlemi başarıyla tamamlandı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
