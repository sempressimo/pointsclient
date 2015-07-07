using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Points_Server
{
    public partial class frmEmailSettings : Form
    {
        public frmEmailSettings()
        {
            InitializeComponent();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cmdTest_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                string Outcome = CGlobals.SendEmail(this.txtFrom.Text, "Demo Customer", "0");

                MessageBox.Show(Outcome);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void frmEmailSettings_Load(object sender, EventArgs e)
        {
            this.txtSMTP.Text = Properties.Settings.Default.Smtp;
            this.txtFrom.Text = Properties.Settings.Default.From;
            this.txtPort.Text = Properties.Settings.Default.Port;
            this.txtPassword.Text = Properties.Settings.Default.Password;
            this.txtUsername.Text = Properties.Settings.Default.Username;
            this.txtMailBody.Text = Properties.Settings.Default.EmailBody;
            this.txtSubject.Text = Properties.Settings.Default.EmailSubject;
            this.cbDefaultCredentials.Checked = Properties.Settings.Default.UseDefaultCredentials;
            this.cbEnableSSL.Checked = Properties.Settings.Default.EnableSSL;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.Smtp = this.txtSMTP.Text;
                Properties.Settings.Default.From = this.txtFrom.Text;
                Properties.Settings.Default.Port = this.txtPort.Text;
                Properties.Settings.Default.Password = this.txtPassword.Text;
                Properties.Settings.Default.Username = this.txtUsername.Text;
                Properties.Settings.Default.EmailBody = this.txtMailBody.Text;
                Properties.Settings.Default.EmailSubject = this.txtSubject.Text;
                Properties.Settings.Default.UseDefaultCredentials = this.cbDefaultCredentials.Checked;
                Properties.Settings.Default.EnableSSL = this.cbEnableSSL.Checked;

                Properties.Settings.Default.Save();

                MessageBox.Show("Settings saved.", "Save",  MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void cbDefaultCredentials_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbDefaultCredentials.Checked)
            {
                this.txtUsername.Enabled = this.txtPassword.Enabled = false;
            }
            else
            {
                this.txtUsername.Enabled = this.txtPassword.Enabled = true;
            }
        }

        private void cbDefaultCredentials_CheckedChanged_1(object sender, EventArgs e)
        {
            this.txtUsername.Enabled = this.txtPassword.Enabled = !this.cbDefaultCredentials.Checked;
        }
    }
}
