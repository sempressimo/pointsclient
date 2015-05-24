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
    public partial class frmViewCustomers : Form
    {
        public frmViewCustomers()
        {
            InitializeComponent();
        }

        protected void LoadCustomers()
        {
            PointsServiceClient c = new PointsServiceClient();

            DataTable dt = c.GetCustomers(CGlobals.AppKey, this.txtName.Text);

            this.dataGridView1.DataSource = dt;

            this.dataGridView1.Columns[0].Visible = false;

            this.dataGridView1.Columns[1].HeaderText = "Cliente";
            this.dataGridView1.Columns[2].HeaderText = "Teléfono";
            this.dataGridView1.Columns[3].HeaderText = "PIN";

            this.lblCustomerCount.Text = "# clientes: " + dt.Rows.Count.ToString("N0");
        }

        private void frmViewCustomers_Load(object sender, EventArgs e)
        {
            try
            {
                //this.LoadCustomers();
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

        private void cmdClose_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;

            this.Dispose();
        }

        private void cmdSelect_Click(object sender, EventArgs e)
        {
            try
            {
                CGlobals.TempCustomer = this.dataGridView1.SelectedRows[0].Cells[2].Value.ToString();

                DialogResult = System.Windows.Forms.DialogResult.OK;

                this.Dispose();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Está seguro que quiere borrar este cliente?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    frmAdminPassword manager = new frmAdminPassword();
                    DialogResult r = manager.ShowDialog();

                    if (r == System.Windows.Forms.DialogResult.OK)
                    {
                        if (this.dataGridView1.SelectedRows != null)
                        {
                            PointsServiceClient c = new PointsServiceClient();

                            int Customer_ID = Convert.ToInt32(this.dataGridView1.SelectedRows[0].Cells[0].Value);

                            c.DeleteCustomer(CGlobals.AppKey, Customer_ID);

                            MessageBox.Show("El cliente fue borrado del sistema.", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.LoadCustomers();
                        }
                        else
                        {
                            MessageBox.Show("Favor de escojer el cliente de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
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

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                this.LoadCustomers();
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
    }
}
