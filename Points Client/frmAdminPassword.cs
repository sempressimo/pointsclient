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
    public partial class frmAdminPassword : Form
    {
        public frmAdminPassword()
        {
            InitializeComponent();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;

            this.Dispose();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                PointsServiceClient c = new PointsServiceClient();

                if (this.txtPassword.Text == c.GetManagerPassword(CGlobals.AppKey, 1))
                {
                    DialogResult = System.Windows.Forms.DialogResult.OK;

                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("Password incorrecto.", "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    this.txtPassword.Clear();
                    this.txtPassword.Focus();
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
    }
}