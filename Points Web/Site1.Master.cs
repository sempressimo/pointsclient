using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Points_Web
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        PointsEntities db = new PointsEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    int ID = Convert.ToInt32(Session["Customer_ID"]);

                    var C = this.db.Customers.Single(p => p.Customer_ID == ID);

                    this.lblCustomerName.Text = C.Customer_Name;
                }
            }
            catch (Exception E)
            {
                Response.Write(E.Message);
            }
        }
    }
}