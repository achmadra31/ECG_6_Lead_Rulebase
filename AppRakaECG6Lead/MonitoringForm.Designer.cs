namespace AppRakaECG6Lead
{
    partial class MonitoringForm
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
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnDirectory = new System.Windows.Forms.Button();
            this.btnAutoEcg = new System.Windows.Forms.Button();
            this.btnSetting = new System.Windows.Forms.Button();
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
            this.panelNavbar.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkGray;
            this.panel3.Controls.Add(this.lblStatus);
            this.panel3.Location = new System.Drawing.Point(1506, 67);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(384, 49);
            this.panel3.TabIndex = 16;
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblStatus.Location = new System.Drawing.Point(0, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(384, 49);
            this.lblStatus.TabIndex = 11;
            this.lblStatus.Text = "...";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpBorn
            // 
            this.dtpBorn.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpBorn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpBorn.Location = new System.Drawing.Point(806, 83);
            this.dtpBorn.Name = "dtpBorn";
            this.dtpBorn.Size = new System.Drawing.Size(400, 31);
            this.dtpBorn.TabIndex = 11;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkGray;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.lblBPM);
            this.panel2.Location = new System.Drawing.Point(1506, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(384, 49);
            this.panel2.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(249, 0);
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
            this.lblBPM.Size = new System.Drawing.Size(384, 49);
            this.lblBPM.TabIndex = 11;
            this.lblBPM.Text = "0";
            this.lblBPM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBPM.Click += new System.EventHandler(this.lblBPM_Click);
            // 
            // cmbGender
            // 
            this.cmbGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGender.FormattingEnabled = true;
            this.cmbGender.Location = new System.Drawing.Point(806, 16);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(400, 33);
            this.cmbGender.TabIndex = 8;
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGender.ForeColor = System.Drawing.SystemColors.Control;
            this.lblGender.Location = new System.Drawing.Point(586, 19);
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
            this.lblBorn.Location = new System.Drawing.Point(586, 87);
            this.lblBorn.Name = "lblBorn";
            this.lblBorn.Size = new System.Drawing.Size(192, 25);
            this.lblBorn.TabIndex = 4;
            this.lblBorn.Text = "TANGGAL LAHIR";
            // 
            // txtPasienName
            // 
            this.txtPasienName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPasienName.Location = new System.Drawing.Point(198, 85);
            this.txtPasienName.Name = "txtPasienName";
            this.txtPasienName.Size = new System.Drawing.Size(351, 31);
            this.txtPasienName.TabIndex = 3;
            // 
            // lblPasienName
            // 
            this.lblPasienName.AutoSize = true;
            this.lblPasienName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPasienName.ForeColor = System.Drawing.SystemColors.Control;
            this.lblPasienName.Location = new System.Drawing.Point(17, 88);
            this.lblPasienName.Name = "lblPasienName";
            this.lblPasienName.Size = new System.Drawing.Size(166, 25);
            this.lblPasienName.TabIndex = 2;
            this.lblPasienName.Text = "PASIEN NAME";
            // 
            // txtPasienId
            // 
            this.txtPasienId.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPasienId.Location = new System.Drawing.Point(198, 16);
            this.txtPasienId.Name = "txtPasienId";
            this.txtPasienId.Size = new System.Drawing.Size(351, 31);
            this.txtPasienId.TabIndex = 1;
            // 
            // lblPasienId
            // 
            this.lblPasienId.AutoSize = true;
            this.lblPasienId.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPasienId.ForeColor = System.Drawing.SystemColors.Control;
            this.lblPasienId.Location = new System.Drawing.Point(17, 24);
            this.lblPasienId.Name = "lblPasienId";
            this.lblPasienId.Size = new System.Drawing.Size(123, 25);
            this.lblPasienId.TabIndex = 0;
            this.lblPasienId.Text = "PASIEN ID";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.RoyalBlue;
            this.panel1.Controls.Add(this.btnPause);
            this.panel1.Controls.Add(this.btnLogout);
            this.panel1.Controls.Add(this.btnDirectory);
            this.panel1.Controls.Add(this.btnAutoEcg);
            this.panel1.Controls.Add(this.btnSetting);
            this.panel1.Controls.Add(this.lblNamaUser);
            this.panel1.Controls.Add(this.lblWaktu);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.ForeColor = System.Drawing.SystemColors.Control;
            this.panel1.Location = new System.Drawing.Point(0, 932);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1902, 59);
            this.panel1.TabIndex = 1;
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.Navy;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.SystemColors.Control;
            this.btnLogout.Location = new System.Drawing.Point(1054, 12);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(116, 37);
            this.btnLogout.TabIndex = 16;
            this.btnLogout.Text = "LOGOUT";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnDirectory
            // 
            this.btnDirectory.BackColor = System.Drawing.Color.Navy;
            this.btnDirectory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDirectory.Location = new System.Drawing.Point(1187, 12);
            this.btnDirectory.Name = "btnDirectory";
            this.btnDirectory.Size = new System.Drawing.Size(157, 37);
            this.btnDirectory.TabIndex = 15;
            this.btnDirectory.Text = "DIRECTORY";
            this.btnDirectory.UseVisualStyleBackColor = false;
            this.btnDirectory.Click += new System.EventHandler(this.btnDirectory_Click);
            // 
            // btnAutoEcg
            // 
            this.btnAutoEcg.BackColor = System.Drawing.Color.Navy;
            this.btnAutoEcg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAutoEcg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAutoEcg.ForeColor = System.Drawing.SystemColors.Control;
            this.btnAutoEcg.Location = new System.Drawing.Point(1692, 13);
            this.btnAutoEcg.Name = "btnAutoEcg";
            this.btnAutoEcg.Size = new System.Drawing.Size(198, 35);
            this.btnAutoEcg.TabIndex = 14;
            this.btnAutoEcg.Text = "AUTO ECG";
            this.btnAutoEcg.UseVisualStyleBackColor = false;
            this.btnAutoEcg.Click += new System.EventHandler(this.btnAutoEcg_Click);
            // 
            // btnSetting
            // 
            this.btnSetting.BackColor = System.Drawing.Color.Navy;
            this.btnSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetting.Location = new System.Drawing.Point(1362, 13);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(157, 36);
            this.btnSetting.TabIndex = 12;
            this.btnSetting.Text = "SETTING";
            this.btnSetting.UseVisualStyleBackColor = false;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // lblNamaUser
            // 
            this.lblNamaUser.AutoSize = true;
            this.lblNamaUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNamaUser.ForeColor = System.Drawing.SystemColors.Control;
            this.lblNamaUser.Location = new System.Drawing.Point(338, 16);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1902, 800);
            this.tableLayoutPanel1.TabIndex = 2;
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
            this.formsPlot5.Click += new System.EventHandler(this.btnLogout_Click);
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
            this.btnPause.Location = new System.Drawing.Point(1536, 13);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(139, 36);
            this.btnPause.TabIndex = 18;
            this.btnPause.Text = "PAUSE";
            this.btnPause.UseVisualStyleBackColor = false;
            // 
            // MonitoringForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RoyalBlue;
            this.ClientSize = new System.Drawing.Size(1902, 991);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelNavbar);
            this.MaximumSize = new System.Drawing.Size(1920, 1080);
            this.MinimumSize = new System.Drawing.Size(1918, 1030);
            this.Name = "MonitoringForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Monitoring ECG";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MonitoringForm_Load);
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
        private System.Windows.Forms.Label lblPasienId;
        private System.Windows.Forms.TextBox txtPasienId;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.TextBox txtPasienName;
        private System.Windows.Forms.Label lblPasienName;
        private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblBPM;
        private System.Windows.Forms.DateTimePicker dtpBorn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblBorn;
        private System.Windows.Forms.Label lblWaktu;
        private System.Windows.Forms.Label lblNamaUser;
        private System.Windows.Forms.Button btnDirectory;
        private System.Windows.Forms.Button btnAutoEcg;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ScottPlot.WinForms.FormsPlot formsPlot6;
        private ScottPlot.WinForms.FormsPlot formsPlot5;
        private ScottPlot.WinForms.FormsPlot formsPlot4;
        private ScottPlot.WinForms.FormsPlot formsPlot3;
        private ScottPlot.WinForms.FormsPlot formsPlot2;
        private ScottPlot.WinForms.FormsPlot formsPlot1;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnPause;
    }
}