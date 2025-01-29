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
    public partial class YetkiliMenu : Form
    {
        public YetkiliMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Tables tablesForm = new Tables(); // Tables formunun bir örneğini oluşturuyoruz.
            tablesForm.Show(); // Tables formunu açıyoruz.
        }

        private void AnaMenu_Load(object sender, EventArgs e)
        {

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

        private void btnBasvuru_Click(object sender, EventArgs e)
        {
            try
            {
                Basvurular basvurularForm = new Basvurular();
                basvurularForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
