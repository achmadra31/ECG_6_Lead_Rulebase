namespace AppRakaECG6Lead
{
    partial class RecordForm
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
            this.panelNavbar = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.dtpBorn = new System.Windows.Forms.DateTimePicker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblBPM = new System.Windows.Forms.Label();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.lblGender = new System.Windows.Forms.Label();
            this.lblBorn = new System.Windows.Forms.Label();
            this.txtPasienName = new System.Windows.Forms.TextBox();
            this.lblPasienName = new System.Windows.Forms.Label();
            this.txtPasienId = new System.Windows.Forms.TextBox();
            this.lblPasienId = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.lblNamaUser = new System.Windows.Forms.Label();
            this.lblWaktu = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.formsPlot6 = new ScottPlot.WinForms.FormsPlot();
            this.formsPlot5 = new ScottPlot.WinForms.FormsPlot();
            this.formsPlot4 = new ScottPlot.WinForms.FormsPlot();
            this.formsPlot3 = new ScottPlot.WinForms.FormsPlot();
            this.formsPlot2 = new ScottPlot.WinForms.FormsPlot();
            this.formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            this.btnPause = new System.Windows.Forms.Button();
            this.panelNavbar.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelNavbar
            // 
            this.panelNavbar.BackColor = System.Drawing.Color.RoyalBlue;
            this.panelNavbar.Controls.Add(this.panel3);
            this.panelNavbar.Controls.Add(this.dtpBorn);
            this.panelNavbar.Controls.Add(this.panel2);
            this.panelNavbar.Controls.Add(this.cmbGender);
            this.panelNavbar.Controls.Add(this.lblGender);
            this.panelNavbar.Controls.Add(this.lblBorn);
            this.panelNavbar.Controls.Add(this.txtPasienName);
            this.panelNavbar.Controls.Add(this.lblPasienName);
            this.panelNavbar.Controls.Add(this.txtPasienId);
            this.panelNavbar.Controls.Add(this.lblPasienId);
            this.panelNavbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNavbar.Location = new System.Drawing.Point(0, 0);
            this.panelNavbar.Name = "panelNavbar";
            this.panelNavbar.Size = new System.Drawing.Size(1902, 132);
            this.panelNavbar.TabIndex = 1;
            this.panelNavbar.Paint += new System.Windows.Forms.PaintEventHandler(this.panelNavbar_Paint);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkGray;
            this.panel3.Controls.Add(this.lblStatus);
            this.panel3.Location = new System.Drawing.Point(1576, 77);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(314, 37);
            this.panel3.TabIndex = 12;
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblStatus.Location = new System.Drawing.Point(0, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(314, 37);
            this.lblStatus.TabIndex = 11;
            this.lblStatus.Text = "...";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatus.Click += new System.EventHandler(this.lblStatus_Click);
            // 
            // dtpBorn
            // 
            this.dtpBorn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpBorn.Location = new System.Drawing.Point(803, 81);
            this.dtpBorn.Name = "dtpBorn";
            this.dtpBorn.Size = new System.Drawing.Size(481, 31);
            this.dtpBorn.TabIndex = 11;
            this.dtpBorn.ValueChanged += new System.EventHandler(this.dtpBorn_ValueChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkGray;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.lblBPM);
            this.panel2.Location = new System.Drawing.Point(1576, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(314, 37);
            this.panel2.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(258, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 24);
            this.label2.TabIndex = 12;
            this.label2.Text = "BPM";
            // 
            // lblBPM
            // 
            this.lblBPM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBPM.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBPM.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblBPM.Location = new System.Drawing.Point(0, 0);
            this.lblBPM.Name = "lblBPM";
            this.lblBPM.Size = new System.Drawing.Size(314, 37);
            this.lblBPM.TabIndex = 11;
            this.lblBPM.Text = "0";
            this.lblBPM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbGender
            // 
            this.cmbGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGender.FormattingEnabled = true;
            this.cmbGender.Location = new System.Drawing.Point(803, 24);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(481, 33);
            this.cmbGender.TabIndex = 8;
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGender.ForeColor = System.Drawing.SystemColors.Control;
            this.lblGender.Location = new System.Drawing.Point(554, 27);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(182, 25);
            this.lblGender.TabIndex = 5;
            this.lblGender.Text = "JENIS KELAMIN";
            // 
            // lblBorn
            // 
            this.lblBorn.AutoSize = true;
            this.lblBorn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBorn.ForeColor = System.Drawing.SystemColors.Control;
            this.lblBorn.Location = new System.Drawing.Point(554, 83);
            this.lblBorn.Name = "lblBorn";
            this.lblBorn.Size = new System.Drawing.Size(192, 25);
            this.lblBorn.TabIndex = 4;
            this.lblBorn.Text = "TANGGAL LAHIR";
            // 
            // txtPasienName
            // 
            this.txtPasienName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPasienName.Location = new System.Drawing.Point(211, 83);
            this.txtPasienName.Name = "txtPasienName";
            this.txtPasienName.Size = new System.Drawing.Size(297, 31);
            this.txtPasienName.TabIndex = 3;
            // 
            // lblPasienName
            // 
            this.lblPasienName.AutoSize = true;
            this.lblPasienName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPasienName.ForeColor = System.Drawing.SystemColors.Control;
            this.lblPasienName.Location = new System.Drawing.Point(17, 86);
            this.lblPasienName.Name = "lblPasienName";
            this.lblPasienName.Size = new System.Drawing.Size(166, 25);
            this.lblPasienName.TabIndex = 2;
            this.lblPasienName.Text = "PASIEN NAME";
            // 
            // txtPasienId
            // 
            this.txtPasienId.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPasienId.Location = new System.Drawing.Point(211, 24);
            this.txtPasienId.Name = "txtPasienId";
            this.txtPasienId.Size = new System.Drawing.Size(297, 31);
            this.txtPasienId.TabIndex = 1;
            // 
            // lblPasienId
            // 
            this.lblPasienId.AutoSize = true;
            this.lblPasienId.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPasienId.ForeColor = System.Drawing.SystemColors.Control;
            this.lblPasienId.Location = new System.Drawing.Point(17, 27);
            this.lblPasienId.Name = "lblPasienId";
            this.lblPasienId.Size = new System.Drawing.Size(123, 25);
            this.lblPasienId.TabIndex = 0;
            this.lblPasienId.Text = "PASIEN ID";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.RoyalBlue;
            this.panel1.Controls.Add(this.btnPause);
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.btnUpdate);
            this.panel1.Controls.Add(this.lblNamaUser);
            this.panel1.Controls.Add(this.lblWaktu);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 932);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1902, 59);
            this.panel1.TabIndex = 2;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Navy;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.SystemColors.Control;
            this.btnBack.Location = new System.Drawing.Point(1156, 16);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(157, 35);
            this.btnBack.TabIndex = 15;
            this.btnBack.Text = "BACK";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.Navy;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.ForeColor = System.Drawing.SystemColors.Control;
            this.btnExport.Location = new System.Drawing.Point(1701, 16);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(198, 35);
            this.btnExport.TabIndex = 14;
            this.btnExport.Text = "CETAK PDF";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnCetak_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.Navy;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.SystemColors.Control;
            this.btnUpdate.Location = new System.Drawing.Point(1517, 16);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(157, 35);
            this.btnUpdate.TabIndex = 12;
            this.btnUpdate.Text = "UPDATE";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // lblNamaUser
            // 
            this.lblNamaUser.AutoSize = true;
            this.lblNamaUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNamaUser.ForeColor = System.Drawing.SystemColors.Control;
            this.lblNamaUser.Location = new System.Drawing.Point(365, 16);
            this.lblNamaUser.Name = "lblNamaUser";
            this.lblNamaUser.Size = new System.Drawing.Size(193, 25);
            this.lblNamaUser.TabIndex = 13;
            this.lblNamaUser.Text = "Nama User Login";
            // 
            // lblWaktu
            // 
            this.lblWaktu.AutoSize = true;
            this.lblWaktu.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWaktu.ForeColor = System.Drawing.SystemColors.Control;
            this.lblWaktu.Location = new System.Drawing.Point(12, 16);
            this.lblWaktu.Name = "lblWaktu";
            this.lblWaktu.Size = new System.Drawing.Size(229, 25);
            this.lblWaktu.TabIndex = 12;
            this.lblWaktu.Text = "30/06/2026 00:00:00";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.RoyalBlue;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.formsPlot6, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.formsPlot5, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.formsPlot4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.formsPlot3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.formsPlot2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.formsPlot1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 132);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1902, 800);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // formsPlot6
            // 
            this.formsPlot6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlot6.Location = new System.Drawing.Point(1271, 403);
            this.formsPlot6.Name = "formsPlot6";
            this.formsPlot6.Size = new System.Drawing.Size(628, 394);
            this.formsPlot6.TabIndex = 5;
            // 
            // formsPlot5
            // 
            this.formsPlot5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlot5.Location = new System.Drawing.Point(637, 403);
            this.formsPlot5.Name = "formsPlot5";
            this.formsPlot5.Size = new System.Drawing.Size(628, 394);
            this.formsPlot5.TabIndex = 4;
            // 
            // formsPlot4
            // 
            this.formsPlot4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlot4.Location = new System.Drawing.Point(3, 403);
            this.formsPlot4.Name = "formsPlot4";
            this.formsPlot4.Size = new System.Drawing.Size(628, 394);
            this.formsPlot4.TabIndex = 3;
            // 
            // formsPlot3
            // 
            this.formsPlot3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlot3.Location = new System.Drawing.Point(1271, 3);
            this.formsPlot3.Name = "formsPlot3";
            this.formsPlot3.Size = new System.Drawing.Size(628, 394);
            this.formsPlot3.TabIndex = 2;
            // 
            // formsPlot2
            // 
            this.formsPlot2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlot2.Location = new System.Drawing.Point(637, 3);
            this.formsPlot2.Name = "formsPlot2";
            this.formsPlot2.Size = new System.Drawing.Size(628, 394);
            this.formsPlot2.TabIndex = 1;
            // 
            // formsPlot1
            // 
            this.formsPlot1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlot1.Location = new System.Drawing.Point(3, 3);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(628, 394);
            this.formsPlot1.TabIndex = 0;
            // 
            // btnPause
            // 
            this.btnPause.BackColor = System.Drawing.Color.Navy;
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPause.ForeColor = System.Drawing.SystemColors.Control;
            this.btnPause.Location = new System.Drawing.Point(1336, 16);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(157, 35);
            this.btnPause.TabIndex = 16;
            this.btnPause.Text = "PAUSE";
            this.btnPause.UseVisualStyleBackColor = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // RecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.ClientSize = new System.Drawing.Size(1902, 991);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelNavbar);
            this.MaximumSize = new System.Drawing.Size(1920, 1080);
            this.MinimumSize = new System.Drawing.Size(1918, 1030);
            this.Name = "RecordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hasil Rekaman Medis";
            this.Load += new System.EventHandler(this.RecordForm_Load);
            this.panelNavbar.ResumeLayout(false);
            this.panelNavbar.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelNavbar;
        private System.Windows.Forms.DateTimePicker dtpBorn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblBPM;
        private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Label lblBorn;
        private System.Windows.Forms.TextBox txtPasienName;
        private System.Windows.Forms.Label lblPasienName;
        private System.Windows.Forms.TextBox txtPasienId;
        private System.Windows.Forms.Label lblPasienId;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label lblNamaUser;
        private System.Windows.Forms.Label lblWaktu;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ScottPlot.WinForms.FormsPlot formsPlot6;
        private ScottPlot.WinForms.FormsPlot formsPlot5;
        private ScottPlot.WinForms.FormsPlot formsPlot4;
        private ScottPlot.WinForms.FormsPlot formsPlot3;
        private ScottPlot.WinForms.FormsPlot formsPlot2;
        private ScottPlot.WinForms.FormsPlot formsPlot1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPause;
    }
}