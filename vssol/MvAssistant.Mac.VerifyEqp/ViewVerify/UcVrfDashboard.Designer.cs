namespace MvAssistantMacVerifyEqp.ViewVerify
{
    partial class UcDashboard
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
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.btnThousandTransferStart = new System.Windows.Forms.Button();
            this.btnThousandTransferStop = new System.Windows.Forms.Button();
            this.btnThousandTransferCheck = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtbLog
            // 
            this.rtbLog.Dock = System.Windows.Forms.DockStyle.Right;
            this.rtbLog.Location = new System.Drawing.Point(841, 0);
            this.rtbLog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(181, 695);
            this.rtbLog.TabIndex = 35;
            this.rtbLog.Text = "";
            // 
            // btnThousandTransferStart
            // 
            this.btnThousandTransferStart.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnThousandTransferStart.Location = new System.Drawing.Point(195, 68);
            this.btnThousandTransferStart.Name = "btnThousandTransferStart";
            this.btnThousandTransferStart.Size = new System.Drawing.Size(154, 56);
            this.btnThousandTransferStart.TabIndex = 36;
            this.btnThousandTransferStart.Text = "千傳 Start";
            this.btnThousandTransferStart.UseVisualStyleBackColor = true;
            // 
            // btnThousandTransferStop
            // 
            this.btnThousandTransferStop.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnThousandTransferStop.Location = new System.Drawing.Point(355, 68);
            this.btnThousandTransferStop.Name = "btnThousandTransferStop";
            this.btnThousandTransferStop.Size = new System.Drawing.Size(154, 56);
            this.btnThousandTransferStop.TabIndex = 37;
            this.btnThousandTransferStop.Text = "千傳 Stop";
            this.btnThousandTransferStop.UseVisualStyleBackColor = true;
            // 
            // btnThousandTransferCheck
            // 
            this.btnThousandTransferCheck.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnThousandTransferCheck.Location = new System.Drawing.Point(35, 68);
            this.btnThousandTransferCheck.Name = "btnThousandTransferCheck";
            this.btnThousandTransferCheck.Size = new System.Drawing.Size(154, 56);
            this.btnThousandTransferCheck.TabIndex = 38;
            this.btnThousandTransferCheck.Text = "千傳 Check";
            this.btnThousandTransferCheck.UseVisualStyleBackColor = true;
            // 
            // UcDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnThousandTransferCheck);
            this.Controls.Add(this.btnThousandTransferStop);
            this.Controls.Add(this.btnThousandTransferStart);
            this.Controls.Add(this.rtbLog);
            this.Name = "UcDashboard";
            this.Size = new System.Drawing.Size(1022, 695);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Button btnThousandTransferStart;
        private System.Windows.Forms.Button btnThousandTransferStop;
        private System.Windows.Forms.Button btnThousandTransferCheck;
    }
}
