namespace MvAssistantMacVerifyEqp
{
    partial class FrmDrawerTest
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
            this.BtnIni = new System.Windows.Forms.Button();
            this.GrpBox_INI = new System.Windows.Forms.GroupBox();
            this.GrpBox_INI.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnIni
            // 
            this.BtnIni.Location = new System.Drawing.Point(6, 31);
            this.BtnIni.Name = "BtnIni";
            this.BtnIni.Size = new System.Drawing.Size(75, 23);
            this.BtnIni.TabIndex = 0;
            this.BtnIni.Text = "INI";
            this.BtnIni.UseVisualStyleBackColor = true;
            // 
            // GrpBox_INI
            // 
            this.GrpBox_INI.Controls.Add(this.BtnIni);
            this.GrpBox_INI.Location = new System.Drawing.Point(12, 12);
            this.GrpBox_INI.Name = "GrpBox_INI";
            this.GrpBox_INI.Size = new System.Drawing.Size(713, 100);
            this.GrpBox_INI.TabIndex = 1;
            this.GrpBox_INI.TabStop = false;
            this.GrpBox_INI.Text = "INI";
            // 
            // FrmDrawerTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 692);
            this.Controls.Add(this.GrpBox_INI);
            this.Name = "FrmDrawerTest";
            this.Text = "FrmDrawerTestcs";
            this.Load += new System.EventHandler(this.FrmDrawerTest_Load);
            this.GrpBox_INI.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnIni;
        private System.Windows.Forms.GroupBox GrpBox_INI;
    }
}