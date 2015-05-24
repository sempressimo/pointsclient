namespace Points_Client
{
    partial class frmClaimPoints
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDolarsUsed = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBalance = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbReward = new System.Windows.Forms.ComboBox();
            this.cbRewardPaid = new System.Windows.Forms.CheckBox();
            this.lblPointsForCash = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Gold;
            this.pictureBox1.Location = new System.Drawing.Point(-18, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(868, 47);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Gold;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(251, 9);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(182, 26);
            this.label4.TabIndex = 13;
            this.label4.Text = "Reclamar Premio";
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(124, 345);
            this.cmdOK.Margin = new System.Windows.Forms.Padding(6);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(150, 44);
            this.cmdOK.TabIndex = 3;
            this.cmdOK.Text = "&OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(286, 345);
            this.cmdCancel.Margin = new System.Windows.Forms.Padding(6);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(150, 44);
            this.cmdCancel.TabIndex = 4;
            this.cmdCancel.Text = "&Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.ForeColor = System.Drawing.Color.Teal;
            this.lblCustomer.Location = new System.Drawing.Point(112, 84);
            this.lblCustomer.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(184, 26);
            this.lblCustomer.TabIndex = 19;
            this.lblCustomer.Text = "Customer Name..";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 84);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 26);
            this.label5.TabIndex = 18;
            this.label5.Text = "Cliente:";
            // 
            // txtDolarsUsed
            // 
            this.txtDolarsUsed.Location = new System.Drawing.Point(117, 220);
            this.txtDolarsUsed.Margin = new System.Windows.Forms.Padding(6);
            this.txtDolarsUsed.Name = "txtDolarsUsed";
            this.txtDolarsUsed.Size = new System.Drawing.Size(101, 32);
            this.txtDolarsUsed.TabIndex = 1;
            this.txtDolarsUsed.TextChanged += new System.EventHandler(this.txtPointsUsed_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 220);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 26);
            this.label1.TabIndex = 17;
            this.label1.Text = "Dólares:";
            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.ForeColor = System.Drawing.Color.Teal;
            this.lblBalance.Location = new System.Drawing.Point(112, 128);
            this.lblBalance.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(60, 26);
            this.lblBalance.TabIndex = 21;
            this.lblBalance.Text = "0000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 128);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 26);
            this.label3.TabIndex = 20;
            this.label3.Text = "Balance:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 174);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 26);
            this.label2.TabIndex = 22;
            this.label2.Text = "Premio:";
            // 
            // cmbReward
            // 
            this.cmbReward.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReward.FormattingEnabled = true;
            this.cmbReward.Location = new System.Drawing.Point(117, 174);
            this.cmbReward.Name = "cmbReward";
            this.cmbReward.Size = new System.Drawing.Size(319, 33);
            this.cmbReward.TabIndex = 0;
            this.cmbReward.SelectedIndexChanged += new System.EventHandler(this.cmbReward_SelectedIndexChanged);
            // 
            // cbRewardPaid
            // 
            this.cbRewardPaid.AutoSize = true;
            this.cbRewardPaid.Checked = true;
            this.cbRewardPaid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRewardPaid.Location = new System.Drawing.Point(117, 265);
            this.cbRewardPaid.Name = "cbRewardPaid";
            this.cbRewardPaid.Size = new System.Drawing.Size(215, 30);
            this.cbRewardPaid.TabIndex = 2;
            this.cbRewardPaid.Text = "Premio fue pagado";
            this.cbRewardPaid.UseVisualStyleBackColor = true;
            // 
            // lblPointsForCash
            // 
            this.lblPointsForCash.AutoSize = true;
            this.lblPointsForCash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblPointsForCash.Location = new System.Drawing.Point(226, 223);
            this.lblPointsForCash.Name = "lblPointsForCash";
            this.lblPointsForCash.Size = new System.Drawing.Size(54, 26);
            this.lblPointsForCash.TabIndex = 23;
            this.lblPointsForCash.Text = "0.00";
            this.lblPointsForCash.Visible = false;
            // 
            // frmClaimPoints
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 404);
            this.Controls.Add(this.lblPointsForCash);
            this.Controls.Add(this.cbRewardPaid);
            this.Controls.Add(this.cmbReward);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblBalance);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtDolarsUsed);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmClaimPoints";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reclamar Premio";
            this.Load += new System.EventHandler(this.frmClaimPoints_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDolarsUsed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbReward;
        private System.Windows.Forms.CheckBox cbRewardPaid;
        private System.Windows.Forms.Label lblPointsForCash;
    }
}