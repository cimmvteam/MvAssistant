namespace MvAssistantMacVerifyEqp.ViewUc
{
    partial class UcLoadPort
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
            this.LP_IPList = new System.Windows.Forms.ComboBox();
            this.LP_Port = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LP_ConnectBtn = new System.Windows.Forms.Button();
            this.LP_MsgSendBtn = new System.Windows.Forms.Button();
            this.LP_SendMsgList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.labConnection = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LP_IPList
            // 
            this.LP_IPList.FormattingEnabled = true;
            this.LP_IPList.Location = new System.Drawing.Point(200, 60);
            this.LP_IPList.Name = "LP_IPList";
            this.LP_IPList.Size = new System.Drawing.Size(145, 20);
            this.LP_IPList.TabIndex = 0;
            // 
            // LP_Port
            // 
            this.LP_Port.Location = new System.Drawing.Point(389, 58);
            this.LP_Port.Name = "LP_Port";
            this.LP_Port.Size = new System.Drawing.Size(100, 22);
            this.LP_Port.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(168, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(357, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port";
            // 
            // LP_ConnectBtn
            // 
            this.LP_ConnectBtn.Location = new System.Drawing.Point(44, 50);
            this.LP_ConnectBtn.Name = "LP_ConnectBtn";
            this.LP_ConnectBtn.Size = new System.Drawing.Size(81, 38);
            this.LP_ConnectBtn.TabIndex = 4;
            this.LP_ConnectBtn.Text = "Connect";
            this.LP_ConnectBtn.UseVisualStyleBackColor = true;
            this.LP_ConnectBtn.Click += new System.EventHandler(this.LP_ConnectBtn_Click);
            // 
            // LP_MsgSendBtn
            // 
            this.LP_MsgSendBtn.Location = new System.Drawing.Point(44, 107);
            this.LP_MsgSendBtn.Name = "LP_MsgSendBtn";
            this.LP_MsgSendBtn.Size = new System.Drawing.Size(81, 38);
            this.LP_MsgSendBtn.TabIndex = 5;
            this.LP_MsgSendBtn.Text = "Send";
            this.LP_MsgSendBtn.UseVisualStyleBackColor = true;
            // 
            // LP_SendMsgList
            // 
            this.LP_SendMsgList.FormattingEnabled = true;
            this.LP_SendMsgList.Location = new System.Drawing.Point(200, 117);
            this.LP_SendMsgList.Name = "LP_SendMsgList";
            this.LP_SendMsgList.Size = new System.Drawing.Size(145, 20);
            this.LP_SendMsgList.TabIndex = 6;
            this.LP_SendMsgList.SelectedIndexChanged += new System.EventHandler(this.LP_SendMsgList_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(168, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "Msg";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(44, 183);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(462, 274);
            this.richTextBox1.TabIndex = 8;
            this.richTextBox1.Text = "";
            // 
            // labConnection
            // 
            this.labConnection.AutoSize = true;
            this.labConnection.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labConnection.Location = new System.Drawing.Point(399, 117);
            this.labConnection.Name = "labConnection";
            this.labConnection.Size = new System.Drawing.Size(107, 19);
            this.labConnection.TabIndex = 9;
            this.labConnection.Text = "Disconnected";
            // 
            // UcLoadPort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labConnection);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LP_SendMsgList);
            this.Controls.Add(this.LP_MsgSendBtn);
            this.Controls.Add(this.LP_ConnectBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LP_Port);
            this.Controls.Add(this.LP_IPList);
            this.Name = "UcLoadPort";
            this.Size = new System.Drawing.Size(587, 537);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox LP_IPList;
        private System.Windows.Forms.TextBox LP_Port;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button LP_ConnectBtn;
        private System.Windows.Forms.Button LP_MsgSendBtn;
        private System.Windows.Forms.ComboBox LP_SendMsgList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label labConnection;
    }
}
