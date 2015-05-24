using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Points_Client.PointsServiceReference;

namespace Points_Client
{
    public partial class frmChangePassword : Form
    {
        public frmChangePassword()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ManagerPassword == this.txtOldPassword.Text)
                {
                    if (this.txtNewPassword.Text == this.txtNewPassword2.Text)
                    {
                        PointsServiceClient c = new PointsServiceClient();

                        c.UpdateManagerPassword(CGlobals.AppKey, 1, this.txtNewPassword.Text);

                        this.lblDebug.Visible = false;

                        MessageBox.Show("El password fue cambiado.", "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Los passwords no son iguales.", "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Password incorrecto.", "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private string ManagerPassword = "";
        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            PointsServiceClient c = new PointsServiceClient();

            this.ManagerPassword = c.GetManagerPassword(CGlobals.AppKey, 1);
        }

        private void frmChangePassword_DoubleClick(object sender, EventArgs e)
        {
            this.lblDebug.Text = this.ManagerPassword;
            this.lblDebug.Visible = true;
        }
    }
}
