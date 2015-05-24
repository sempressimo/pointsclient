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
    public partial class frmClaimPoints : Form
    {
        string CustomerName = "";
        int Customer_ID = 0;
        int PointsBalance = 0;
        bool EditMode = false;
        int Claim_ID = 0;

        public frmClaimPoints(string CustomerName, int Customer_ID, int PointsBalance, bool EditMode, int Claim_ID)
        {
            InitializeComponent();

            this.CustomerName = CustomerName;
            this.Customer_ID = Customer_ID;
            this.PointsBalance = PointsBalance;

            this.Claim_ID = Claim_ID;

            this.EditMode = EditMode;

            if (this.EditMode)
            {
                PointsServiceClient c = new PointsServiceClient();

                DataTable Claim = c.GetClaim(CGlobals.AppKey, this.Claim_ID);

                this.txtDolarsUsed.Text = Convert.ToDecimal(Claim.Rows[0]["AmountPaid"]).ToString("0.00");

                bool IsPaid = false;
                if (Claim.Rows[0]["Claimed"] != DBNull.Value)
                { 
                    IsPaid = Convert.ToBoolean(Claim.Rows[0]["Claimed"]);
                }

                this.cbRewardPaid.Checked = IsPaid;

                this.txtDolarsUsed.Enabled = this.cmbReward.Enabled = false;

                if (Claim.Rows[0]["PointsUsed"] != DBNull.Value)
                {
                    this.lblPointsForCash.Text = "Puntos: " + Convert.ToDecimal(Claim.Rows[0]["PointsUsed"]).ToString();
                }
                else
                {
                    this.lblPointsForCash.Text = "0";
                }

                this.lblPointsForCash.Visible = true;
            }
        }

        private void frmClaimPoints_Load(object sender, EventArgs e)
        {
            this.txtDolarsUsed.Focus();

            this.lblCustomer.Text = this.CustomerName;
            this.lblCustomer.Tag = this.Customer_ID;
            this.lblBalance.Text = this.PointsBalance.ToString("N0");

            PointsServiceClient c = new PointsServiceClient();

            DataTable dtRewards = c.GetRewards(CGlobals.AppKey);

            this.cmbReward.DisplayMember = "Reward_Name";
            this.cmbReward.ValueMember = "Reward_ID";
            this.cmbReward.DataSource = dtRewards;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private bool ValidateForm()
        {
            try
            {
                int PointsToClaim = Convert.ToInt32(this.lblPointsForCash.Text.Replace("Puntos: ", ""));

                if (PointsToClaim > Convert.ToInt32(this.lblBalance.Text.Replace(",", "")))
                {
                    MessageBox.Show("La cantidad de puntos es mayor al balance.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return false;
                }
            }
            catch
            {
                MessageBox.Show("Favor de entrar una cantidad valida.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return false;
            }

            return true;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                PointsServiceClient c = new PointsServiceClient();

                if (this.EditMode)
                {
                    if (this.cbRewardPaid.Checked)
                    {
                        c.SetClaimAsPaid(CGlobals.AppKey, this.Claim_ID, true);

                        MessageBox.Show("El premio fue marcado como pagado.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Favor de marcar el premio como pagado.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    if (this.ValidateForm())
                    {
                        int PointsUsed = Convert.ToInt32(this.lblPointsForCash.Text.Replace("Puntos: ", ""));

                        decimal DollarsUsed = Convert.ToDecimal(this.txtDolarsUsed.Text.Replace(",", ""));

                        int Reward_ID = Convert.ToInt32(this.cmbReward.SelectedValue);

                        c.ClaimReward(CGlobals.AppKey, Convert.ToInt32(this.lblCustomer.Tag), DateTime.Today, Reward_ID, PointsUsed, "", "", "", "", this.cbRewardPaid.Checked, DollarsUsed);

                        MessageBox.Show("Los puntos fueron reclamados.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Dispose();
                    }
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

        decimal CashUsed = 0;
        decimal PointsUsed = 0;
        decimal Cash = 0;
        private void cmbReward_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.EditMode == false)
                {
                    int Reward_ID = Convert.ToInt32(this.cmbReward.SelectedValue);

                    PointsServiceClient c = new PointsServiceClient();

                    //this.txtDolarsUsed.Text = c.GetRewardCost(CGlobals.AppKey, Reward_ID).ToString();

                    this.Cash = c.GetRewardCashForPoints(CGlobals.AppKey, Reward_ID);

                    if (Cash != -1)
                    {
                        if (Cash != 0)
                        {
                            // Calc the max dollars the customer could exchange
                            this.txtDolarsUsed.Text = (Convert.ToDecimal(this.lblBalance.Text) * this.Cash).ToString("0.00");

                            //this.CalcCashForPoints();
                            this.CalcPointsForCash();
                        }
                    }
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

        private void CalcCashForPoints()
        {
            try
            {
                this.PointsUsed = Convert.ToDecimal(this.txtDolarsUsed.Text);

                this.lblPointsForCash.Text = "Premio: " + (this.PointsUsed * this.Cash).ToString("0.00");
                this.lblPointsForCash.Visible = true;
            }
            catch
            {
                throw new Exception("Favor de entrar una cantidad valida.");
            }
        }

        private void CalcPointsForCash()
        {
            try
            {
                if (this.Cash > 0)
                {
                    this.CashUsed = Convert.ToDecimal(this.txtDolarsUsed.Text);

                    this.lblPointsForCash.Text = "Puntos: " + (this.CashUsed / this.Cash).ToString();
                    this.lblPointsForCash.Visible = true;
                }
            }
            catch
            {
                throw new Exception("Favor de entrar una cantidad valida.");
            }
        }

        private void txtPointsUsed_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //this.CalcCashForPoints();
                this.CalcPointsForCash();
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
    }
}
