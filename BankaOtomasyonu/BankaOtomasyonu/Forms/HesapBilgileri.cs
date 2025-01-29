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
    public partial class HesapBilgileri : Form
    {
        public HesapBilgileri()
        {
            InitializeComponent();
        }
        public HesapBilgileri(List<CreditCard> creditCards, List<BankCard> bankCards, List<Accounts> accounts) 
        {
            InitializeComponent();

            // Kredi kartı bilgilerini doldur
            dataGridView1.DataSource = creditCards;

            // Banka kartı bilgilerini doldur
            dataGridView2.DataSource = bankCards;

            dataGridView3.DataSource = accounts;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void HesapBilgileri_Load(object sender, EventArgs e)
        {
            BeautifyDataGridView(dataGridView1);
            BeautifyDataGridView(dataGridView2);
            BeautifyDataGridView(dataGridView3);
            EnableDoubleBuffering(dataGridView1);
            EnableDoubleBuffering(dataGridView2);
            EnableDoubleBuffering(dataGridView3);
        }

        private void btnBasvuru_Click(object sender, EventArgs e)
        {
            this.Hide();
            BankaKartBasvuru basvuruForm = new BankaKartBasvuru();
            basvuruForm.Show();
            
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
