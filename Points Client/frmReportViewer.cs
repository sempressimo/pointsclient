using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Points_Client.PointsServiceReference;
using Microsoft.Reporting.WinForms;

namespace Points_Client
{
    public partial class frmReportViewer : Form
    {
        protected string ReportName = "";
        protected string SqlFilter = "";

        public frmReportViewer(string ReportName, string SqlFilter)
        {
            InitializeComponent();

            this.ReportName = ReportName;
            this.SqlFilter = SqlFilter;
        }

        private void frmReportViewer_Load(object sender, EventArgs e)
        {
            DataTable dt = null;

            PointsServiceClient c = new PointsServiceClient();

            switch(this.ReportName)
            {
                case "Report1.rdlc":
                    {
                        dt = c.GetAllTransactions(CGlobals.AppKey);
                    }
                    break;

                    case "Claims.rdlc":
                    {
                        dt = c.GetAllClaims(CGlobals.AppKey);
                    }
                    break;
            }

            DataTable filteredTable = dt.Select(this.SqlFilter).CopyToDataTable();

            this.reportViewer1.Reset();
            this.reportViewer1.LocalReport.ReportPath = @".\" + this.ReportName;

            ReportDataSource rds = null;

            if (this.SqlFilter != "")
            {
                rds = new ReportDataSource("DataSet1", filteredTable);
            }
            else
            {
                rds = new ReportDataSource("DataSet1", dt);
            }

            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(rds);
            this.reportViewer1.LocalReport.Refresh();

            this.reportViewer1.RefreshReport();
        }

        private void frmReportViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.reportViewer1.LocalReport.ReleaseSandboxAppDomain(); 
        }
    }
}