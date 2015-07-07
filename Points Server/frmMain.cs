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
using System.Xml;
using System.Reflection;

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
            string ImportPath = pService.GetFilesPath(CGlobals.AppKey, 1);

            string Filename = ImportPath + @"\Customers.csv";

            this.WriteToOutput("Looking for " + Filename + "...");

            string[] FileContentArray;

            if (File.Exists(Filename))
            {
                this.WriteToOutput("Customers.csv file found. Starting Import...");

                using (StreamReader sr = new StreamReader(Filename))
                {
                    FileContentArray = sr.ReadToEnd().Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                }

                if (FileContentArray.Count() > 0)
                {
                    if (FileContentArray.Count() == 1)
                    {
                        this.WriteToOutput("Customers.csv contains only 1 line. This can be a format error. Records must be ended with line feed and carriage return (Enter).");

                        return;
                    }
                    else if (FileContentArray.Count() == 2)
                    {
                        this.WriteToOutput("Customers.csv contains only 2 lines. This can be a format error. Records must be ended with line feed and carriage return (Enter).");
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

                    string MoveTo = ImportPath + @"\imported";

                    if (!Directory.Exists(MoveTo))
                    {
                        MessageBox.Show("Backup directory: " + MoveTo + " does not exists. Will be created.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Directory.CreateDirectory(MoveTo);
                    }

                    string UniqueID = Guid.NewGuid().ToString();

                    File.Move(Filename, MoveTo + @"\customers." + DateTime.Today.ToString("yyyyMMdd") + "." + UniqueID + ".csv");

                    this.WriteToOutput(@"RO.csv moved to \imported.");

                    File.WriteAllText(MoveTo + @"\customers_log." + DateTime.Today.ToString("yyyyMMdd") + "." + UniqueID + ".txt", this.txtOutput.Text);
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

            string ImportPath = pService.GetFilesPath(CGlobals.AppKey, 1);

            string Filename = ImportPath + @"\RO.csv";

            this.WriteToOutput("Looking for " + Filename + "...");

            if (File.Exists(Filename))
            {
                using (StreamReader sr = new StreamReader(Filename))
                {
                    FileContentArray = sr.ReadToEnd().Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
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

                    string MoveTo = ImportPath + @"\imported";

                    if (!Directory.Exists(MoveTo))
                    {
                        MessageBox.Show("Backup directory: " + MoveTo + " does not exists. Will be created.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Directory.CreateDirectory(MoveTo);
                    }

                    string UniqueID = Guid.NewGuid().ToString(); 

                    File.Move(Filename, MoveTo + @"\ro." + DateTime.Today.ToString("yyyyMMdd") + "." + UniqueID + ".csv");

                    this.WriteToOutput(@"RO.csv moved to \imported.");

                    File.WriteAllText(MoveTo + @"\ro_log." + DateTime.Today.ToString("yyyyMMdd") + "." + UniqueID + ".txt", this.txtOutput.Text);
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

        public static Version GetPublishedVersion()
        {
            XmlDocument xmlDoc = new XmlDocument();
            Assembly asmCurrent = System.Reflection.Assembly.GetExecutingAssembly();
            string executePath = new Uri(asmCurrent.GetName().CodeBase).LocalPath;

            xmlDoc.Load(executePath + ".manifest");
            string retval = string.Empty;
            if (xmlDoc.HasChildNodes)
            {
                retval = xmlDoc.ChildNodes[1].ChildNodes[0].Attributes.GetNamedItem("version").Value.ToString();
            }
            return new Version(retval);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Version v = GetPublishedVersion();

            MessageBox.Show("(2014) CODEPR (Ismael Placa) - Todos los derechos reservados." + Environment.NewLine + Environment.NewLine + "Version: " + v.Major + "." + v.MajorRevision + "." + v.Minor + "." + v.MinorRevision, "Acerca de", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void frmMain_Load(object sender, EventArgs e)
        {
            PointsServiceClient c = new PointsServiceClient();

            this.lblEndpoint.Text = "Service Endpoint: " + c.Endpoint.Address.Uri.AbsoluteUri;
        }

        private void sendReminderEmailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                PointsServiceClient c = new PointsServiceClient();
                DataTable Customers = c.GetBalanceReport(CGlobals.AppKey, true);

                this.WriteToOutput("Sending email reminders to " + Customers.Rows.Count.ToString() + " customers, from host: " + Properties.Settings.Default.Smtp + ", Port: " + Properties.Settings.Default.Port + ", From: " + Properties.Settings.Default.From);

                int LoopCtr = 0;
                int MailsSent = 0;
                int MailsNotSent = 0;

                foreach (DataRow r in Customers.Rows)
                {
                    try
                    {
                        LoopCtr++;

                        this.WriteToOutput("Sending email to " + r["email"].ToString());

                        string Outcome = CGlobals.SendEmail(r["Email"].ToString(), r["Customer_Name"].ToString(), r["BAL"].ToString());

                        if (Outcome == "Sent")
                        {
                            MailsSent++;

                            if (LoopCtr % 10 == 0)
                            {
                                this.WriteToOutput(MailsSent.ToString() + " emails sent so far...");
                            }
                        }
                        else
                        {
                            this.WriteToOutput(Outcome);
                        }

                        //break; // DEBUG
                    }
                    catch (Exception E)
                    {
                        MailsNotSent++;

                        this.WriteToOutput(E.Message);
                    }
                }

                this.WriteToOutput("Sending email complete. " + MailsSent.ToString() + " emails sent. " + MailsNotSent.ToString() + " had problems, check address.");
            }
            catch (Exception E)
            {
                this.WriteToOutput(E.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void emailSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmailSettings E = new frmEmailSettings();
            E.ShowDialog();
        }
    }
}
