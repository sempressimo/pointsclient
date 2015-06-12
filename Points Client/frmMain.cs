using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Points_Client.PointsServiceReference;
using System.Diagnostics;
using System.IO;

namespace Points_Client
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("(2014) CODEPR - Todos los derechos reservados. Version: 1.1", "Acerca de",  MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cmdRegisterPoints_Click(object sender, EventArgs e)
        {
            frmRegisterPoints r = new frmRegisterPoints(this.txtFullName.Text, this.Customer_ID, this.Customer_VINs);
            r.ShowDialog();

            this.CustomerSearch();
        }

        private void cmdRegisterCustomer_Click(object sender, EventArgs e)
        {
            frmRegisterCustomer c = new frmRegisterCustomer();
            DialogResult r =  c.ShowDialog();

            if (r == System.Windows.Forms.DialogResult.OK)
            {
                this.txtPhoneSearch.Text = CGlobals.TempCustomer;

                this.CustomerSearch();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblWebPin_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this.lblWebPin.Tag.ToString(), "Web PIN");
        }

        protected void CustomerSearch()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                txtPhoneSearch.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                if (this.txtPhoneSearch.Text.Trim() != "")
                {
                    PointsServiceClient c = new PointsServiceClient();

                    DataTable dt = c.GetCustomer(CGlobals.AppKey, this.txtFullName.Text, this.txtPhoneSearch.Text);

                    if (dt.Rows.Count == 0)
                    {
                        this.panel2.BackColor = Color.Tan;

                        this.panel1.Enabled = this.panel2.Enabled = false;

                        this.txtEmail.Text = this.txtFullName.Text = this.txtPhone.Text = this.txtVINs.Text = "";

                        MessageBox.Show("El cliente no fue encontrado.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        this.Customer_ID = Convert.ToInt32(dt.Rows[0]["Customer_ID"]);

                        this.txtFullName.Text = dt.Rows[0]["Customer_Name"].ToString();
                        this.txtPhone.Text = dt.Rows[0]["Phone_Number"].ToString();
                        this.lblWebPin.Tag = dt.Rows[0]["Password"].ToString();
                        this.txtEmail.Text = dt.Rows[0]["Email"].ToString();
                        this.Customer_VINs = this.txtVINs.Text = dt.Rows[0]["VINs"].ToString();

                        DataTable dtTransactions = c.GetTransactions(CGlobals.AppKey, this.Customer_ID.ToString());

                        this.dataGridView1.DataSource = dtTransactions;

                        this.dataGridView1.Columns[0].Visible = false;
                        this.dataGridView1.Columns[1].Visible = false;

                        this.dataGridView1.Columns[2].HeaderText = "Fecha";
                        this.dataGridView1.Columns[3].HeaderText = "RO #";
                        this.dataGridView1.Columns[4].HeaderText = "VIN";
                        this.dataGridView1.Columns[5].HeaderText = "Puntos";
                        this.dataGridView1.Columns[6].HeaderText = "Es regalo";
                        this.dataGridView1.Columns[7].HeaderText = "Comentarios";

                        this.panel1.Enabled = this.panel2.Enabled = true;

                        //
                        // Claims
                        //
                        DataTable dtClaims = null;

                        if (dtTransactions.Rows.Count > 0)
                        {
                            dtClaims = c.GetClaims(CGlobals.AppKey, this.Customer_ID.ToString());

                            this.gvClaims.DataSource = dtClaims;

                            this.gvClaims.Columns[0].Visible = true;
                            this.gvClaims.Columns[1].Visible = false;
                            this.gvClaims.Columns[3].Visible = false;
                            this.gvClaims.Columns[5].Visible = false;
                            this.gvClaims.Columns[6].Visible = false;
                            this.gvClaims.Columns[7].Visible = false;

                            this.gvClaims.Columns[0].HeaderText = "ID";
                            this.gvClaims.Columns[2].HeaderText = "Fecha";
                            this.gvClaims.Columns[9].HeaderText = "Pagado";
                            this.gvClaims.Columns[10].HeaderText = "Cantidad";
                            this.gvClaims.Columns[11].HeaderText = "Premio";

                            DataGridViewCellStyle m = new DataGridViewCellStyle();
                            m.Format = "0.00";
                            this.gvClaims.Columns[10].DefaultCellStyle = m;

                            this.panel2.BackColor = Color.White;
                        }

                        //
                        // Balance
                        //
                        if (dtTransactions.Rows.Count > 0)
                        {
                            this.lblPointsBalance.Text = c.GetPointsBalance(CGlobals.AppKey, this.Customer_ID).ToString("N0");
                        }
                        else
                        {
                            this.lblPointsBalance.Text = "0";
                        }

                        //
                        // Paint Rows
                        //
                        /*
                        foreach (DataGridViewRow row in gvClaims.Rows)
                        {
                            if (row.Cells[9].Value != DBNull.Value)
                            {
                                if (Convert.ToBoolean(row.Cells[9].Value) == true)
                                {
                                    row.DefaultCellStyle.BackColor = Color.Red;
                                }
                            }
                        }
                        */
                    }
                }
                else
                {
                    MessageBox.Show("Por favor entre el número de teléfono registrado.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
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

        int Customer_ID = 0;
        string Customer_VINs = "";
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            this.CustomerSearch();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                PointsServiceClient c = new PointsServiceClient();

                this.lblWS.Text = "WS: " + c.Endpoint.Address.Uri.AbsoluteUri;

                Application.DoEvents();

                //frmImportCustomers cust = new frmImportCustomers();
                //cust.ShowDialog();
                
                this.txtPhoneSearch.Focus();
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

        private void cmdUsePoints_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(this.lblPointsBalance.Text.Replace(",", "")) > 0)
                {
                    frmClaimPoints p = new frmClaimPoints(this.txtFullName.Text, this.Customer_ID, Convert.ToInt32(this.lblPointsBalance.Text.Replace(",", "")), false, -1);
                    p.ShowDialog();

                    this.CustomerSearch();
                }
                else
                {
                    MessageBox.Show("Este cliente no tiene puntos acumulados.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cmdViewCustomers_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                frmViewCustomers c = new frmViewCustomers();
                DialogResult r = c.ShowDialog();

                if (r == System.Windows.Forms.DialogResult.OK)
                {
                    this.txtPhoneSearch.Text = CGlobals.TempCustomer;

                    this.CustomerSearch();
                }
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

        private void verCatalogoDePremiosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                frmCatalog c = new frmCatalog();
                c.ShowDialog();
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

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBackup b = new frmBackup();
            b.ShowDialog();
        }

        private void txtPhoneSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.CustomerSearch();
            }
        }

        private void cmdUpdateCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                PointsServiceClient c = new PointsServiceClient();

                this.txtPhone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

                c.UpdateCustomer(CGlobals.AppKey, this.Customer_ID, this.txtFullName.Text, this.txtPhone.Text, this.lblWebPin.Tag.ToString(), this.txtVINs.Text, this.txtEmail.Text);

                this.Customer_VINs = this.txtVINs.Text;

                MessageBox.Show("Los datos del cliente fueron actualizados.", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void cmdGift_Click(object sender, EventArgs e)
        {
            frmGiftPoints g = new frmGiftPoints(this.txtFullName.Text, this.Customer_ID, this.Customer_VINs);
            g.ShowDialog();

            this.CustomerSearch();
        }

        private void cambiarPasswordDeGerenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword p = new frmChangePassword();
            p.ShowDialog();
        }

        private void inicializarBaseDeDatosAdminOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInitDB db = new frmInitDB();
            db.ShowDialog();
        }

        private void gvClaims_DoubleClick(object sender, EventArgs e)
        {
            if (this.gvClaims.SelectedRows != null)
            {
                int Claim_ID = Convert.ToInt32(this.gvClaims.SelectedRows[0].Cells[0].Value);

                frmClaimPoints p = new frmClaimPoints(this.txtFullName.Text, this.Customer_ID, Convert.ToInt32(this.lblPointsBalance.Text.Replace(",", "")), true, Claim_ID);
                p.ShowDialog();

                this.CustomerSearch();
            }
        }

        private void puntosRegaladosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportViewer2 v = new frmReportViewer2("Transactions");
            v.ShowDialog();
        }

        private void puntosObsequiadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportViewer2 v = new frmReportViewer2("Gift");
            v.ShowDialog();
        }

        private void premiosReclamadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportViewer2 v = new frmReportViewer2("Claims");
            v.ShowDialog();
        }

        private void cmdOpenPortal_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://www.vaultgate.com/actpoints");
        }

        private void importarClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCustomersFilePath p = new frmCustomersFilePath();
            p.ShowDialog();
        }

        private void cmdReadCard_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                this.txtPhoneSearch.Text = "1111111111";

                this.CustomerSearch();
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

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }

        private void gvClaims_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 9)
            {
                if (e.Value != DBNull.Value)
                {
                    if (Convert.ToBoolean(e.Value) == false)
                    {
                        e.CellStyle.BackColor = Color.Red;
                    }
                    else
                    {
                        e.CellStyle.BackColor = Color.Green;
                    }
                }
            }
        }

        private void balancesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportViewer2 v = new frmReportViewer2("Balance");
            v.ShowDialog();
        }
    }
}
