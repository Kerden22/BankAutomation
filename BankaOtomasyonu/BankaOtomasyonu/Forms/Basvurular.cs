using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BankAutomation.Business;
using BankAutomation.Business.Class;
using BankAutomation.DataAccess.Entities;


namespace BankaOtomasyonu.Forms
{
    public partial class Basvurular : Form
    {
        private readonly BankaKartBasvurulariService _basvuruService;
        public Basvurular()
        {
            InitializeComponent();
            _basvuruService = new BankaKartBasvurulariService();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Basvurular_Load(object sender, EventArgs e)
        {
            // Başvuruları listele
            var basvurular = _basvuruService.GetAllBasvurular();
            dataGridView1.DataSource = basvurular;

            // DataGridView ayarları
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns["BasvuruId"].Visible = true;
            BeautifyDataGridView(dataGridView1);
            EnableDoubleBuffering(dataGridView1);


        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnOnayla_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    var selectedRow = dataGridView1.SelectedRows[0];
                    var basvuruId = Convert.ToInt32(selectedRow.Cells["BasvuruId"].Value);

                    var basvuru = _basvuruService.GetAllBasvurular().FirstOrDefault(x => x.BasvuruId == basvuruId);
                    if (basvuru == null)
                        throw new Exception("Başvuru bulunamadı!");

                    var bankCard = new BankCard
                    {
                        MusteriNo = basvuru.MusteriNo,
                        KartSahibiAdi = basvuru.AdSoyad,
                        Skt = DateTime.Now.AddYears(5),
                        CVV = new Random().Next(100, 999).ToString(), // Rastgele 3 haneli sayı
                        KartTur = "Mastercard",
                        KartNumarası = GenerateCardNumber()
                    };

                    var bankCardService = new BankCardService();
                    bankCardService.AddBankCard(bankCard);

                    _basvuruService.DeleteBasvuru(basvuruId);

                    MessageBox.Show("Başvuru onaylandı ve banka kartı oluşturuldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Basvurular_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Lütfen bir başvuru seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerateCardNumber()
        {
            // Kart numarası oluştur
            string baseNumber = "45438900"; // Sabit başlangıç
            Random random = new Random();
            string randomNumbers = string.Concat(Enumerable.Range(0, 8).Select(_ => random.Next(0, 10).ToString()));
            return $"{baseNumber}{randomNumbers}";

        }

        public void BeautifyDataGridView(DataGridView dataGridView)
        {
           // BeautifyDataGridView(dataGridView1);
         //   EnableDoubleBuffering(dataGridView1);
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

        private void btnReddet_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // Seçili başvuruyu al
                    var selectedRow = dataGridView1.SelectedRows[0];
                    var basvuruId = Convert.ToInt32(selectedRow.Cells["BasvuruId"].Value);

                    // Kullanıcıdan onay al
                    DialogResult result = MessageBox.Show("Bu başvuruyu reddetmek istediğinize emin misiniz?", "Başvuru Reddet",
                                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Başvuruyu sil
                        _basvuruService.DeleteBasvuru(basvuruId);

                        MessageBox.Show("Başvuru başarıyla reddedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Listeyi güncelle
                        Basvurular_Load(sender, e);
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen bir başvuru seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
