using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Points_Server.PointsService;

namespace Points_Server
{
    public partial class frmCustomersFilePath : Form
    {
        public frmCustomersFilePath()
        {
            InitializeComponent();
        }

        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                this.txtPath.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            try
            {
                PointsServiceClient c = new PointsServiceClient();

                c.UpdateFilesPath(CGlobals.AppKey, 1, this.txtPath.Text);

                this.Dispose();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void frmCustomersFilePath_Load(object sender, EventArgs e)
        {
            try
            {
                PointsServiceClient c = new PointsServiceClient();

                this.txtPath.Text = c.GetFilesPath(CGlobals.AppKey, 1);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }
    }
}
