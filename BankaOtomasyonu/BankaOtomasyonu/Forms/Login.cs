using BankAutomation.DataAccess.Repositories;
using BankAutomation.Business.Class;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using BankaOtomasyonu.Forms;
using BankAutomation.Business;

namespace BankaOtomasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

     
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void txtKullaniciAdi_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = txtKullaniciAdi.Text;
            string sifre = txtSifre.Text;

            try
            {
                if (chkGirisTuru.Checked && chkKullanıcı.Checked) // Her iki checkbox seçildiyse
                {
                    MessageBox.Show("Lütfen sadece bir kullanıcı türü seçin!",
                                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // İşlemi sonlandır
                }

                if (!chkGirisTuru.Checked && !chkKullanıcı.Checked) // Hiçbir checkbox seçilmediyse
                {
                    MessageBox.Show("Lütfen bir kullanıcı türü seçin!",
                                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // İşlemi sonlandır
                }

                if (chkGirisTuru.Checked) // Yönetici Girişi kontrolü
                {
                    var employeeService = new AdminService(); // Çalışan servisi (Admin girişleri için)
                    bool isValidAdmin = employeeService.ValidateLogin(kullaniciAdi, sifre); // Yönetici doğrulama

                    if (isValidAdmin)
                    {
                        this.Hide(); // Login formunu gizle
                        YetkiliMenu anaMenu = new YetkiliMenu(); // Yönetici için Menu formu
                        anaMenu.Show(); // Menu formunu aç
                    }
                    else
                    {
                        MessageBox.Show("Yönetici girişi başarısız! Kullanıcı adı veya şifre hatalı.",
                                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                if (chkKullanıcı.Checked) // Kullanıcı Girişi kontrolü
                {
                    var customerService = new CustomerService();
                    bool isValidUser = customerService.ValidateLogin(kullaniciAdi, sifre); // Kullanıcı doğrulama

                    if (isValidUser)
                    {
                        // Giriş yapan müşterinin ID'sini al ve global olarak sakla
                        Session.CurrentCustomerId = customerService.GetCustomerId(kullaniciAdi);

                        this.Hide(); // Login formunu gizle
                        KullanıcıMenu kullaniciForm = new KullanıcıMenu(); // Kullanıcı menüsü
                        kullaniciForm.Show(); // Kullanıcı formunu aç
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı girişi başarısız! Kullanıcı adı veya şifre hatalı.",
                                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkKayıtOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            KayitOl KayitOl = new KayitOl(); 
            KayitOl.Show();
        }
    }
}
