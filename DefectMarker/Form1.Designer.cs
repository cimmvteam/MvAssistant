namespace DefectMarker
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.loadImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSelectRawToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadImageToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.preImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.summaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pbMain = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadImageToolStripMenuItem,
            this.preImageToolStripMenuItem,
            this.nextImageToolStripMenuItem,
            this.summaryToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1364, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // loadImageToolStripMenuItem
            // 
            this.loadImageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadSelectRawToolStripMenuItem,
            this.loadImageToolStripMenuItem1});
            this.loadImageToolStripMenuItem.Name = "loadImageToolStripMenuItem";
            this.loadImageToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.loadImageToolStripMenuItem.Text = "Load";
            // 
            // loadSelectRawToolStripMenuItem
            // 
            this.loadSelectRawToolStripMenuItem.Name = "loadSelectRawToolStripMenuItem";
            this.loadSelectRawToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.loadSelectRawToolStripMenuItem.Text = "Load Image";
            this.loadSelectRawToolStripMenuItem.Click += new System.EventHandler(this.loadSelectRawToolStripMenuItem_Click);
            // 
            // loadImageToolStripMenuItem1
            // 
            this.loadImageToolStripMenuItem1.Name = "loadImageToolStripMenuItem1";
            this.loadImageToolStripMenuItem1.Size = new System.Drawing.Size(175, 22);
            this.loadImageToolStripMenuItem1.Text = "Load Marking File";
            // 
            // preImageToolStripMenuItem
            // 
            this.preImageToolStripMenuItem.Name = "preImageToolStripMenuItem";
            this.preImageToolStripMenuItem.Size = new System.Drawing.Size(105, 20);
            this.preImageToolStripMenuItem.Text = "Previous Image";
            this.preImageToolStripMenuItem.Click += new System.EventHandler(this.preImageToolStripMenuItem_Click);
            // 
            // nextImageToolStripMenuItem
            // 
            this.nextImageToolStripMenuItem.Name = "nextImageToolStripMenuItem";
            this.nextImageToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.nextImageToolStripMenuItem.Text = "Next Image";
            this.nextImageToolStripMenuItem.Click += new System.EventHandler(this.nextImageToolStripMenuItem_Click);
            // 
            // summaryToolStripMenuItem
            // 
            this.summaryToolStripMenuItem.Name = "summaryToolStripMenuItem";
            this.summaryToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.summaryToolStripMenuItem.Text = "Summary";
            // 
            // pbMain
            // 
            this.pbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbMain.Location = new System.Drawing.Point(0, 24);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(1364, 775);
            this.pbMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbMain.TabIndex = 2;
            this.pbMain.TabStop = false;
            this.pbMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pbMain_Paint);
            this.pbMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbMain_MouseDown);
            this.pbMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbMain_MouseMove);
            this.pbMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbMain_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1364, 799);
            this.Controls.Add(this.pbMain);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Defect Marker";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem loadImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nextImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem summaryToolStripMenuItem;
        private System.Windows.Forms.PictureBox pbMain;
        private System.Windows.Forms.ToolStripMenuItem loadSelectRawToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadImageToolStripMenuItem1;
    }
}

