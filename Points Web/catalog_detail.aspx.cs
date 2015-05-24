using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Points_Web
{
    public partial class catalog_detail : System.Web.UI.Page
    {
        PointsEntities db = new PointsEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    Session["id"] = Request.QueryString["id"];
                    Session["mode"] = Request.QueryString["m"];

                    if (Session["mode"].ToString() == "mod")
                    {
                        this.LoadRecord();
                    }
                }
                catch (Exception E)
                {
                    this.lblFeedback.Text = E.Message;
                }
            }
        }

        protected void LoadRecord()
        {
            int id = Convert.ToInt32(Session["id"].ToString());

            var Rec = db.Rewards.Single(p => p.Reward_ID == id);

            this.txtRewardDescription.Text = Rec.Reward_Description;
            this.txtRewardName.Text = Rec.Reward_Name;
            this.txtCost.Text = Rec.Cost.Value.ToString();
            this.cbIsActive.Checked = Rec.IsActive.Value;
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("reward_categories_list.aspx");
        }

        protected void cmdOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["mode"].ToString() == "add")
                {
                    Rewards R = new Rewards();

                    R.Reward_Name = this.txtRewardName.Text;
                    R.Reward_Description = this.txtRewardDescription.Text;
                    R.Cost = Convert.ToInt32(this.txtCost.Text);

                    this.db.AddToRewards(R);
                    this.db.SaveChanges();

                    Response.Redirect("catalog_admin.aspx");
                }
                else if (Session["mode"].ToString() == "mod")
                {
                    Rewards R = new Rewards();

                    R.Reward_Name = this.txtRewardName.Text;
                    R.Reward_Description = this.txtRewardDescription.Text;
                    R.Cost = Convert.ToInt32(this.txtCost.Text);

                    this.db.SaveChanges();

                    Response.Redirect("catalog_admin.aspx");
                }
            }
            catch (Exception E)
            {
                this.lblFeedback.Text = E.Message;
            }
        }
    }
}