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
    public partial class frmCatalog : Form
    {
        public frmCatalog()
        {
            InitializeComponent();
        }

        private void LoadCatalog()
        {
            PointsServiceClient c = new PointsServiceClient();

            DataTable dt = c.GetCatalog(CGlobals.AppKey);

            this.dataGridView1.DataSource = dt;

            this.dataGridView1.Columns[0].Visible = false;

            this.dataGridView1.Columns[1].HeaderText = "Nombre";
            this.dataGridView1.Columns[2].HeaderText = "Categoria";
            this.dataGridView1.Columns[3].HeaderText = "Descripción";
            this.dataGridView1.Columns[4].HeaderText = "Costo";
            this.dataGridView1.Columns[5].HeaderText = "Activo";
            this.dataGridView1.Columns[6].HeaderText = "Cash por punto";

        }

        private void frmCatalog_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                this.LoadCatalog();
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
            this.Dispose();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            frmAdminPassword pass = new frmAdminPassword();
            DialogResult rPass = pass.ShowDialog();

            if (rPass == DialogResult.OK)
            {
                frmReward r = new frmReward(0);
                if (r.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.LoadCatalog();
                }
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    frmAdminPassword pass = new frmAdminPassword();
                    DialogResult rPass = pass.ShowDialog();

                    if (rPass == DialogResult.OK)
                    {
                        Cursor = Cursors.WaitCursor;

                        PointsServiceClient c = new PointsServiceClient();

                        int Reward_ID = Convert.ToInt32(this.dataGridView1.SelectedRows[0].Cells[0].Value);

                        c.DeleteReward(CGlobals.AppKey, Reward_ID);

                        this.LoadCatalog();

                        MessageBox.Show("El premio fue eliminado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Favor de escojer un premio de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void cmdEdit_Click(object sender, EventArgs e)
        {
                        frmAdminPassword pass = new frmAdminPassword();
            DialogResult rPass = pass.ShowDialog();

            if (rPass == DialogResult.OK)
            {
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    int Reward_ID = Convert.ToInt32(this.dataGridView1.SelectedRows[0].Cells[0].Value);

                    frmReward r = new frmReward(Reward_ID);
                    if (r.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        this.LoadCatalog();
                    }
                }
                else
                {
                    MessageBox.Show("Favor de escojer un premio de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
