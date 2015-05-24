namespace Points_Client
{
    partial class frmImportCustomers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImportCustomers));
            this.lblImportText = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblSkip = new System.Windows.Forms.LinkLabel();
            this.lblNew = new System.Windows.Forms.Label();
            this.lblExisting = new System.Windows.Forms.Label();
            this.lblRejected = new System.Windows.Forms.Label();
            this.cmdImportNow = new System.Windows.Forms.Button();
            this.cmdLater = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblImportText
            // 
            this.lblImportText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblImportText.ForeColor = System.Drawing.Color.White;
            this.lblImportText.Location = new System.Drawing.Point(12, 36);
            this.lblImportText.Name = "lblImportText";
            this.lblImportText.Size = new System.Drawing.Size(450, 28);
            this.lblImportText.TabIndex = 0;
            this.lblImportText.Text = "Importando clientes. Favor de esperar...";
            this.lblImportText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(148, 94);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(130, 69);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // lblSkip
            // 
            this.lblSkip.AutoSize = true;
            this.lblSkip.LinkColor = System.Drawing.Color.White;
            this.lblSkip.Location = new System.Drawing.Point(12, 205);
            this.lblSkip.Name = "lblSkip";
            this.lblSkip.Size = new System.Drawing.Size(46, 24);
            this.lblSkip.TabIndex = 2;
            this.lblSkip.TabStop = true;
            this.lblSkip.Text = "Skip";
            this.lblSkip.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblSkip_LinkClicked);
            // 
            // lblNew
            // 
            this.lblNew.AutoSize = true;
            this.lblNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNew.ForeColor = System.Drawing.Color.White;
            this.lblNew.Location = new System.Drawing.Point(316, 94);
            this.lblNew.Name = "lblNew";
            this.lblNew.Size = new System.Drawing.Size(96, 17);
            this.lblNew.TabIndex = 3;
            this.lblNew.Text = "Nuevos: 0000";
            this.lblNew.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblExisting
            // 
            this.lblExisting.AutoSize = true;
            this.lblExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExisting.ForeColor = System.Drawing.Color.White;
            this.lblExisting.Location = new System.Drawing.Point(316, 120);
            this.lblExisting.Name = "lblExisting";
            this.lblExisting.Size = new System.Drawing.Size(112, 17);
            this.lblExisting.TabIndex = 4;
            this.lblExisting.Text = "Existentes: 0000";
            this.lblExisting.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRejected
            // 
            this.lblRejected.AutoSize = true;
            this.lblRejected.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRejected.ForeColor = System.Drawing.Color.White;
            this.lblRejected.Location = new System.Drawing.Point(316, 146);
            this.lblRejected.Name = "lblRejected";
            this.lblRejected.Size = new System.Drawing.Size(127, 17);
            this.lblRejected.TabIndex = 5;
            this.lblRejected.Text = "Rechazados: 0000";
            this.lblRejected.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmdImportNow
            // 
            this.cmdImportNow.BackColor = System.Drawing.Color.White;
            this.cmdImportNow.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.cmdImportNow.FlatAppearance.BorderSize = 2;
            this.cmdImportNow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdImportNow.Location = new System.Drawing.Point(148, 193);
            this.cmdImportNow.Name = "cmdImportNow";
            this.cmdImportNow.Size = new System.Drawing.Size(178, 36);
            this.cmdImportNow.TabIndex = 6;
            this.cmdImportNow.Text = "Importar Ahora";
            this.cmdImportNow.UseVisualStyleBackColor = false;
            this.cmdImportNow.Click += new System.EventHandler(this.cmdImportNow_Click);
            // 
            // cmdLater
            // 
            this.cmdLater.BackColor = System.Drawing.Color.White;
            this.cmdLater.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.cmdLater.FlatAppearance.BorderSize = 2;
            this.cmdLater.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdLater.Location = new System.Drawing.Point(331, 193);
            this.cmdLater.Name = "cmdLater";
            this.cmdLater.Size = new System.Drawing.Size(117, 36);
            this.cmdLater.TabIndex = 7;
            this.cmdLater.Text = "Despues";
            this.cmdLater.UseVisualStyleBackColor = false;
            this.cmdLater.Click += new System.EventHandler(this.cmdLater_Click);
            // 
            // frmImportCustomers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(474, 251);
            this.Controls.Add(this.cmdLater);
            this.Controls.Add(this.cmdImportNow);
            this.Controls.Add(this.lblRejected);
            this.Controls.Add(this.lblExisting);
            this.Controls.Add(this.lblNew);
            this.Controls.Add(this.lblSkip);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblImportText);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmImportCustomers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Importar Clientes";
            this.Activated += new System.EventHandler(this.frmImportCustomers_Activated);
            this.Load += new System.EventHandler(this.frmImportCustomers_Load);
            this.Enter += new System.EventHandler(this.frmImportCustomers_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblImportText;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel lblSkip;
        private System.Windows.Forms.Label lblNew;
        private System.Windows.Forms.Label lblExisting;
        private System.Windows.Forms.Label lblRejected;
        private System.Windows.Forms.Button cmdImportNow;
        private System.Windows.Forms.Button cmdLater;

    }
}