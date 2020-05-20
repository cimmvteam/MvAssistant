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
            this.CmbBoxMotionType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.NumUdpSn = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.NumUdpSpeed = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumUdpSn)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumUdpSpeed)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LstBxPositionInfo
            // 
            this.LstBxPositionInfo.FormattingEnabled = true;
            this.LstBxPositionInfo.HorizontalScrollbar = true;
            this.LstBxPositionInfo.ItemHeight = 15;
            this.LstBxPositionInfo.Location = new System.Drawing.Point(16, 25);
            this.LstBxPositionInfo.Margin = new System.Windows.Forms.Padding(4);
            this.LstBxPositionInfo.Name = "LstBxPositionInfo";
            this.LstBxPositionInfo.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.LstBxPositionInfo.Size = new System.Drawing.Size(1588, 364);
            this.LstBxPositionInfo.TabIndex = 0;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(4, 63);
            this.BtnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(131, 42);
            this.BtnAdd.TabIndex = 1;
            this.BtnAdd.Text = "Add";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Location = new System.Drawing.Point(4, 113);
            this.BtnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(131, 42);
            this.BtnDelete.TabIndex = 2;
            this.BtnDelete.Text = "Delete";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(4, 213);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(131, 42);
            this.BtnSave.TabIndex = 3;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnLoad
            // 
            this.BtnLoad.Location = new System.Drawing.Point(4, 13);
            this.BtnLoad.Margin = new System.Windows.Forms.Padding(4);
            this.BtnLoad.Name = "BtnLoad";
            this.BtnLoad.Size = new System.Drawing.Size(131, 42);
            this.BtnLoad.TabIndex = 4;
            this.BtnLoad.Text = "Load";
            this.BtnLoad.UseVisualStyleBackColor = true;
            this.BtnLoad.Click += new System.EventHandler(this.BtnLoad_Click);
            // 
            // CmbBoxDeviceName
            // 
            this.CmbBoxDeviceName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbBoxDeviceName.FormattingEnabled = true;
            this.CmbBoxDeviceName.Location = new System.Drawing.Point(69, 25);
            this.CmbBoxDeviceName.Margin = new System.Windows.Forms.Padding(4);
            this.CmbBoxDeviceName.Name = "CmbBoxDeviceName";
            this.CmbBoxDeviceName.Size = new System.Drawing.Size(1050, 23);
            this.CmbBoxDeviceName.TabIndex = 5;
            this.CmbBoxDeviceName.SelectedIndexChanged += new System.EventHandler(this.CmbBoxDeviceName_SelectedIndexChanged);
            // 
            // TxtBxDevicePath
            // 
            this.TxtBxDevicePath.Location = new System.Drawing.Point(69, 90);
            this.TxtBxDevicePath.Margin = new System.Windows.Forms.Padding(4);
            this.TxtBxDevicePath.Name = "TxtBxDevicePath";
            this.TxtBxDevicePath.ReadOnly = true;
            this.TxtBxDevicePath.Size = new System.Drawing.Size(1050, 25);
            this.TxtBxDevicePath.TabIndex = 6;
            // 
            // txtBxDeviceIP
            // 
            this.txtBxDeviceIP.Location = new System.Drawing.Point(69, 57);
            this.txtBxDeviceIP.Margin = new System.Windows.Forms.Padding(4);
            this.txtBxDeviceIP.Name = "txtBxDeviceIP";
            this.txtBxDeviceIP.ReadOnly = true;
            this.txtBxDeviceIP.Size = new System.Drawing.Size(1050, 25);
            this.txtBxDeviceIP.TabIndex = 7;
            // 
            // btnGetPosition
            // 
            this.btnGetPosition.Location = new System.Drawing.Point(1619, 57);
            this.btnGetPosition.Margin = new System.Windows.Forms.Padding(4);
            this.btnGetPosition.Name = "btnGetPosition";
            this.btnGetPosition.Size = new System.Drawing.Size(131, 42);
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
            this.LstBxGetPosition.HorizontalScrollbar = true;
            this.LstBxGetPosition.ItemHeight = 15;
            this.LstBxGetPosition.Location = new System.Drawing.Point(17, 54);
            this.LstBxGetPosition.Name = "LstBxGetPosition";
            this.LstBxGetPosition.Size = new System.Drawing.Size(1588, 47);
            this.LstBxGetPosition.TabIndex = 12;
            // 
            // BtnAddGet
            // 
            this.BtnAddGet.Location = new System.Drawing.Point(783, 307);
            this.BtnAddGet.Margin = new System.Windows.Forms.Padding(4);
            this.BtnAddGet.Name = "BtnAddGet";
            this.BtnAddGet.Size = new System.Drawing.Size(65, 67);
            this.BtnAddGet.TabIndex = 13;
            this.BtnAddGet.Text = "↓";
            this.BtnAddGet.UseVisualStyleBackColor = true;
            this.BtnAddGet.Click += new System.EventHandler(this.BtnAddGet_Click);
            // 
            // BtnDeleteAll
            // 
            this.BtnDeleteAll.Location = new System.Drawing.Point(4, 163);
            this.BtnDeleteAll.Margin = new System.Windows.Forms.Padding(4);
            this.BtnDeleteAll.Name = "BtnDeleteAll";
            this.BtnDeleteAll.Size = new System.Drawing.Size(131, 42);
            this.BtnDeleteAll.TabIndex = 14;
            this.BtnDeleteAll.Text = "Delete All";
            this.BtnDeleteAll.UseVisualStyleBackColor = true;
            this.BtnDeleteAll.Click += new System.EventHandler(this.BtnDeleteAll_Click);
            // 
            // CmbBoxMotionType
            // 
            this.CmbBoxMotionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbBoxMotionType.FormattingEnabled = true;
            this.CmbBoxMotionType.Location = new System.Drawing.Point(161, 21);
            this.CmbBoxMotionType.Name = "CmbBoxMotionType";
            this.CmbBoxMotionType.Size = new System.Drawing.Size(121, 23);
            this.CmbBoxMotionType.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 23);
            this.label1.TabIndex = 16;
            this.label1.Text = "Device: ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(11, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 23);
            this.label2.TabIndex = 17;
            this.label2.Text = "IP: ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(11, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 23);
            this.label3.TabIndex = 18;
            this.label3.Text = "File:  ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(40, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 19;
            this.label4.Text = "Motion Type: ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(18, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 23);
            this.label5.TabIndex = 20;
            this.label5.Text = "Speed(mm/second): ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CmbBoxDeviceName);
            this.groupBox1.Controls.Add(this.TxtBxDevicePath);
            this.groupBox1.Controls.Add(this.txtBxDeviceIP);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1138, 132);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.NumUdpSpeed);
            this.groupBox2.Controls.Add(this.CmbBoxMotionType);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(1200, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(307, 133);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(1609, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 23);
            this.label6.TabIndex = 22;
            this.label6.Text = "Serial No:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NumUdpSn
            // 
            this.NumUdpSn.Location = new System.Drawing.Point(1611, 57);
            this.NumUdpSn.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.NumUdpSn.Name = "NumUdpSn";
            this.NumUdpSn.Size = new System.Drawing.Size(120, 25);
            this.NumUdpSn.TabIndex = 23;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.LstBxGetPosition);
            this.groupBox3.Controls.Add(this.btnGetPosition);
            this.groupBox3.Location = new System.Drawing.Point(12, 169);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1762, 120);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.panel1);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.NumUdpSn);
            this.groupBox5.Controls.Add(this.LstBxPositionInfo);
            this.groupBox5.Location = new System.Drawing.Point(13, 400);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(1761, 391);
            this.groupBox5.TabIndex = 26;
            this.groupBox5.TabStop = false;
            // 
            // NumUdpSpeed
            // 
            this.NumUdpSpeed.Location = new System.Drawing.Point(162, 54);
            this.NumUdpSpeed.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.NumUdpSpeed.Name = "NumUdpSpeed";
            this.NumUdpSpeed.Size = new System.Drawing.Size(120, 25);
            this.NumUdpSpeed.TabIndex = 24;
            this.NumUdpSpeed.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnLoad);
            this.panel1.Controls.Add(this.BtnSave);
            this.panel1.Controls.Add(this.BtnDeleteAll);
            this.panel1.Controls.Add(this.BtnAdd);
            this.panel1.Controls.Add(this.BtnDelete);
            this.panel1.Location = new System.Drawing.Point(1608, 124);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(146, 261);
            this.panel1.TabIndex = 15;
            // 
            // FmRobotPath
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1845, 958);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BtnAddGet);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FmRobotPath";
            this.Text = "FmRobotPath";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FmRobotPath_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NumUdpSn)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NumUdpSpeed)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private System.Windows.Forms.ComboBox CmbBoxMotionType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown NumUdpSn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.NumericUpDown NumUdpSpeed;
        private System.Windows.Forms.Panel panel1;
    }
}