namespace MvAssistantMacVerifyEqp.ViewUc
{
    partial class UcDrawer
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.DR_ConnectBtn = new System.Windows.Forms.Button();
            this.DR_IPAddressBox = new System.Windows.Forms.TextBox();
            this.DR_SendPortBox = new System.Windows.Forms.TextBox();
            this.DR_ListenPortBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DR_Log = new System.Windows.Forms.RichTextBox();
            this.DR_SendedMsg = new System.Windows.Forms.ComboBox();
            this.DR_MsgSendBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DR_ConnectBtn
            // 
            this.DR_ConnectBtn.Location = new System.Drawing.Point(34, 24);
            this.DR_ConnectBtn.Name = "DR_ConnectBtn";
            this.DR_ConnectBtn.Size = new System.Drawing.Size(92, 70);
            this.DR_ConnectBtn.TabIndex = 0;
            this.DR_ConnectBtn.Text = "Connect";
            this.DR_ConnectBtn.UseVisualStyleBackColor = true;
            this.DR_ConnectBtn.Click += new System.EventHandler(this.DR_ConnectBtn_Click);
            // 
            // DR_IPAddressBox
            // 
            this.DR_IPAddressBox.Location = new System.Drawing.Point(217, 35);
            this.DR_IPAddressBox.Name = "DR_IPAddressBox";
            this.DR_IPAddressBox.Size = new System.Drawing.Size(175, 22);
            this.DR_IPAddressBox.TabIndex = 1;
            // 
            // DR_SendPortBox
            // 
            this.DR_SendPortBox.Location = new System.Drawing.Point(217, 72);
            this.DR_SendPortBox.Name = "DR_SendPortBox";
            this.DR_SendPortBox.Size = new System.Drawing.Size(67, 22);
            this.DR_SendPortBox.TabIndex = 2;
            // 
            // DR_ListenPortBox
            // 
            this.DR_ListenPortBox.Location = new System.Drawing.Point(358, 72);
            this.DR_ListenPortBox.Name = "DR_ListenPortBox";
            this.DR_ListenPortBox.Size = new System.Drawing.Size(67, 22);
            this.DR_ListenPortBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(294, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Port_Listen";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(178, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "IP";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(158, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "Port_Send";
            // 
            // DR_Log
            // 
            this.DR_Log.Location = new System.Drawing.Point(34, 167);
            this.DR_Log.Name = "DR_Log";
            this.DR_Log.Size = new System.Drawing.Size(420, 101);
            this.DR_Log.TabIndex = 7;
            this.DR_Log.Text = "";
            // 
            // DR_SendedMsg
            // 
            this.DR_SendedMsg.FormattingEnabled = true;
            this.DR_SendedMsg.Items.AddRange(new object[] {
            "~099,INI@",
            "~011,TrayMotion,0@",
            "~011,TrayMotion,1@",
            "~011,TrayMotion,2@"});
            this.DR_SendedMsg.Location = new System.Drawing.Point(217, 131);
            this.DR_SendedMsg.Name = "DR_SendedMsg";
            this.DR_SendedMsg.Size = new System.Drawing.Size(208, 20);
            this.DR_SendedMsg.TabIndex = 8;
            // 
            // DR_MsgSendBtn
            // 
            this.DR_MsgSendBtn.Location = new System.Drawing.Point(34, 108);
            this.DR_MsgSendBtn.Name = "DR_MsgSendBtn";
            this.DR_MsgSendBtn.Size = new System.Drawing.Size(97, 43);
            this.DR_MsgSendBtn.TabIndex = 9;
            this.DR_MsgSendBtn.Text = "Send";
            this.DR_MsgSendBtn.UseVisualStyleBackColor = true;
            this.DR_MsgSendBtn.Click += new System.EventHandler(this.DR_MsgSendBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(169, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "msg";
            // 
            // UcDrawer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DR_MsgSendBtn);
            this.Controls.Add(this.DR_SendedMsg);
            this.Controls.Add(this.DR_Log);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DR_ListenPortBox);
            this.Controls.Add(this.DR_SendPortBox);
            this.Controls.Add(this.DR_IPAddressBox);
            this.Controls.Add(this.DR_ConnectBtn);
            this.Name = "UcDrawer";
            this.Size = new System.Drawing.Size(542, 389);
            this.Load += new System.EventHandler(this.UcDrawer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DR_ConnectBtn;
        private System.Windows.Forms.TextBox DR_IPAddressBox;
        private System.Windows.Forms.TextBox DR_SendPortBox;
        private System.Windows.Forms.TextBox DR_ListenPortBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox DR_Log;
        private System.Windows.Forms.ComboBox DR_SendedMsg;
        private System.Windows.Forms.Button DR_MsgSendBtn;
        private System.Windows.Forms.Label label4;
    }
}
