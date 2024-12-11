using System.Diagnostics;
using System.Text;

namespace BackupProgram
{
	public partial class Backup : Form
	{
		//BLUEDRIVE == false
		//REDDRIVE == true
		string? driveName; //USB lokation (den lokation der skal kopieres til)
		string? driveVolumeLable;
		bool doneLoading = false;
		public Backup()
		{
			InitializeComponent();
			CheckUSBDrives();
		}

		/// <summary>
		/// Metode der gemmer bool v�rdien
		/// </summary>
		/// <param name="value"></param>
		private void SaveBoolValue(bool value)
		{
			Properties.Settings.Default.BoolSetting = value; // Gem v�rdien
			Properties.Settings.Default.Save(); // S�rg for at gemme �ndringen
		}

		/// <summary>
		/// Metode der gemmer den sidst valgte gemme lokation
		/// </summary>
		/// <param name="locationPath"></param>
		private void SaveLastLocation(string locationPath)
		{
			Properties.Settings.Default.LastLocation = locationPath;
			Properties.Settings.Default.Save();
		}

		/// <summary>
		/// Metode der henter bool v�rdien
		/// </summary>
		/// <returns></returns>
		private bool LoadBoolValue()
		{
			return Properties.Settings.Default.BoolSetting;
		}

		/// <summary>
		/// Metode der henter den sidst valgte lokation
		/// </summary>
		/// <returns></returns>
		private string LoadLastLocation()
		{
			return Properties.Settings.Default.LastLocation;
		}

		/// <summary>
		/// Metode der k�rer hvis usb inds�ttes eller fjernes
		/// </summary>
		/// <param name="message"></param>
		protected override void WndProc(ref Message message)
		{
			const int usbInsertedOrRemoved = 0x0219; //Kode der bliver sendt n�r hardware�ndringer opst�r s�som: Tilf�jelse eller fjernelse af enheder (USB-stik, harddiske osv.)

			base.WndProc(ref message);

			if (message.Msg == usbInsertedOrRemoved)
			{
				CheckUSBDrives();
			}
		}

		/// <summary>
		/// Metode der tjekker om en eller flere drev p� computeren er en 'removable' og 'is ready' og opdatere label
		/// </summary>
		private void CheckUSBDrives()
		{
			DriveInfo[] allDrives = DriveInfo.GetDrives(); //Get dive info on all driven on the computer

			foreach (DriveInfo drive in allDrives)
			{
				if (drive.DriveType == DriveType.Removable && drive.IsReady)
				{
					CheckForRightUsb();
				}
				else
				{
					lblUSBCheck.Text = "Ins�t USB f�r backup";
					BtnInitializeBackup.Visible = false;
					txtBoxChosenBackupFile.Visible = false;
					BtnChooseBackupFile.Visible = false;
				}
			}
		}

		/// <summary>
		/// Metode der tjekker volumeName p� usb
		/// </summary>
		private void CheckForRightUsb()
		{
			var drives = System.IO.DriveInfo.GetDrives();
			foreach (var drive in drives)
			{
				if (drive.VolumeLabel == "BLUEDRIVE" && LoadBoolValue() == false)
				{
					lblUSBCheck.Text = $"USB fundet ({drive.VolumeLabel}), du kan nu lave backup";
					driveVolumeLable = drive.VolumeLabel;
					driveName = drive.Name;
					BtnInitializeBackup.Visible = true;
					txtBoxChosenBackupFile.Visible = true;
					txtBoxChosenBackupFile.Text = LoadLastLocation(); // Henter sidste lokation
					BtnChooseBackupFile.Visible = true;
				}
				else if (drive.VolumeLabel == "REDDRIVE" && LoadBoolValue() == true)
				{
					lblUSBCheck.Text = $"USB fundet ({drive.VolumeLabel}), du kan nu lave backup";
					driveVolumeLable = drive.VolumeLabel;
					driveName = drive.Name;
					BtnInitializeBackup.Visible = true;
					txtBoxChosenBackupFile.Visible = true;
					txtBoxChosenBackupFile.Text = LoadLastLocation(); // Henter sidste lokation
					BtnChooseBackupFile.Visible = true;
				}
				else
				{
					lblUSBCheck.Text = $"Forkert usb, du har indsat ({drive.VolumeLabel})";
					BtnInitializeBackup.Visible = false;
					txtBoxChosenBackupFile.Visible = false;
					BtnChooseBackupFile.Visible = false;
				}
			}
		}

		private void BtnChooseBackupFile_Click(object sender, EventArgs e)
		{
			ChooseBackupLocation();
		}

