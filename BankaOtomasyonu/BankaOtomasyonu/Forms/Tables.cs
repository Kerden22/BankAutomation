using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BankAutomation.DataAccess.Repositories;

namespace BankaOtomasyonu.Forms
{
    public partial class Tables : Form
    {
        public Tables()
        {
            InitializeComponent();
        }

        private void Tables_Load(object sender, EventArgs e)
        {
            BeautifyDataGridView(dataGridView1);
            EnableDoubleBuffering(dataGridView1);
        }
        private void LoadCustomers()
        {
            try
            {
               var customerRepository = new CustomerRepository();
               var customers = customerRepository.GetAllCustomers();

                dataGridView1.DataSource = customers;
                // Tüm sütunları DataGridView'in genişliğine yay
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
               MessageBox.Show($"Hata: {ex.Message}");
            }
        }
        private void LoadStaffs()
        {
            try
            {
                var staffsRepository = new StaffsRepositories(); // StaffsRepositories sınıfından bir örnek oluşturuyoruz.
                var staffs = staffsRepository.GetAllStaffs(); // Tüm çalışanları alıyoruz.

                dataGridView1.DataSource = staffs; // Çalışan listesini DataGridView'e bağlıyoruz.
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                // Tüm sütunları DataGridView'in genişliğine yay.
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}"); // Bir hata oluşursa kullanıcıya gösteriyoruz.
            }
        }
        private void LoadAccounts()
        {
            try
            {
                var accountsRepository = new AccountsRepositories(); // AccountsRepositories sınıfından bir örnek oluşturuyoruz.
                var accounts = accountsRepository.GetAllAccounts(); // Tüm hesapları alıyoruz.

                dataGridView1.DataSource = accounts; // Hesap listesini DataGridView'e bağlıyoruz.
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                // Tüm sütunları DataGridView'in genişliğine yay.
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}"); // Bir hata oluşursa kullanıcıya gösteriyoruz.
            }
        }
        private void LoadCreditCard()
        {
            try
            {
                var bankCardRepository = new CreditCardRepositories(); // BankCardRepositories sınıfından bir örnek oluşturuyoruz.
                var bankCards = bankCardRepository.GetAllBankCards(); // Tüm banka kartlarını alıyoruz.

                dataGridView1.DataSource = bankCards; // Banka kartlarını DataGridView'e bağlıyoruz.
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                // Tüm sütunları DataGridView'in genişliğine yay.
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}"); // Bir hata oluşursa kullanıcıya gösteriyoruz.
            }
        }
        private void LoadDeletedCustomers()
        {
            try
            {
                var deletedCustomersRepository = new DeletedCustomersRepositories(); // Repositories sınıfını çağırıyoruz.
                var deletedCustomers = deletedCustomersRepository.GetAllDeletedCustomers(); // Silinen müşterileri getiriyoruz.

                dataGridView1.DataSource = deletedCustomers; // Silinen müşteri verilerini DataGridView'e bağlıyoruz.
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Sütunları DataGridView'in genişliğine yay.
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}"); // Hata durumunda mesaj gösteriyoruz.
            }
        }
        private void LoadAdministrators()
        {
            try
            {
                var adminRepository = new AdministratorsRepositories(); // Repository'den örnek oluşturuyoruz.
                var administrators = adminRepository.GetAllAdministrators(); // Tüm adminleri alıyoruz.

                dataGridView1.DataSource = administrators; // DataGridView'e verileri bağlama.
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Tüm sütunları otomatik olarak genişlet.
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}"); // Hata durumunda mesaj gösterir.
            }
        }
        private void LoadBankCards()
        {
            try
            {
                var bankCardRepository = new BankCardRepositories(); // Repository'den bir örnek oluşturuyoruz.
                var bankCards = bankCardRepository.GetAllBankCards(); // Tüm banka kartlarını alıyoruz.

                dataGridView1.DataSource = bankCards; // DataGridView'e verileri bağlama.
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Sütunları otomatik genişletme.
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}"); // Hata durumunda mesaj gösterir.
            }
        }
        private void LoadTransactions()
        {
            try
            {
                var transactionRepository = new TransactionRepositories(); // Repository'den bir örnek oluşturuyoruz.
                var transactions = transactionRepository.GetAllTransactions(); // Tüm işlemleri alıyoruz.

                dataGridView1.DataSource = transactions; // DataGridView'e verileri bağlama.
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Sütunları otomatik genişletme.
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}"); // Hata durumunda mesaj gösterir.
            }
        }
        private void LoadBranches()
        {
            try
            {
                var branchRepository = new BranchRepositories(); // Branch repository'den bir örnek oluşturuyoruz.
                var branches = branchRepository.GetAllBranches(); // Tüm şubeleri alıyoruz.

                dataGridView1.DataSource = branches; // DataGridView'e şube verilerini bağlama.
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Sütunları otomatik genişletme.
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}"); // Hata durumunda kullanıcıya mesaj gösterir.
            }
        }
        private void LoadTitles()
        {
            try
            {
                var titleRepository = new TitleRepositories(); // Title repository'den bir örnek oluşturuyoruz.
                var titles = titleRepository.GetAllTitles(); // Tüm unvanları alıyoruz.

                dataGridView1.DataSource = titles; // DataGridView'e unvan verilerini bağlama.
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Sütunları otomatik genişletme.
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}"); // Hata durumunda kullanıcıya mesaj gösterir.
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            LoadStaffs(); // Çalışanları yükleme metodunu çağırıyoruz.
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            LoadAccounts(); 
        }

        private void btnBankCard_Click(object sender, EventArgs e)
        {
            LoadCreditCard(); // Kredi kartlarını yükleme metodunu çağırıyoruz.
        }

        private void btnDeletedCustomers_Click(object sender, EventArgs e)
        {
            LoadDeletedCustomers();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            LoadAdministrators();
        }

        private void btnBankCard_Click_1(object sender, EventArgs e)
        {
            LoadBankCards();
        }

        private void btnTransactions_Click(object sender, EventArgs e)
        {
            LoadTransactions();
        }

        private void btnBranches_Click(object sender, EventArgs e)
        {
            LoadBranches();
        }
        private void btnTitle_Click(object sender, EventArgs e)
        {
            LoadTitles();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void BeautifyDataGridView(DataGridView dataGridView)
        {
            // BeautifyDataGridView(dataGridView1);
            // EnableDoubleBuffering(dataGridView1);
            // Genel Ayarlar
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView.BackgroundColor = Color.White;
            dataGridView.GridColor = Color.LightGray;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Sütun ve Hücre Stilleri
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.DefaultCellStyle.BackColor = Color.White;
            dataGridView.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.DarkBlue;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            // Satır/Sütun Ayarları
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.AllowUserToResizeColumns = false;
            dataGridView.RowHeadersVisible = false;

            // Boş Satırları Gizle
            dataGridView.AllowUserToAddRows = false;

            // Performans İyileştirmeleri
            EnableDoubleBuffering(dataGridView);
            dataGridView.VirtualMode = true;

            // Sütun Sıralama
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }
        private void EnableDoubleBuffering(DataGridView dgv)
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                null, dgv, new object[] { true });
        }
  
    }
}
