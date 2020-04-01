namespace BoxTransferTest
{
    partial class FmSgsTest
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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.ucMaskRobot1 = new BoxTransferTest.ViewUc.UcMaskRobot();
            this.ucOpenStage1 = new BoxTransferTest.ViewUc.UcOpenStage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(854, 525);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ucMaskRobot1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(846, 499);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Mask Robot";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(846, 499);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Box Robot";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(846, 499);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Load Port";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.ucOpenStage1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(846, 499);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Open Stage";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(846, 499);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Drawer";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // ucMaskRobot1
            // 
            this.ucMaskRobot1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMaskRobot1.Location = new System.Drawing.Point(3, 3);
            this.ucMaskRobot1.Name = "ucMaskRobot1";
            this.ucMaskRobot1.Size = new System.Drawing.Size(840, 493);
            this.ucMaskRobot1.TabIndex = 0;
            // 
            // ucOpenStage1
            // 
            this.ucOpenStage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucOpenStage1.Location = new System.Drawing.Point(0, 0);
            this.ucOpenStage1.Name = "ucOpenStage1";
            this.ucOpenStage1.Size = new System.Drawing.Size(846, 499);
            this.ucOpenStage1.TabIndex = 0;
            // 
            // FmSgsTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 525);
            this.Controls.Add(this.tabControl1);
            this.Name = "FmSgsTest";
            this.Text = "FmMaskRobot";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private ViewUc.UcMaskRobot ucMaskRobot1;
        private ViewUc.UcOpenStage ucOpenStage1;
    }
}