namespace MaskCleanerVerify
{
    partial class FmRobotPath
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
            this.LstBxPositionHeaders = new System.Windows.Forms.ListBox();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.CmbBoxDeviceName = new System.Windows.Forms.ComboBox();
            this.TxtBxDevicePath = new System.Windows.Forms.TextBox();
            this.txtBxDeviceIP = new System.Windows.Forms.TextBox();
            this.btnGetPosition = new System.Windows.Forms.Button();
            this.LstBxPositionDetail = new System.Windows.Forms.ListBox();
            this.LstBxGetPosition = new System.Windows.Forms.ListBox();
            this.BtnAddGet = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LstBxPositionHeaders
            // 
            this.LstBxPositionHeaders.FormattingEnabled = true;
            this.LstBxPositionHeaders.ItemHeight = 15;
            this.LstBxPositionHeaders.Location = new System.Drawing.Point(16, 118);
            this.LstBxPositionHeaders.Margin = new System.Windows.Forms.Padding(4);
            this.LstBxPositionHeaders.Name = "LstBxPositionHeaders";
            this.LstBxPositionHeaders.Size = new System.Drawing.Size(407, 334);
            this.LstBxPositionHeaders.TabIndex = 0;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(400, 490);
            this.BtnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(159, 100);
            this.BtnAdd.TabIndex = 1;
            this.BtnAdd.Text = "Add";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(901, 490);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(159, 100);
            this.button2.TabIndex = 2;
            this.button2.Text = "Delete";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(567, 490);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(159, 100);
            this.button3.TabIndex = 3;
            this.button3.Text = "Save Xml";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(734, 490);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(159, 100);
            this.button4.TabIndex = 4;
            this.button4.Text = "Load Xml";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // CmbBoxDeviceName
            // 
            this.CmbBoxDeviceName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbBoxDeviceName.FormattingEnabled = true;
            this.CmbBoxDeviceName.Location = new System.Drawing.Point(16, 15);
            this.CmbBoxDeviceName.Margin = new System.Windows.Forms.Padding(4);
            this.CmbBoxDeviceName.Name = "CmbBoxDeviceName";
            this.CmbBoxDeviceName.Size = new System.Drawing.Size(407, 23);
            this.CmbBoxDeviceName.TabIndex = 5;
            this.CmbBoxDeviceName.SelectedIndexChanged += new System.EventHandler(this.CmbBoxDeviceName_SelectedIndexChanged);
            // 
            // TxtBxDevicePath
            // 
            this.TxtBxDevicePath.Location = new System.Drawing.Point(16, 82);
            this.TxtBxDevicePath.Margin = new System.Windows.Forms.Padding(4);
            this.TxtBxDevicePath.Name = "TxtBxDevicePath";
            this.TxtBxDevicePath.ReadOnly = true;
            this.TxtBxDevicePath.Size = new System.Drawing.Size(407, 25);
            this.TxtBxDevicePath.TabIndex = 6;
            // 
            // txtBxDeviceIP
            // 
            this.txtBxDeviceIP.Location = new System.Drawing.Point(16, 48);
            this.txtBxDeviceIP.Margin = new System.Windows.Forms.Padding(4);
            this.txtBxDeviceIP.Name = "txtBxDeviceIP";
            this.txtBxDeviceIP.ReadOnly = true;
            this.txtBxDeviceIP.Size = new System.Drawing.Size(407, 25);
            this.txtBxDeviceIP.TabIndex = 7;
            // 
            // btnGetPosition
            // 
            this.btnGetPosition.Location = new System.Drawing.Point(13, 490);
            this.btnGetPosition.Margin = new System.Windows.Forms.Padding(4);
            this.btnGetPosition.Name = "btnGetPosition";
            this.btnGetPosition.Size = new System.Drawing.Size(159, 100);
            this.btnGetPosition.TabIndex = 9;
            this.btnGetPosition.Text = "Get";
            this.btnGetPosition.UseVisualStyleBackColor = true;
            this.btnGetPosition.Click += new System.EventHandler(this.btnGetPosition_Click);
            // 
            // LstBxPositionDetail
            // 
            this.LstBxPositionDetail.FormattingEnabled = true;
            this.LstBxPositionDetail.ItemHeight = 15;
            this.LstBxPositionDetail.Location = new System.Drawing.Point(430, 118);
            this.LstBxPositionDetail.Name = "LstBxPositionDetail";
            this.LstBxPositionDetail.Size = new System.Drawing.Size(136, 334);
            this.LstBxPositionDetail.TabIndex = 11;
            // 
            // LstBxGetPosition
            // 
            this.LstBxGetPosition.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.LstBxGetPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LstBxGetPosition.FormattingEnabled = true;
            this.LstBxGetPosition.ItemHeight = 15;
            this.LstBxGetPosition.Location = new System.Drawing.Point(572, 118);
            this.LstBxGetPosition.Name = "LstBxGetPosition";
            this.LstBxGetPosition.Size = new System.Drawing.Size(120, 332);
            this.LstBxGetPosition.TabIndex = 12;
            // 
            // BtnAddGet
            // 
            this.BtnAddGet.Location = new System.Drawing.Point(180, 490);
            this.BtnAddGet.Margin = new System.Windows.Forms.Padding(4);
            this.BtnAddGet.Name = "BtnAddGet";
            this.BtnAddGet.Size = new System.Drawing.Size(159, 100);
            this.BtnAddGet.TabIndex = 13;
            this.BtnAddGet.Text = "AddGet";
            this.BtnAddGet.UseVisualStyleBackColor = true;
            // 
            // FmRobotPath
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 745);
            this.Controls.Add(this.BtnAddGet);
            this.Controls.Add(this.LstBxGetPosition);
            this.Controls.Add(this.LstBxPositionDetail);
            this.Controls.Add(this.btnGetPosition);
            this.Controls.Add(this.txtBxDeviceIP);
            this.Controls.Add(this.TxtBxDevicePath);
            this.Controls.Add(this.CmbBoxDeviceName);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.LstBxPositionHeaders);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FmRobotPath";
            this.Text = "FmRobotPath";
            this.Load += new System.EventHandler(this.FmRobotPath_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox LstBxPositionHeaders;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ComboBox CmbBoxDeviceName;
        private System.Windows.Forms.TextBox TxtBxDevicePath;
        private System.Windows.Forms.TextBox txtBxDeviceIP;
        private System.Windows.Forms.Button btnGetPosition;
        private System.Windows.Forms.ListBox LstBxPositionDetail;
        private System.Windows.Forms.ListBox LstBxGetPosition;
        private System.Windows.Forms.Button BtnAddGet;
    }
}