using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Points_Server.PointsService;

namespace Points_Server
{
    public partial class frmMain : Form
    {
        PointsServiceClient pService = new PointsServiceClient();

        //
        // Member Vars
        //
        int LineNumber = 0;
        private bool Importing = false;
        private bool FullDetail = false;

        public frmMain()
        {
            InitializeComponent();
        }

        protected void LoadCustomers()
        {
            string Filename = pService.GetFilesPath(CGlobals.AppKey, 1) + @"\Customers.csv";

            this.WriteToOutput("Looking for " + Filename + "...");

            string[] FileContentArray;

            if (File.Exists(Filename))
            {
                this.WriteToOutput("Customers.csv file found. Starting Import...");

                using (StreamReader sr = new StreamReader(Filename))
                {
                    FileContentArray = sr.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                }

                if (FileContentArray.Count() > 0)
                {
                    if (FileContentArray.Count() == 1)
                    {
                        this.WriteToOutput("Customers.csv contains only 1 line. This can be a format error. Records must be ended with line feed and carriage return (Enter).");

                        return;
                    }

                    PointsServiceClient c = new PointsServiceClient();

                    this.WriteToOutput("File contains " + FileContentArray.Count().ToString() + " records...");

                    this.lblNewEntries.Text = "";

                    this.progressBar.Value = 0;
                    this.progressBar.Minimum = 0;
                    this.progressBar.Maximum = FileContentArray.Count();

                    LineNumber = 1;
                    foreach (string line in FileContentArray)
                    {
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

                                this.lblNewEntries.Text = "New: " + CGlobals.Count_New_Customers.ToString();
                            }
                            else
                            {
                                CGlobals.Count_Updated_Customers++;
                            }
                        }

                        LineNumber++;

                        this.progressBar.Value++;

                        Application.DoEvents();
                    }

                    this.WriteToOutput("Customers.csv import complete. Summary: New (" + CGlobals.Count_New_Customers.ToString() + "), Existing(" + CGlobals.Count_Updated_Customers.ToString() + ").");

                    File.Delete(Filename);

                    this.WriteToOutput("Customers.csv deleted.");
                }
                else
                {
                    this.WriteToOutput("Customers.csv contains no records.");
                }
            }
        }

        private void LoadROs()
        {
            string[] FileContentArray;

            string Filename = pService.GetFilesPath(CGlobals.AppKey, 1) + @"\RO.csv";

            this.WriteToOutput("Looking for " + Filename + "...");

            if (File.Exists(Filename))
            {
                using (StreamReader sr = new StreamReader(Filename))
                {
                    FileContentArray = sr.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                }

                this.WriteToOutput("File contains " + FileContentArray.Count().ToString() + " records...");

                if (FileContentArray.Count() > 0)
                {
                    PointsServiceClient c = new PointsServiceClient();

                    this.lblNewEntries.Text = "";

                    this.progressBar.Value = 0;
                    this.progressBar.Minimum = 0;
                    this.progressBar.Maximum = FileContentArray.Count();

                    LineNumber = 1;
                    foreach (string line in FileContentArray)
                    {
                        try
                        {
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
                                int Points = Convert.ToInt32(Convert.ToDecimal(Sale));
                                int Customer_ID = 0;

                                if (!c.ROExists(CGlobals.AppKey, RO_Number))
                                {
                                    Customer_ID = c.GetCustomerID(CGlobals.AppKey, Phone_Number);

                                    if (Customer_ID != -1 && Phone_Number.Trim() != "")
                                    {
                                        c.RegisterPoints(CGlobals.AppKey, Customer_ID, TransactionDate, RO_Number, VIN, Points, false, "$ Servicio: " + Sale.ToString("0.00"));

                                        CGlobals.Count_New_RO++;

                                        this.lblNewEntries.Text = "New: " + CGlobals.Count_New_RO.ToString();
                                    }
                                    else
                                    {
                                        if (FullDetail)
                                        {
                                            this.WriteToOutput("Rejected RO entry with values: Phone Number = " + Phone_Number + ", Customer ID: " + Customer_ID.ToString());
                                        }

                                        CGlobals.Count_Rejected_RO++;
                                    }
                                }
                                else
                                {
                                    CGlobals.Count_Repeated_RO++;
                                }
                            }

                            LineNumber++;

                            this.progressBar.Value++;

                            Application.DoEvents();
                        }
                        catch(Exception E)
                        {
                            if (FullDetail)
                            {
                                this.WriteToOutput("Rejected RO. Reason: " + E.Message);
                            }

                            CGlobals.Count_Rejected_RO++;
                        }
                    }

                    this.WriteToOutput("RO.csv import complete. Summary: New (" + CGlobals.Count_New_RO.ToString() + "), Existing(" + CGlobals.Count_Repeated_RO.ToString() + "), Rejected(" + CGlobals.Count_Rejected_RO.ToString() + ").");

                    File.Delete(Filename);

                    this.WriteToOutput("RO.csv deleted.");
                }
            }
        }

        private void WriteToOutput(string Message)
        {
            this.txtOutput.Text += "[" + DateTime.Now.ToString() + "] " + Message + Environment.NewLine + Environment.NewLine;

            Application.DoEvents();
        }

        private void RunImport()
        {
            try
            {
                if (!this.Importing)
                {
                    this.Importing = true;

                    this.progressBar.Value = 0;

                    this.LoadCustomers();

                    this.LoadROs();

                    this.lblNewEntries.Text = "Last import: " + DateTime.Now.ToString();

                    this.Importing = false;
                }
                else
                {
                    this.WriteToOutput("Process is already running. Skipping command. If this is an error please reset the app.");
                }
            }
            catch
            {
                this.Importing = false;

                throw;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                this.RunImport();
            }
            catch (Exception E)
            {
                this.WriteToOutput(E.Message + " Line Number: " + this.LineNumber.ToString());
            }
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Importing = false;

            this.progressBar.Value = 0;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("(2014) CODEPR - Todos los derechos reservados.", "Acerca de", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCustomersFilePath o = new frmCustomersFilePath();
            o.ShowDialog();
        }

        private void forceRunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.RunImport();
            }
            catch (Exception E)
            {
                this.WriteToOutput(E.Message + " Line Number: " + this.LineNumber.ToString());
            }
        }

        private void initDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInitDB o = new frmInitDB();
            o.ShowDialog();
        }

        private void clearLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.txtOutput.Text = "";

            Application.DoEvents();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void toogleDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.FullDetail = !this.FullDetail;
        }
    }
}
