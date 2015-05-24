using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Points_Client.PointsServiceReference;

namespace Points_Client
{
    public partial class frmInitDB : Form
    {
        public frmInitDB()
        {
            InitializeComponent();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                if (this.txtPassword.Text == "iqc@prrh10")
                {
                    PointsServiceClient c = new PointsServiceClient();

                    c.EraseTable(CGlobals.AppKey, "iqc@prrh10", "Claims");

                    c.EraseTable(CGlobals.AppKey, "iqc@prrh10", "Transactions");

                    c.EraseTable(CGlobals.AppKey, "iqc@prrh10", "Customers");

                    MessageBox.Show("DB was initialized.");
                }
                else
                {
                    MessageBox.Show("Incorrect password.");
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
