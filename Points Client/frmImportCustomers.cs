using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Points_Client.PointsServiceReference;

namespace Points_Client
{
    public partial class frmImportCustomers : Form
    {
        PointsServiceClient pService = new PointsServiceClient();

        public frmImportCustomers()
        {
            InitializeComponent();
        }

        protected void LoadCustomers()
        {
            this.lblImportText.Text = "Importando clientes. Favor de esperar...";

            string[] FileContentArray;

            string Filename = pService.GetFilesPath(CGlobals.AppKey, 1) + @"\Customers.csv";

            if (!File.Exists(Filename))
            {
                this.Dispose();

                CGlobals.CustomersFileNotFound = true;
            }
            else
            {
                using (StreamReader sr = new StreamReader(Filename))
                {
                    FileContentArray = sr.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                }

                if (FileContentArray.Count() > 0)
                {
                    PointsServiceClient c = new PointsServiceClient();

                    int LineNumber = 1;
                    foreach (string line in FileContentArray)
                    {
                        if (this.SkipImport) break;

                        if (LineNumber > 1)
                        {
                            string[] values = line.Split(',');

                            string FullName = values[0].Replace("\"", "") + " " + values[1].Replace("\"", "");
                            string PhoneNumber = "";

                            if (values[10].Trim() != "")
                            {
                                PhoneNumber = values[10].Replace("\"", "").Replace("-", "");
                            }
                            else
                            {
                                PhoneNumber = values[9].Replace("\"", "").Replace("-", "");
                            }

                            string Password = "autocentro1";
                            string VINS = values[11].Replace("\"", "");
                            string Email = values[8].Replace("\"", "");

                            if (!c.CustomerExists(CGlobals.AppKey, PhoneNumber))
                            {
                                c.RegisterCustomer(CGlobals.AppKey, FullName, PhoneNumber, Password, VINS, Email);

                                CGlobals.Count_New_Customers++;

                                this.lblNew.Text = "Nuevos: " + CGlobals.Count_New_Customers.ToString().PadLeft(4, '0');
                            }
                            else
                            {
                                CGlobals.Count_Updated_Customers++;

                                this.lblExisting.Text = "Existentes: " + CGlobals.Count_Updated_Customers.ToString().PadLeft(4, '0'); ;
                            }
                        }

                        LineNumber++;

                        Application.DoEvents();
                    }

                    File.Delete(Filename);
                }
            }
        }

        private void LoadROs()
        {
            this.lblImportText.Text = "Importando RO(s). Favor de esperar...";

            Application.DoEvents();

            string[] FileContentArray;

            string Filename = pService.GetFilesPath(CGlobals.AppKey, 1) + @"\RO.csv";

            if (!File.Exists(Filename))
            {
                this.Dispose();

                CGlobals.ROFileNotFound = true;
            }
            else
            {
                using (StreamReader sr = new StreamReader(Filename))
                {
                    FileContentArray = sr.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                }

                if (FileContentArray.Count() > 0)
                {
                    PointsServiceClient c = new PointsServiceClient();

                    int LineNumber = 1;
                    foreach (string line in FileContentArray)
                    {
                        try
                        {
                            if (this.SkipImport) break;

                            if (LineNumber > 1)
                            {
                                string[] values = line.Split(',');

                                //
                                // From File
                                //
                                string RO_Number = values[0].Replace("\"", "");
                                DateTime TransactionDate = Convert.ToDateTime(values[1].Replace("\"", ""));
                                string Phone_Number = values[2].Replace("-", "").Replace("\"", "");
                                decimal Sale = Convert.ToDecimal(values[3].Replace("\"", ""));
                                string VIN = values[4].Replace("\"", "");

                                //
                                // Not in file
                                //
                                int Points = 0;
                                int Customer_ID = 0;

                                if (!c.ROExists(CGlobals.AppKey, RO_Number))
                                {
                                    Customer_ID = c.GetCustomerID(CGlobals.AppKey, Phone_Number);

                                    if (Customer_ID != -1)
                                    {
                                        c.RegisterPoints(CGlobals.AppKey, Customer_ID, TransactionDate, RO_Number, VIN, Points, false, "");

                                        CGlobals.Count_New_RO++;

                                        this.lblNew.Text = "Nuevos: " + CGlobals.Count_New_RO.ToString().PadLeft(4, '0');
                                    }
                                    else
                                    {
                                        CGlobals.Count_Rejected_RO++;

                                        this.lblRejected.Text = "Rechazados: " + CGlobals.Count_Rejected_RO.ToString().PadLeft(4, '0');
                                    }
                                }
                                else
                                {
                                    CGlobals.Count_Repeated_RO++;

                                    this.lblExisting.Text = "Repetidos: " + CGlobals.Count_Repeated_RO.ToString().PadLeft(4, '0');
                                }
                            }

                            LineNumber++;

                            Application.DoEvents();
                        }
                        catch
                        {
                            CGlobals.Count_Rejected_RO++;

                            this.lblRejected.Text = "Rechazados: " + CGlobals.Count_Rejected_RO.ToString().PadLeft(4, '0');
                        }
                    }

                    File.Delete(Filename);
                }
            }
        }

        private void frmImportCustomers_Load(object sender, EventArgs e)
        {

        }

        private void frmImportCustomers_Enter(object sender, EventArgs e)
        {

        }

        private void frmImportCustomers_Activated(object sender, EventArgs e)
        {

        }

        bool SkipImport = false;
        private void lblSkip_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SkipImport = true;
        }

        private void cmdLater_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cmdImportNow_Click(object sender, EventArgs e)
        {
            try
            {
                //
                // Load Customers
                //
                Application.DoEvents();

                this.LoadCustomers();

                //
                // Reset label counters
                //
                this.lblNew.Text = "Nuevos: 0000";
                this.lblExisting.Text = "Existentes: 0000";

                Application.DoEvents();

                System.Threading.Thread.Sleep(1000);

                //
                // Load ROs
                //
                Application.DoEvents();

                this.LoadROs();

                Application.DoEvents();

                System.Threading.Thread.Sleep(1000);
            }
            catch (Exception E)
            {
                this.Dispose();

                MessageBox.Show(E.Message, "Import File Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                this.Dispose();

                Cursor = Cursors.Default;
            }
        }
    }
}
