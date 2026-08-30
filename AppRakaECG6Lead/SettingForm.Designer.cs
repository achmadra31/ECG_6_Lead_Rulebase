namespace AppRakaECG6Lead
{
    partial class SettingForm
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
            this.lblPort = new System.Windows.Forms.Label();
            this.cmbCom = new System.Windows.Forms.ComboBox();
            this.lblRecordTime = new System.Windows.Forms.Label();
            this.txtRecordTime = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUserAccess = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPort.ForeColor = System.Drawing.SystemColors.Control;
            this.lblPort.Location = new System.Drawing.Point(8, 9);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(173, 20);
            this.lblPort.TabIndex = 1;
            this.lblPort.Text = "SELECT COM PORT";
            // 
            // cmbCom
            // 
            this.cmbCom.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCom.FormattingEnabled = true;
            this.cmbCom.Location = new System.Drawing.Point(12, 32);
            this.cmbCom.Name = "cmbCom";
            this.cmbCom.Size = new System.Drawing.Size(260, 32);
            this.cmbCom.TabIndex = 2;
            // 
            // lblRecordTime
            // 
            this.lblRecordTime.AutoSize = true;
            this.lblRecordTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordTime.ForeColor = System.Drawing.SystemColors.Control;
            this.lblRecordTime.Location = new System.Drawing.Point(12, 84);
            this.lblRecordTime.Name = "lblRecordTime";
            this.lblRecordTime.Size = new System.Drawing.Size(171, 20);
            this.lblRecordTime.TabIndex = 3;
            this.lblRecordTime.Text = "LAMA PEREKAMAN";
            this.lblRecordTime.Visible = false;
            // 
            // txtRecordTime
            // 
            this.txtRecordTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRecordTime.Location = new System.Drawing.Point(12, 107);
            this.txtRecordTime.Name = "txtRecordTime";
            this.txtRecordTime.Size = new System.Drawing.Size(260, 31);
            this.txtRecordTime.TabIndex = 4;
            this.txtRecordTime.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Navy;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSave.Location = new System.Drawing.Point(12, 167);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(260, 35);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnUserAccess
            // 
            this.btnUserAccess.BackColor = System.Drawing.Color.Navy;
            this.btnUserAccess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUserAccess.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUserAccess.ForeColor = System.Drawing.SystemColors.Control;
            this.btnUserAccess.Location = new System.Drawing.Point(12, 234);
            this.btnUserAccess.Name = "btnUserAccess";
            this.btnUserAccess.Size = new System.Drawing.Size(260, 35);
            this.btnUserAccess.TabIndex = 14;
            this.btnUserAccess.Text = "USER ACCESS";
            this.btnUserAccess.UseVisualStyleBackColor = false;
            this.btnUserAccess.Click += new System.EventHandler(this.btnUserAccess_Click);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RoyalBlue;
            this.ClientSize = new System.Drawing.Size(284, 281);
            this.Controls.Add(this.btnUserAccess);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtRecordTime);
            this.Controls.Add(this.lblRecordTime);
            this.Controls.Add(this.cmbCom);
            this.Controls.Add(this.lblPort);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximumSize = new System.Drawing.Size(300, 320);
            this.MinimumSize = new System.Drawing.Size(300, 320);
            this.Name = "SettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pengaturan";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.ComboBox cmbCom;
        private System.Windows.Forms.Label lblRecordTime;
        private System.Windows.Forms.TextBox txtRecordTime;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUserAccess;
    }
}