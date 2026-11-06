namespace lab3_bai3
{
    partial class FormServer
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
            btnListen = new Button();
            rtbServerLog = new RichTextBox();
            SuspendLayout();
            // 
            // btnListen
            // 
            btnListen.BackColor = Color.FromArgb(192, 255, 255);
            btnListen.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnListen.Location = new Point(325, 33);
            btnListen.Name = "btnListen";
            btnListen.Size = new Size(94, 29);
            btnListen.TabIndex = 0;
            btnListen.Text = "Listen";
            btnListen.UseVisualStyleBackColor = false;
            btnListen.Click += btnListen_Click;
            // 
            // rtbServerLog
            // 
            rtbServerLog.Location = new Point(57, 79);
            rtbServerLog.Name = "rtbServerLog";
            rtbServerLog.Size = new Size(362, 209);
            rtbServerLog.TabIndex = 1;
            rtbServerLog.Text = "";
            // 
            // FormServer
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(472, 315);
            Controls.Add(rtbServerLog);
            Controls.Add(btnListen);
            Name = "FormServer";
            Text = "FormServer";
            ResumeLayout(false);
        }

        #endregion

        private Button btnListen;
        private RichTextBox rtbServerLog;
    }
}