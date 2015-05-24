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
    public partial class frmReward : Form
    {
        int Reward_ID = 0;
        public frmReward(int Reward_ID)
        {
            InitializeComponent();

            this.Reward_ID = Reward_ID;
        }

        private void LoadCategories()
        {
            PointsServiceClient c = new PointsServiceClient();

            DataTable dtRewards = c.GetAllRewardCategories(CGlobals.AppKey);

            this.cmbCategory.DisplayMember = "Reward_Category_Description";
            this.cmbCategory.ValueMember = "Reward_Category_ID";
            this.cmbCategory.DataSource = dtRewards;
        }

        private void LoadReward()
        {
            PointsServiceClient c = new PointsServiceClient();

            DataTable r = c.GetReward(CGlobals.AppKey, this.Reward_ID);

            this.txtName.Text = r.Rows[0]["Reward_Name"].ToString();
            this.txtDescription.Text = r.Rows[0]["Reward_Description"].ToString();
            this.cmbCategory.SelectedValue = r.Rows[0]["Reward_Category_ID"].ToString();
            this.txtCost.Text = Convert.ToInt32(r.Rows[0]["Cost"]).ToString();
            this.txtCashForPoints.Text = Convert.ToDecimal(r.Rows[0]["CashForPoints"]).ToString("0.00");
            
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            this.Dispose();
        }

        private void AddReward()
        {
            int Cost = 0;
            decimal CashForPoints = 0;

            try
            {
                Cost = Convert.ToInt32(this.txtCost.Text);
            }
            catch
            {
                MessageBox.Show("Favor de entrar una cantidad valida en el costo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                this.txtCost.Focus();

                return;
            }

            try
            {
                CashForPoints = Convert.ToDecimal(this.txtCashForPoints.Text);
            }
            catch
            {
                MessageBox.Show("Favor de entrar una cantidad valida en cash por puntos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                this.txtCashForPoints.Focus();

                return;
            }

            PointsServiceClient c = new PointsServiceClient();

            c.RegisterReward(CGlobals.AppKey, this.txtName.Text, Convert.ToInt32(this.cmbCategory.SelectedValue), this.txtDescription.Text, Cost, this.cbIsActive.Checked, CashForPoints);

            MessageBox.Show("El premio fue registrado.", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Dispose();
        }

        private void UpdateReward()
        {
            int Cost = 0;
            decimal CashForPoints = 0;

            try
            {
                Cost = Convert.ToInt32(this.txtCost.Text);
            }
            catch
            {
                MessageBox.Show("Favor de entrar una cantidad valida en el costo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                this.txtCost.Focus();

                return;
            }

            try
            {
                CashForPoints = Convert.ToDecimal(this.txtCashForPoints.Text);
            }
            catch
            {
                MessageBox.Show("Favor de entrar una cantidad valida en cash por puntos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                this.txtCashForPoints.Focus();

                return;
            }

            PointsServiceClient c = new PointsServiceClient();

            c.UpdateReward(CGlobals.AppKey, this.Reward_ID, this.txtName.Text, Convert.ToInt32(this.cmbCategory.SelectedValue), this.txtDescription.Text, Cost, this.cbIsActive.Checked, CashForPoints);

            MessageBox.Show("El premio fue actualizado.", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Dispose();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                if (this.Reward_ID > 0)
                {
                    this.UpdateReward();
                }
                else
                {
                    this.AddReward();
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

        private void frmReward_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                this.LoadCategories();

                if (this.Reward_ID > 0)
                {
                    this.LoadReward();
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
    }
}
