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
    public partial class frmRegisterCustomer : Form
    {
        public frmRegisterCustomer()
        {
            InitializeComponent();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                PointsServiceClient c = new PointsServiceClient();

                txtPhone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

                if (!c.CustomerExists(CGlobals.AppKey, this.txtPhone.Text))
                {
                    c.RegisterCustomer(CGlobals.AppKey, this.txtFullName.Text, this.txtPhone.Text, this.lblPIN.Text, this.txtVINs.Text, this.txtEmail.Text);

                    CGlobals.TempCustomer = this.txtPhone.Text;

                    MessageBox.Show("Registro completado.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = System.Windows.Forms.DialogResult.OK;

                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("El cliente con ese telefono ya existe.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

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

        private void frmRegisterCustomer_Load(object sender, EventArgs e)
        {
            Random d = new Random();

            int PIN = d.Next(9999);

            this.lblPIN.Text = PIN.ToString("0000");
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            this.Dispose();
        }
    }
}
