using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace Points_Web
{
    public partial class login : System.Web.UI.Page
    {
        PointsEntities db = new PointsEntities();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cmdLogin_Click(object sender, EventArgs e)
        {
            try
            {
                this.lblFeedBack.Text = "";

                var user = db.Customers.SingleOrDefault(p => p.Phone_Number == this.txtUser.Value && p.Password == this.txtPassword.Value);

                if (user != null)
                {
                    Session["Customer_ID"] = user.Customer_ID;

                    FormsAuthentication.RedirectFromLoginPage(this.txtUser.Value, false);
                }
                else
                {
                    this.lblFeedBack.Text = "La cuenta no es valida.";
                }
            }
            catch (Exception E)
            {
                this.lblFeedBack.Text = E.Message;   
            }
        }
    }
}