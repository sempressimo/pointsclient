using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Points_Client.PointsServiceReference;
using System.IO;

namespace Points_Client
{
    public partial class frmBackup : Form
    {
        public frmBackup()
        {
            InitializeComponent();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public string ToCSV(DataTable table)
        {
            var result = new StringBuilder();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                result.Append(table.Columns[i].ColumnName);
                result.Append(i == table.Columns.Count - 1 ? "\n" : ",");
            }

            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    result.Append(row[i].ToString());
                    result.Append(i == table.Columns.Count - 1 ? "\n" : ",");
                }
            }

            return result.ToString();
        }

        private void SaveToDisk(string Filename, string Data)
        {
            File.WriteAllText(Filename, Data);
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                PointsServiceClient c = new PointsServiceClient();

                DataTable dtCustomers = c.GetCustomers(CGlobals.AppKey, "");
                DataTable dtTransactions = c.GetAllTransactions(CGlobals.AppKey);
                DataTable dtClaims = c.GetAllClaims(CGlobals.AppKey);

                string CustomersCSV = this.ToCSV(dtCustomers);
                this.SaveToDisk(this.txtSaveTo.Text + @"\customers.csv", CustomersCSV);

                string TransactionsCSV = this.ToCSV(dtTransactions);
                this.SaveToDisk(this.txtSaveTo.Text + @"\transactions.csv", TransactionsCSV);

                string ClaimsCSV = this.ToCSV(dtClaims);
                this.SaveToDisk(this.txtSaveTo.Text + @"\claims.csv", ClaimsCSV);

                //
                // Save the folder
                //
                Properties.Settings.Default.BackupFolder = this.txtSaveTo.Text;
                Properties.Settings.Default.Save();

                MessageBox.Show("El backup fue creado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void cmdSavePath_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.folderBrowserDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    this.txtSaveTo.Text = this.folderBrowserDialog1.SelectedPath;
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void frmBackup_Load(object sender, EventArgs e)
        {
            this.txtSaveTo.Text = Properties.Settings.Default.BackupFolder;
        }
    }
}
