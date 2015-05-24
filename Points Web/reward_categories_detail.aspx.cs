using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Points_Web
{
    public partial class reward_categories_detail : System.Web.UI.Page
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

            var Rec = db.Reward_Categories.Single(p => p.Reward_Category_ID == id);

            this.txtDescription.Text = Rec.Reward_Category_Description;
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
                    Reward_Categories R = new Reward_Categories();

                    R.Reward_Category_Description = this.txtDescription.Text;

                    this.db.AddToReward_Categories(R);
                    this.db.SaveChanges();

                    Response.Redirect("reward_categories_list.aspx");
                }
                else if (Session["mode"].ToString() == "mod")
                {
                    Reward_Categories R = new Reward_Categories();

                    R.Reward_Category_Description = this.txtDescription.Text;

                    this.db.SaveChanges();

                    Response.Redirect("reward_categories_list.aspx");
                }
            }
            catch (Exception E)
            {
                this.lblFeedback.Text = E.Message;
            }
        }
    }
}