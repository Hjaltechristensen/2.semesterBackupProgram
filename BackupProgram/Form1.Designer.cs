namespace BackupProgram
{
    partial class Backup
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			lblUSBCheck = new Label();
			BtnInitializeBackup = new Button();
			BtnChooseBackupFile = new Button();
			txtBoxChosenBackupFile = new TextBox();
			lblLoading = new Label();
			lblLoadingDots = new Label();
			SuspendLayout();
			// 
			// lblUSBCheck
			// 
			lblUSBCheck.AutoSize = true;
			lblUSBCheck.Location = new Point(56, 29);
			lblUSBCheck.Name = "lblUSBCheck";
			lblUSBCheck.Size = new Size(181, 25);
			lblUSBCheck.TabIndex = 0;
			lblUSBCheck.Text = "Insæt usb før backup";
			// 
			// BtnInitializeBackup
			// 
			BtnInitializeBackup.Location = new Point(56, 238);
			BtnInitializeBackup.Name = "BtnInitializeBackup";
			BtnInitializeBackup.Size = new Size(181, 61);
			BtnInitializeBackup.TabIndex = 1;
			BtnInitializeBackup.Text = "Start backup";
			BtnInitializeBackup.UseVisualStyleBackColor = true;
			BtnInitializeBackup.Visible = false;
			BtnInitializeBackup.Click += BtnInitializeBackup_Click;
			// 
			// BtnChooseBackupFile
			// 
			BtnChooseBackupFile.Location = new Point(56, 133);
			BtnChooseBackupFile.Name = "BtnChooseBackupFile";
			BtnChooseBackupFile.Size = new Size(112, 34);
			BtnChooseBackupFile.TabIndex = 3;
			BtnChooseBackupFile.Text = "Gennemse";
			BtnChooseBackupFile.UseVisualStyleBackColor = true;
			BtnChooseBackupFile.Visible = false;
			BtnChooseBackupFile.Click += BtnChooseBackupFile_Click;
			// 
			// txtBoxChosenBackupFile
			// 
			txtBoxChosenBackupFile.Location = new Point(56, 96);
			txtBoxChosenBackupFile.Name = "txtBoxChosenBackupFile";
			txtBoxChosenBackupFile.Size = new Size(803, 31);
			txtBoxChosenBackupFile.TabIndex = 5;
			// 
			// lblLoading
			// 
			lblLoading.AutoSize = true;
			lblLoading.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
			lblLoading.Location = new Point(326, 250);
			lblLoading.Name = "lblLoading";
			lblLoading.Size = new Size(204, 28);
			lblLoading.TabIndex = 6;
			lblLoading.Text = "Kopiere til BLUEDRIVE";
			lblLoading.Visible = false;
			// 
			// lblLoadingDots
			// 
			lblLoadingDots.AutoSize = true;
			lblLoadingDots.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
			lblLoadingDots.Location = new Point(525, 242);
			lblLoadingDots.Name = "lblLoadingDots";
			lblLoadingDots.Size = new Size(51, 38);
			lblLoadingDots.TabIndex = 7;
			lblLoadingDots.Text = ". . .";
			lblLoadingDots.Visible = false;
			// 
			// Backup
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(956, 380);
			Controls.Add(lblLoading);
			Controls.Add(txtBoxChosenBackupFile);
			Controls.Add(BtnChooseBackupFile);
			Controls.Add(BtnInitializeBackup);
			Controls.Add(lblUSBCheck);
			Controls.Add(lblLoadingDots);
			Name = "Backup";
			Text = "Backup";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label lblUSBCheck;
		private Button BtnInitializeBackup;
		private Button BtnChooseBackupFile;
		private TextBox txtBoxChosenBackupFile;
		private Label lblLoading;
		private Label lblLoadingDots;
	}
}
