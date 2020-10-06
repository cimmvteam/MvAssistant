namespace MvAssistantMacVerifyEqp
{
    partial class FmMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBoxRobot = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSgsTest = new System.Windows.Forms.ToolStripMenuItem();
            this.robotPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsmiVerifyEQP = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1416, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiBoxRobot,
            this.tsmiSgsTest,
            this.robotPathToolStripMenuItem,
            this.tsmiVerifyEQP});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(58, 24);
            this.fileToolStripMenuItem.Text = "&Form";
            // 
            // tsmiBoxRobot
            // 
            this.tsmiBoxRobot.Name = "tsmiBoxRobot";
            this.tsmiBoxRobot.Size = new System.Drawing.Size(216, 26);
            this.tsmiBoxRobot.Text = "&Box Robot";
            this.tsmiBoxRobot.Click += new System.EventHandler(this.tsmiBoxRobot_Click);
            // 
            // tsmiMaskRobot
            // 
            this.tsmiSgsTest.Name = "tsmiMaskRobot";
            this.tsmiSgsTest.Size = new System.Drawing.Size(216, 26);
            this.tsmiSgsTest.Text = "SGS Test";
            this.tsmiSgsTest.Click += new System.EventHandler(this.tsmiSgsTest_Click);
            // 
            // robotPathToolStripMenuItem
            // 
            this.robotPathToolStripMenuItem.Name = "robotPathToolStripMenuItem";
            this.robotPathToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.robotPathToolStripMenuItem.Text = "Robot Path";
            this.robotPathToolStripMenuItem.Click += new System.EventHandler(this.robotPathToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(63, 24);
            this.aboutToolStripMenuItem.Text = "&About";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1416, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsmiVerifyEQP
            // 
            this.tsmiVerifyEQP.Name = "tsmiVerifyEQP";
            this.tsmiVerifyEQP.Size = new System.Drawing.Size(216, 26);
            this.tsmiVerifyEQP.Text = "Verify EQP";
            this.tsmiVerifyEQP.Click += new System.EventHandler(this.tsmiVerifyEQP_Click);
            // 
            // FmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1416, 690);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FmMain";
            this.Text = "FmMain";
            this.Load += new System.EventHandler(this.FmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiBoxRobot;
        private System.Windows.Forms.ToolStripMenuItem tsmiSgsTest;
        private System.Windows.Forms.ToolStripMenuItem robotPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiVerifyEQP;
    }
}