		/// <summary>
		/// �bner gennemse-vindue s� der kan v�lges backup lokation
		/// </summary>
		private void ChooseBackupLocation()
		{
			using (FolderBrowserDialog folder = new FolderBrowserDialog())
			{
				folder.Description = "V�lg en backup mappe";

				if (folder.ShowDialog() == DialogResult.OK)
				{
					txtBoxChosenBackupFile.Text = folder.SelectedPath;
					SaveLastLocation(folder.SelectedPath); // Gemmer lokationen lokalt
				}
			}
		}

		private async void BtnInitializeBackup_Click(object sender, EventArgs e)
		{
			doneLoading = false;
			if (txtBoxChosenBackupFile.Text != string.Empty && !string.IsNullOrEmpty(driveName))
			{
				string batFilePath = Path.Combine(Application.StartupPath, "batFile", "backup.bat");
				//Application.StartupPath henter den bagvedliggende sti udfra batFile(mappe) og backup.bat(batfilen) filen ligger efter rebuild i bin\debug osv

				bool backupSuccess = await RunBatFile(batFilePath, txtBoxChosenBackupFile.Text, driveName); // K�rer metoden der s�tter gang i bat filen

				if (backupSuccess)
				{
					string caption = "Backup f�rdig";
					string message = "Vil du lukke backup programmet?";
					DialogResult yesNo = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
					if (yesNo == System.Windows.Forms.DialogResult.Yes)
					{
						Application.Exit();
					}
				}
				else
				{
					string message = "Backup lykkedes ikke!";
					MessageBox.Show(message, "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			else
			{
				MessageBox.Show("V�lg den sti der skal kopieres og s�rg for, at USB-drevet er korrekt tilsluttet", "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private async Task<bool> RunBatFile(string batFilePath, string sourceFolder, string destinationFolder)
		{
			bool success = false;
			try
			{
				Process process = new Process();

				process.StartInfo.FileName = batFilePath; // Reference til den bat fil der skal k�res
				process.StartInfo.Arguments = $"\"{sourceFolder}\" \"{destinationFolder}\\backup\""; // Opretter en ny mappe med navn 'backup' og gemmer der i
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.CreateNoWindow = true; // Sikre at der ikke �bnes consol vindue
				process.StartInfo.RedirectStandardOutput = true; // Giver mulighed for at se l�bende output beskeder i programmet
				process.StartInfo.RedirectStandardError = true; // Giver mulighed for at se fejl beskeder i programmet

				process.Start();
				var loadingTask = ShowLoading();

				string output = await process.StandardOutput.ReadToEndAsync(); // ReadToEndAsync er med returv�rdi Task<string> og returnerer derfor en string
				string error = await process.StandardError.ReadToEndAsync(); 
				await process.WaitForExitAsync();

				doneLoading = true; // Stop loading-animation
				await loadingTask;  // Vent p�, at ShowLoading afsluttes
				bool processSuccess = false;

				if (!string.IsNullOrWhiteSpace(output))
				{
					MessageBox.Show($"Output:\n{output}", "Kpoiering f�rdig", MessageBoxButtons.OK, MessageBoxIcon.Information);
					processSuccess = true;
				}

				if (!string.IsNullOrWhiteSpace(error))
				{
					processSuccess = false;
				}

				if (processSuccess)
				{
					success = true;
					if (LoadBoolValue()) // Hvis kopieringen er successful tjekkes der f�rst om den lokale bool er true eller false og derefter gemmer den modsatte v�rdi
					{
						SaveBoolValue(false);
					}
					else
					{
						SaveBoolValue(true);
					}
					CheckForRightUsb();
				}	
				else
				{
					MessageBox.Show($"Error:\n{error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Fejl ved k�rsel af batchfil: {ex.Message}", "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return success;
		}

		private async Task ShowLoading()
		{
			lblLoading.Visible = true;
			lblLoading.Text = $"Kopiere til {driveVolumeLable}";
			if (driveVolumeLable == "REDDRIVE") // Skifter positioinen en lille smule alt efter om det er det ene drev eller andet. Teksten er lidt l�ngere i bluedrive
			{
				lblLoadingDots.Location = new Point(518, 242);
			}
			else if (driveVolumeLable == "BLUEDRIVE")
			{
				lblLoadingDots.Location = new Point(525, 242);
			}
			lblLoadingDots.Visible = true;
			while (!doneLoading) // Giver en loading illution. 
			{
				lblLoadingDots.Text = "";
				await Task.Delay(500); // Venter 0,5 sekunder f�r den g�r videre.
				lblLoadingDots.Text = ".";
				await Task.Delay(500);
				lblLoadingDots.Text = ". .";
				await Task.Delay(500);
				lblLoadingDots.Text = ". . .";
				await Task.Delay(500);
			}
			lblLoading.Text = "";
			lblLoadingDots.Text = "";
			lblLoading.Visible = false;
			lblLoadingDots.Visible = false; // Fjerner alt n�r koiperingen er f�rdig og animationen er f�rdig
		}


	}
}