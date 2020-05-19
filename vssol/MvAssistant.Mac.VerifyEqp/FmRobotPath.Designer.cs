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
            this.LstBxPositionInfo = new System.Windows.Forms.ListBox();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnLoad = new System.Windows.Forms.Button();
            this.CmbBoxDeviceName = new System.Windows.Forms.ComboBox();
            this.TxtBxDevicePath = new System.Windows.Forms.TextBox();
            this.txtBxDeviceIP = new System.Windows.Forms.TextBox();
            this.btnGetPosition = new System.Windows.Forms.Button();
            this.LstBxGetPosition = new System.Windows.Forms.ListBox();
            this.BtnAddGet = new System.Windows.Forms.Button();
            this.BtnDeleteAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LstBxPositionInfo
            // 
            this.LstBxPositionInfo.FormattingEnabled = true;
            this.LstBxPositionInfo.HorizontalScrollbar = true;
            this.LstBxPositionInfo.ItemHeight = 15;
            this.LstBxPositionInfo.Location = new System.Drawing.Point(230, 115);
            this.LstBxPositionInfo.Margin = new System.Windows.Forms.Padding(4);
            this.LstBxPositionInfo.Name = "LstBxPositionInfo";
            this.LstBxPositionInfo.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.LstBxPositionInfo.Size = new System.Drawing.Size(1205, 334);
            this.LstBxPositionInfo.TabIndex = 0;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(180, 475);
            this.BtnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(159, 42);
            this.BtnAdd.TabIndex = 1;
            this.BtnAdd.Text = "Add";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Location = new System.Drawing.Point(514, 475);
            this.BtnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(159, 42);
            this.BtnDelete.TabIndex = 2;
            this.BtnDelete.Text = "Delete";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(347, 475);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(159, 42);
            this.BtnSave.TabIndex = 3;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnLoad
            // 
            this.BtnLoad.Location = new System.Drawing.Point(347, 525);
            this.BtnLoad.Margin = new System.Windows.Forms.Padding(4);
            this.BtnLoad.Name = "BtnLoad";
            this.BtnLoad.Size = new System.Drawing.Size(159, 42);
            this.BtnLoad.TabIndex = 4;
            this.BtnLoad.Text = "Load";
            this.BtnLoad.UseVisualStyleBackColor = true;
            this.BtnLoad.Click += new System.EventHandler(this.BtnLoad_Click);
            // 
            // CmbBoxDeviceName
            // 
            this.CmbBoxDeviceName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbBoxDeviceName.FormattingEnabled = true;
            this.CmbBoxDeviceName.Location = new System.Drawing.Point(13, 17);
            this.CmbBoxDeviceName.Margin = new System.Windows.Forms.Padding(4);
            this.CmbBoxDeviceName.Name = "CmbBoxDeviceName";
            this.CmbBoxDeviceName.Size = new System.Drawing.Size(1422, 23);
            this.CmbBoxDeviceName.TabIndex = 5;
            this.CmbBoxDeviceName.SelectedIndexChanged += new System.EventHandler(this.CmbBoxDeviceName_SelectedIndexChanged);
            // 
            // TxtBxDevicePath
            // 
            this.TxtBxDevicePath.Location = new System.Drawing.Point(16, 82);
            this.TxtBxDevicePath.Margin = new System.Windows.Forms.Padding(4);
            this.TxtBxDevicePath.Name = "TxtBxDevicePath";
            this.TxtBxDevicePath.ReadOnly = true;
            this.TxtBxDevicePath.Size = new System.Drawing.Size(1419, 25);
            this.TxtBxDevicePath.TabIndex = 6;
            // 
            // txtBxDeviceIP
            // 
            this.txtBxDeviceIP.Location = new System.Drawing.Point(16, 48);
            this.txtBxDeviceIP.Margin = new System.Windows.Forms.Padding(4);
            this.txtBxDeviceIP.Name = "txtBxDeviceIP";
            this.txtBxDeviceIP.ReadOnly = true;
            this.txtBxDeviceIP.Size = new System.Drawing.Size(1419, 25);
            this.txtBxDeviceIP.TabIndex = 7;
            // 
            // btnGetPosition
            // 
            this.btnGetPosition.Location = new System.Drawing.Point(13, 475);
            this.btnGetPosition.Margin = new System.Windows.Forms.Padding(4);
            this.btnGetPosition.Name = "btnGetPosition";
            this.btnGetPosition.Size = new System.Drawing.Size(159, 42);
            this.btnGetPosition.TabIndex = 9;
            this.btnGetPosition.Text = "Get";
            this.btnGetPosition.UseVisualStyleBackColor = true;
            this.btnGetPosition.Click += new System.EventHandler(this.btnGetPosition_Click);
            // 
            // LstBxGetPosition
            // 
            this.LstBxGetPosition.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.LstBxGetPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LstBxGetPosition.FormattingEnabled = true;
            this.LstBxGetPosition.ItemHeight = 15;
            this.LstBxGetPosition.Location = new System.Drawing.Point(21, 114);
            this.LstBxGetPosition.Name = "LstBxGetPosition";
            this.LstBxGetPosition.Size = new System.Drawing.Size(94, 332);
            this.LstBxGetPosition.TabIndex = 12;
            // 
            // BtnAddGet
            // 
            this.BtnAddGet.Location = new System.Drawing.Point(122, 252);
            this.BtnAddGet.Margin = new System.Windows.Forms.Padding(4);
            this.BtnAddGet.Name = "BtnAddGet";
            this.BtnAddGet.Size = new System.Drawing.Size(90, 42);
            this.BtnAddGet.TabIndex = 13;
            this.BtnAddGet.Text = "=>";
            this.BtnAddGet.UseVisualStyleBackColor = true;
            this.BtnAddGet.Click += new System.EventHandler(this.BtnAddGet_Click);
            // 
            // BtnDeleteAll
            // 
            this.BtnDeleteAll.Location = new System.Drawing.Point(514, 525);
            this.BtnDeleteAll.Margin = new System.Windows.Forms.Padding(4);
            this.BtnDeleteAll.Name = "BtnDeleteAll";
            this.BtnDeleteAll.Size = new System.Drawing.Size(159, 42);
            this.BtnDeleteAll.TabIndex = 14;
            this.BtnDeleteAll.Text = "Delete All";
            this.BtnDeleteAll.UseVisualStyleBackColor = true;
            this.BtnDeleteAll.Click += new System.EventHandler(this.BtnDeleteAll_Click);
            // 
            // FmRobotPath
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1541, 745);
            this.Controls.Add(this.BtnDeleteAll);
            this.Controls.Add(this.BtnAddGet);
            this.Controls.Add(this.LstBxGetPosition);
            this.Controls.Add(this.btnGetPosition);
            this.Controls.Add(this.txtBxDeviceIP);
            this.Controls.Add(this.TxtBxDevicePath);
            this.Controls.Add(this.CmbBoxDeviceName);
            this.Controls.Add(this.BtnLoad);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.BtnDelete);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.LstBxPositionInfo);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FmRobotPath";
            this.Text = "FmRobotPath";
            this.Load += new System.EventHandler(this.FmRobotPath_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox LstBxPositionInfo;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnLoad;
        private System.Windows.Forms.ComboBox CmbBoxDeviceName;
        private System.Windows.Forms.TextBox TxtBxDevicePath;
        private System.Windows.Forms.TextBox txtBxDeviceIP;
        private System.Windows.Forms.Button btnGetPosition;
        private System.Windows.Forms.ListBox LstBxGetPosition;
        private System.Windows.Forms.Button BtnAddGet;
        private System.Windows.Forms.Button BtnDeleteAll;
    }
}