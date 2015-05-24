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
    public partial class frmGiftPoints : Form
    {
        string CustomerName = "";
        int Customer_ID = 0;
        string Customer_VINs = "";

        public frmGiftPoints(string CustomerName, int Customer_ID, string Customer_VINs)
        {
            InitializeComponent();

            this.CustomerName = CustomerName;
            this.Customer_ID = Customer_ID;
            this.Customer_VINs = Customer_VINs;
        }

        private void frmGiftPoints_Load(object sender, EventArgs e)
        {
            this.lblCustomer.Text = this.CustomerName;
            this.lblCustomer.Tag = this.Customer_ID;

            string[] VINs = this.Customer_VINs.Split(',');

            this.cmbVIN.Items.Clear();

            foreach (string V in VINs)
            {
                this.cmbVIN.Items.Add(V);
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private bool ValidateForm()
        {
            try
            {
                if (this.txtRONumber.Text == "")
                {
                    MessageBox.Show("Favor de entrar el # de RO.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    this.txtRONumber.Focus();

                    return false;
                }

                if (this.cmbVIN.Text == "")
                {
                    MessageBox.Show("Favor de escojer el VIN.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    this.cmbVIN.Focus();

                    return false;
                }

                //
                // Validate Amount
                //
                int Sale = Convert.ToInt32(this.txtPoints.Text);
            }
            catch
            {
                MessageBox.Show("Favor de entrar una cantidad de puntos valida.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                this.txtPoints.Focus();

                return false;
            }

            return true;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ValidateForm())
                {
                    frmAdminPassword pass = new frmAdminPassword();

                    DialogResult r = pass.ShowDialog();

                    if (r == System.Windows.Forms.DialogResult.OK)
                    {
                        Cursor = Cursors.WaitCursor;

                        PointsServiceClient c = new PointsServiceClient();

                        int Points = Convert.ToInt32(this.txtPoints.Text);

                        c.RegisterPoints(CGlobals.AppKey, Convert.ToInt32(this.lblCustomer.Tag), DateTime.Today, this.txtRONumber.Text, this.cmbVIN.Text, Points, true, this.txtReason.Text);

                        MessageBox.Show(Points.ToString() + " puntos fueron acreditados.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Dispose();
                    }
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
    }
}
