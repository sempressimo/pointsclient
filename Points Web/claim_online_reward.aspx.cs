using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZXing;
using ZXing.QrCode;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace Points_Web
{
    public partial class claim_online_reward : System.Web.UI.Page
    {
        PointsEntities db = new PointsEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            int Customer_ID = Convert.ToInt32(Request.QueryString["cus"]);

            int Claim_ID = Convert.ToInt32(Request.QueryString["cid"]);

            if (this.ValidateReward(Customer_ID, Claim_ID))
            {
                this.GenerateQR(Customer_ID, Claim_ID);
            }
            else
            {
                Response.Redirect("not_found.aspx");
            }
        }

        protected bool ValidateReward(int Customer_ID, int Claim_ID)
        {
            var Claim = this.db.Claims.SingleOrDefault(p => p.Customer_ID == Customer_ID && p.Claim_ID == Claim_ID);

            if (Claim != null)
            {
                this.lblRewardName.Text = Claim.Rewards.Reward_Name;
                this.lblDescription.Text = Claim.Rewards.Reward_Description;

                return true;
            }

            return false;
        }

        protected void GenerateQR(int Customer_ID, int Claim_ID)
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = 150,
                    Width = 150,
                }
            };

            Bitmap QrCode = writer.Write(Customer_ID.ToString("00000") + Claim_ID.ToString("00000"));  // works

            MemoryStream ms = new MemoryStream();
            QrCode.Save(ms, ImageFormat.Png);
            var base64Data = Convert.ToBase64String(ms.ToArray());
            imgCtrl.Src = "data:image/gif;base64," + base64Data;
        }
    }
}