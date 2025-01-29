namespace BankaOtomasyonu.Forms
{
    partial class Tables
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btnStaff = new System.Windows.Forms.Button();
            this.btnAccount = new System.Windows.Forms.Button();
            this.btnCreditCard = new System.Windows.Forms.Button();
            this.btnDeletedCustomers = new System.Windows.Forms.Button();
            this.btnAdmin = new System.Windows.Forms.Button();
            this.btnBankCard = new System.Windows.Forms.Button();
            this.btnTransactions = new System.Windows.Forms.Button();
            this.btnBranches = new System.Windows.Forms.Button();
            this.btnTitle = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Silver;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Location = new System.Drawing.Point(0, 135);
            this.button1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(178, 59);
            this.button1.TabIndex = 0;
            this.button1.Text = "Müşteriler";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(20, 398);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(887, 215);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(110)))), ((int)(((byte)(122)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(921, 61);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(14, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(222, 31);
            this.label1.TabIndex = 4;
            this.label1.Text = "Tablo Kontrol Paneli";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(57)))), ((int)(((byte)(53)))));
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.Dock = System.Windows.Forms.DockStyle.Right;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(871, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(50, 61);
            this.button2.TabIndex = 3;
            this.button2.Text = "X";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnStaff
            // 
            this.btnStaff.BackColor = System.Drawing.Color.Silver;
            this.btnStaff.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStaff.Location = new System.Drawing.Point(0, 67);
            this.btnStaff.Name = "btnStaff";
            this.btnStaff.Size = new System.Drawing.Size(178, 59);
            this.btnStaff.TabIndex = 3;
            this.btnStaff.Text = "Personeller";
            this.btnStaff.UseVisualStyleBackColor = false;
            this.btnStaff.Click += new System.EventHandler(this.btnStaff_Click);
            // 
            // btnAccount
            // 
            this.btnAccount.BackColor = System.Drawing.Color.Silver;
            this.btnAccount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAccount.Location = new System.Drawing.Point(0, 203);
            this.btnAccount.Name = "btnAccount";
            this.btnAccount.Size = new System.Drawing.Size(178, 59);
            this.btnAccount.TabIndex = 4;
            this.btnAccount.Text = "Hesaplar";
            this.btnAccount.UseVisualStyleBackColor = false;
            this.btnAccount.Click += new System.EventHandler(this.btnAccount_Click);
            // 
            // btnCreditCard
            // 
            this.btnCreditCard.BackColor = System.Drawing.Color.Silver;
            this.btnCreditCard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreditCard.Location = new System.Drawing.Point(0, 268);
            this.btnCreditCard.Name = "btnCreditCard";
            this.btnCreditCard.Size = new System.Drawing.Size(178, 59);
            this.btnCreditCard.TabIndex = 5;
            this.btnCreditCard.Text = "Kredi Kartları";
            this.btnCreditCard.UseVisualStyleBackColor = false;
            this.btnCreditCard.Click += new System.EventHandler(this.btnBankCard_Click);
            // 
            // btnDeletedCustomers
            // 
            this.btnDeletedCustomers.BackColor = System.Drawing.Color.Silver;
            this.btnDeletedCustomers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeletedCustomers.Location = new System.Drawing.Point(368, 135);
            this.btnDeletedCustomers.Name = "btnDeletedCustomers";
            this.btnDeletedCustomers.Size = new System.Drawing.Size(225, 59);
            this.btnDeletedCustomers.TabIndex = 6;
            this.btnDeletedCustomers.Text = "Silinen Müşteriler";
            this.btnDeletedCustomers.UseVisualStyleBackColor = false;
            this.btnDeletedCustomers.Click += new System.EventHandler(this.btnDeletedCustomers_Click);
            // 
            // btnAdmin
            // 
            this.btnAdmin.BackColor = System.Drawing.Color.Silver;
            this.btnAdmin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdmin.Location = new System.Drawing.Point(184, 67);
            this.btnAdmin.Name = "btnAdmin";
            this.btnAdmin.Size = new System.Drawing.Size(178, 59);
            this.btnAdmin.TabIndex = 7;
            this.btnAdmin.Text = "Yöneticiler";
            this.btnAdmin.UseVisualStyleBackColor = false;
            this.btnAdmin.Click += new System.EventHandler(this.btnAdmin_Click);
            // 
            // btnBankCard
            // 
            this.btnBankCard.BackColor = System.Drawing.Color.Silver;
            this.btnBankCard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBankCard.Location = new System.Drawing.Point(186, 135);
            this.btnBankCard.Name = "btnBankCard";
            this.btnBankCard.Size = new System.Drawing.Size(178, 59);
            this.btnBankCard.TabIndex = 8;
            this.btnBankCard.Text = "Banka Kartları";
            this.btnBankCard.UseVisualStyleBackColor = false;
            this.btnBankCard.Click += new System.EventHandler(this.btnBankCard_Click_1);
            // 
            // btnTransactions
            // 
            this.btnTransactions.BackColor = System.Drawing.Color.Silver;
            this.btnTransactions.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTransactions.Location = new System.Drawing.Point(186, 200);
            this.btnTransactions.Name = "btnTransactions";
            this.btnTransactions.Size = new System.Drawing.Size(178, 59);
            this.btnTransactions.TabIndex = 9;
            this.btnTransactions.Text = "İşlemler";
            this.btnTransactions.UseVisualStyleBackColor = false;
            this.btnTransactions.Click += new System.EventHandler(this.btnTransactions_Click);
            // 
            // btnBranches
            // 
            this.btnBranches.BackColor = System.Drawing.Color.Silver;
            this.btnBranches.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBranches.Location = new System.Drawing.Point(186, 268);
            this.btnBranches.Name = "btnBranches";
            this.btnBranches.Size = new System.Drawing.Size(178, 59);
            this.btnBranches.TabIndex = 10;
            this.btnBranches.Text = "Şubeler";
            this.btnBranches.UseVisualStyleBackColor = false;
            this.btnBranches.Click += new System.EventHandler(this.btnBranches_Click);
            // 
            // btnTitle
            // 
            this.btnTitle.BackColor = System.Drawing.Color.Silver;
            this.btnTitle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTitle.Location = new System.Drawing.Point(368, 67);
            this.btnTitle.Name = "btnTitle";
            this.btnTitle.Size = new System.Drawing.Size(225, 59);
            this.btnTitle.TabIndex = 11;
            this.btnTitle.Text = "Personel Ünvanları";
            this.btnTitle.UseVisualStyleBackColor = false;
            this.btnTitle.Click += new System.EventHandler(this.btnTitle_Click);
            // 
            // Tables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(921, 619);
            this.Controls.Add(this.btnTitle);
            this.Controls.Add(this.btnBranches);
            this.Controls.Add(this.btnTransactions);
            this.Controls.Add(this.btnBankCard);
            this.Controls.Add(this.btnAdmin);
            this.Controls.Add(this.btnDeletedCustomers);
            this.Controls.Add(this.btnCreditCard);
            this.Controls.Add(this.btnAccount);
            this.Controls.Add(this.btnStaff);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(430, 140);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "Tables";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Tables";
            this.Load += new System.EventHandler(this.Tables_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnStaff;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAccount;
        private System.Windows.Forms.Button btnCreditCard;
        private System.Windows.Forms.Button btnDeletedCustomers;
        private System.Windows.Forms.Button btnAdmin;
        private System.Windows.Forms.Button btnBankCard;
        private System.Windows.Forms.Button btnTransactions;
        private System.Windows.Forms.Button btnBranches;
        private System.Windows.Forms.Button btnTitle;
    }
}