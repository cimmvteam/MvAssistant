namespace MvAssistantMacVerifyEqp
{
    partial class FrmTestUI
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtBxIpA = new System.Windows.Forms.TabPage();
            this.GrpDrawer = new System.Windows.Forms.GroupBox();
            this.GrpDrawerD = new System.Windows.Forms.GroupBox();
            this.GrpDrawerC = new System.Windows.Forms.GroupBox();
            this.GrpDrawerB = new System.Windows.Forms.GroupBox();
            this.GrpDrawerA = new System.Windows.Forms.GroupBox();
            this.tabControl1.SuspendLayout();
            this.txtBxIpA.SuspendLayout();
            this.GrpDrawer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.txtBxIpA);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1567, 739);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1559, 713);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtBxIpA
            // 
            this.txtBxIpA.Controls.Add(this.GrpDrawer);
            this.txtBxIpA.Location = new System.Drawing.Point(4, 22);
            this.txtBxIpA.Name = "txtBxIpA";
            this.txtBxIpA.Padding = new System.Windows.Forms.Padding(3);
            this.txtBxIpA.Size = new System.Drawing.Size(1559, 713);
            this.txtBxIpA.TabIndex = 0;
            this.txtBxIpA.Text = "Drawers";
            this.txtBxIpA.UseVisualStyleBackColor = true;
            // 
            // GrpDrawer
            // 
            this.GrpDrawer.Controls.Add(this.GrpDrawerD);
            this.GrpDrawer.Controls.Add(this.GrpDrawerC);
            this.GrpDrawer.Controls.Add(this.GrpDrawerB);
            this.GrpDrawer.Controls.Add(this.GrpDrawerA);
            this.GrpDrawer.Location = new System.Drawing.Point(6, 6);
            this.GrpDrawer.Name = "GrpDrawer";
            this.GrpDrawer.Size = new System.Drawing.Size(1468, 665);
            this.GrpDrawer.TabIndex = 1;
            this.GrpDrawer.TabStop = false;
            this.GrpDrawer.Text = "Drawers";
            // 
            // GrpDrawerD
            // 
            this.GrpDrawerD.Location = new System.Drawing.Point(1210, 375);
            this.GrpDrawerD.Name = "GrpDrawerD";
            this.GrpDrawerD.Size = new System.Drawing.Size(233, 265);
            this.GrpDrawerD.TabIndex = 3;
            this.GrpDrawerD.TabStop = false;
            this.GrpDrawerD.Text = "D(192.168.0.54)";
            // 
            // GrpDrawerC
            // 
            this.GrpDrawerC.Location = new System.Drawing.Point(1124, 375);
            this.GrpDrawerC.Name = "GrpDrawerC";
            this.GrpDrawerC.Size = new System.Drawing.Size(69, 265);
            this.GrpDrawerC.TabIndex = 2;
            this.GrpDrawerC.TabStop = false;
            this.GrpDrawerC.Text = "C(192.168.0.50)";
            // 
            // GrpDrawerB
            // 
            this.GrpDrawerB.Location = new System.Drawing.Point(1210, 21);
            this.GrpDrawerB.Name = "GrpDrawerB";
            this.GrpDrawerB.Size = new System.Drawing.Size(233, 265);
            this.GrpDrawerB.TabIndex = 1;
            this.GrpDrawerB.TabStop = false;
            this.GrpDrawerB.Text = "B(192.168.0.42)";
            // 
            // GrpDrawerA
            // 
            this.GrpDrawerA.Location = new System.Drawing.Point(6, 21);
            this.GrpDrawerA.Name = "GrpDrawerA";
            this.GrpDrawerA.Size = new System.Drawing.Size(854, 565);
            this.GrpDrawerA.TabIndex = 0;
            this.GrpDrawerA.TabStop = false;
            this.GrpDrawerA.Text = "A(192.168.0.34)";
            // 
            // FrmTestUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1621, 763);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmTestUI";
            this.Text = "FrmTestUI";
            this.Load += new System.EventHandler(this.FrmTestUI_Load);
            this.tabControl1.ResumeLayout(false);
            this.txtBxIpA.ResumeLayout(false);
            this.GrpDrawer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage txtBxIpA;
        private System.Windows.Forms.GroupBox GrpDrawer;
        private System.Windows.Forms.GroupBox GrpDrawerD;
        private System.Windows.Forms.GroupBox GrpDrawerC;
        private System.Windows.Forms.GroupBox GrpDrawerB;
        private System.Windows.Forms.GroupBox GrpDrawerA;
        private System.Windows.Forms.TabPage tabPage2;
    }
}