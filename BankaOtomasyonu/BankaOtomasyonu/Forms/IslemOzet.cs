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
    public partial class IslemOzet : Form
    {
        public IslemOzet()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void IslemOzet_Load(object sender, EventArgs e)
        {
            try
            {
                // Giriş yapan müşterinin ID'sini al
                int customerId = Session.CurrentCustomerId;

                // İşlem servisi ile işlemleri al
                var transactionService = new TransactionService();
                var transactions = transactionService.GetCustomerTransactions(customerId);

                // İşlemleri DataGridView'e bağla
                dataGridView1.DataSource = transactions;

                // DataGridView ayarları
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Columns["MusteriNo"].HeaderText = "Müşteri No";
                dataGridView1.Columns["islemtarihi"].HeaderText = "İşlem Tarihi";
                dataGridView1.Columns["islemtur"].HeaderText = "İşlem Türü";
                dataGridView1.Columns["Tutar"].HeaderText = "Tutar";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            BeautifyDataGridView(dataGridView1);
            EnableDoubleBuffering(dataGridView1);
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
