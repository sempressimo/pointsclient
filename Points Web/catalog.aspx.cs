using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Points_Web
{
    public partial class catalog : System.Web.UI.Page
    {
        PointsEntities db = new PointsEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    this.LoadRecords();
                }
            }
            catch (Exception E)
            {
                this.lblFeedback.Text = E.Message;
            }
        }

        protected void LoadRecords()
        {
            var R = db.Rewards
                    .Where(p =>
                        p.Reward_Name.Contains(this.txtDescription.Text) || string.IsNullOrEmpty(this.txtDescription.Text)
                    ).OrderBy(o => o.Reward_Name);

            this.gvRecords.DataSource = R;
            this.gvRecords.DataBind();
        }

        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.LoadRecords();
            }
            catch (Exception E)
            {
                this.lblFeedback.Text = E.Message;
            }
        }

        protected void gvRecords_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int ID = Convert.ToInt32(this.gvRecords.DataKeys[e.RowIndex].Value.ToString());

                var PM = this.db.Rewards.Single(p => p.Reward_ID == ID);

                this.db.DeleteObject(PM);
                this.db.SaveChanges();

                this.LoadRecords();
            }
            catch (Exception E)
            {
                this.lblFeedback.Text = E.Message;
            }
        }

        protected int GetRewardCost(int Reward_ID)
        {
            Rewards R = this.db.Rewards.Single(p => p.Reward_ID == Reward_ID);

            return R.Cost.Value;
        }

        protected void RedeemPrize(int Reward_ID)
        {
            Claims C = new Claims();

            C.Customer_ID = Convert.ToInt32(Session["Customer_ID"]);
            C.Reward_ID = Reward_ID;
            C.Address = "";
            C.Address2 = "";
            C.Town = "";
            C.ZipCode = "";
            C.Date = DateTime.Today;
            C.PointsUsed = this.GetRewardCost(Reward_ID);

            this.db.AddToClaims(C);
            this.db.SaveChanges();

            int Claim_ID = C.Claim_ID;

            Response.Redirect("claim_online_reward.aspx?cus=" + Session["Customer_ID"].ToString() + "&cid=" + Claim_ID.ToString());
        }

        protected void gvRecords_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int Row_ID = Convert.ToInt32(e.CommandArgument);

                int Reward_ID = Convert.ToInt32(this.gvRecords.DataKeys[Row_ID].Value);

                this.RedeemPrize(Reward_ID);
            }
            catch (Exception E)
            {
                this.lblFeedback.Text = E.Message;
            }
        }
    }
}