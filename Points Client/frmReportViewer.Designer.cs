namespace Points_Client
{
    partial class frmReportViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.TransactionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsTransactions = new Points_Client.dsTransactions();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.TransactionsTableAdapter = new Points_Client.dsTransactionsTableAdapters.TransactionsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.TransactionsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsTransactions)).BeginInit();
            this.SuspendLayout();
            // 
            // TransactionsBindingSource
            // 
            this.TransactionsBindingSource.DataMember = "Transactions";
            this.TransactionsBindingSource.DataSource = this.dsTransactions;
            // 
            // dsTransactions
            // 
            this.dsTransactions.DataSetName = "dsTransactions";
            this.dsTransactions.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.TransactionsBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Points_Client.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(530, 400);
            this.reportViewer1.TabIndex = 0;
            // 
            // TransactionsTableAdapter
            // 
            this.TransactionsTableAdapter.ClearBeforeFill = true;
            // 
            // frmReportViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 400);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmReportViewer";
            this.Text = "Reporte";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmReportViewer_FormClosing);
            this.Load += new System.EventHandler(this.frmReportViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TransactionsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsTransactions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource TransactionsBindingSource;
        private dsTransactions dsTransactions;
        private dsTransactionsTableAdapters.TransactionsTableAdapter TransactionsTableAdapter;
    }
}