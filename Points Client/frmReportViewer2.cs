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
    public partial class frmReportViewer2 : Form
    {
        string ReportName = "";

        public frmReportViewer2(string ReportName)
        {
            InitializeComponent();

            this.ReportName = ReportName;
        }

        private void frmReportViewer2_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Value = DateTime.Today;
            this.dateTimePicker2.Value = DateTime.Today;
        }

        public string ToCSV(DataTable table)
        {
            var result = new System.Text.StringBuilder();
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

        DataView dv = null;
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                PointsServiceClient c = new PointsServiceClient();

                //
                // Load the dataset
                //
                switch (this.ReportName)
                {
                    case "Balance":
                        {
                            DataTable dt = c.GetBalanceReport(CGlobals.AppKey, false);

                            dv = new DataView(dt);

                            //dv.RowFilter = "Transaction_Date >= '" + this.dateTimePicker1.Value.ToShortDateString() + "' AND Transaction_Date <= '" + this.dateTimePicker2.Value.ToShortDateString() + "'";
                        }
                        break;

                    case "Transactions":
                        {
                            DataTable dt = c.GetAllTransactions(CGlobals.AppKey);

                            dv = new DataView(dt);

                            dv.RowFilter = "Transaction_Date >= '" + this.dateTimePicker1.Value.ToShortDateString() + "' AND Transaction_Date <= '" + this.dateTimePicker2.Value.ToShortDateString() + "'";
                        }
                        break;

                    case "Gift":
                        {
                            DataTable dt = c.GetAllTransactions(CGlobals.AppKey);

                            dv = new DataView(dt);

                            dv.RowFilter = "ManagerGift = 1 AND Transaction_Date >= '" + this.dateTimePicker1.Value.ToShortDateString() + "' AND Transaction_Date <= '" + this.dateTimePicker2.Value.ToShortDateString() + "'";
                        }
                        break;

                    case "Claims":
                        {
                            DataTable dt = c.GetAllClaims(CGlobals.AppKey);

                            dv = new DataView(dt);

                            dv.RowFilter = "Date >= '" + this.dateTimePicker1.Value.ToShortDateString() + "' AND Date <= '" + this.dateTimePicker2.Value.ToShortDateString() + "'";
                        }
                        break;
                }

                if (dv != null)
                {
                    this.gvReport.DataSource = dv;

                    this.lblRecordCount.Text = "Record count: " + dv.Count.ToString();
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

        private void cmdExport_Click(object sender, EventArgs e)
        {
            try
            {
                this.saveFileDialog1.DefaultExt = "csv";
                this.saveFileDialog1.Filter = "*.csv|*.txt";

                if (this.saveFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    if (this.dv != null)
                    {
                        System.IO.File.WriteAllText(this.saveFileDialog1.FileName, this.ToCSV(this.dv.ToTable()));
                    }
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }
    }
}
