using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Points_Web
{
    public partial class catalog_admin : System.Web.UI.Page
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

        protected void lbAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("catalog_detail.aspx?m=add");
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
    }
}