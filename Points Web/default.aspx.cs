using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Points_Web
{
    public partial class _default : System.Web.UI.Page
    {
        PointsEntities db = new PointsEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.LoadCustomerData(Convert.ToInt32(Session["Customer_ID"]));
            }
        }

        protected void LoadCustomerData(int Customer_ID)
        {
            var T = this.db.Transactions.Where(p => p.Customer_ID == Customer_ID);

            this.lvTransactions.DataSource = T;
            this.lvTransactions.DataBind();

            var C = this.db.Claims.Where(p => p.Customer_ID == Customer_ID);

            this.lvClaims.DataSource = C;
            this.lvClaims.DataBind();

            this.lblPoints.Text = this.GetBalance(Customer_ID).ToString("000");
        }

        protected int GetBalance(int Customer_ID)
        {
            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string Sql = "SELECT SUM(Points) AS Points FROM Transactions WHERE Customer_ID = " + Customer_ID.ToString();

                SqlCommand cm = new SqlCommand(Sql, cn);
                cm.CommandType = CommandType.Text;

                object PointsTotal = cm.ExecuteScalar();

                if (PointsTotal == DBNull.Value)
                {
                    return 0;
                }
                else
                {
                    Sql = "SELECT SUM(PointsUsed) AS PointsUsed FROM Claims WHERE Customer_ID = " + Customer_ID.ToString();

                    cm = new SqlCommand(Sql, cn);

                    object PointsUsed = cm.ExecuteScalar();

                    if (PointsUsed == DBNull.Value)
                    {
                        return Convert.ToInt32(PointsTotal);
                    }
                    else
                    {
                        return Convert.ToInt32(PointsTotal) - Convert.ToInt32(PointsUsed);
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }
    }
}