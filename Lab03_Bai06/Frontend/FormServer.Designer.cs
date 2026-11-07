namespace Lab03_Bai06
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
            pnlTop = new Panel();
            btnStop = new Button();
            btnStart = new Button();
            numPort = new NumericUpDown();
            lblCount = new Label();
            lblPort = new Label();
            lblIP = new Label();
            txtIP = new TextBox();
            grpOnline = new GroupBox();
            lstOnline = new ListBox();
            lstLog = new ListBox();
            pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPort).BeginInit();
            grpOnline.SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.Controls.Add(btnStop);
            pnlTop.Controls.Add(btnStart);
            pnlTop.Controls.Add(numPort);
            pnlTop.Controls.Add(lblCount);
            pnlTop.Controls.Add(lblPort);
            pnlTop.Controls.Add(lblIP);
            pnlTop.Controls.Add(txtIP);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(800, 116);
            pnlTop.TabIndex = 0;
            // 
            // btnStop
            // 
            btnStop.BackColor = SystemColors.ControlDark;
            btnStop.Enabled = false;
            btnStop.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnStop.Location = new Point(652, 63);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(136, 34);
            btnStop.TabIndex = 5;
            btnStop.Text = "STOP";
            btnStop.UseVisualStyleBackColor = false;
            // 
            // btnStart
            // 
            btnStart.BackColor = SystemColors.ControlDark;
            btnStart.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnStart.Location = new Point(487, 63);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(136, 34);
            btnStart.TabIndex = 4;
            btnStart.Text = "START";
            btnStart.UseVisualStyleBackColor = false;
            // 
            // numPort
            // 
            numPort.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            numPort.Location = new Point(319, 22);
            numPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            numPort.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numPort.Name = "numPort";
            numPort.Size = new Size(120, 29);
            numPort.TabIndex = 1;
            numPort.Value = new decimal(new int[] { 8080, 0, 0, 0 });
            // 
            // lblCount
            // 
            lblCount.AutoSize = true;
            lblCount.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCount.Location = new Point(700, 19);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(74, 21);
            lblCount.TabIndex = 3;
            lblCount.Text = "Online: 0";
            // 
            // lblPort
            // 
            lblPort.AutoSize = true;
            lblPort.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPort.Location = new Point(269, 22);
            lblPort.Name = "lblPort";
            lblPort.Size = new Size(44, 21);
            lblPort.TabIndex = 2;
            lblPort.Text = "Port:";
            // 
            // lblIP
            // 
            lblIP.AutoSize = true;
            lblIP.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblIP.Location = new Point(10, 22);
            lblIP.Name = "lblIP";
            lblIP.Size = new Size(28, 21);
            lblIP.TabIndex = 1;
            lblIP.Text = "IP:";
            // 
            // txtIP
            // 
            txtIP.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            txtIP.Location = new Point(44, 19);
            txtIP.Name = "txtIP";
            txtIP.Size = new Size(164, 25);
            txtIP.TabIndex = 1;
            txtIP.Text = "0.0.0.0";
            // 
            // grpOnline
            // 
            grpOnline.Controls.Add(lstOnline);
            grpOnline.Dock = DockStyle.Right;
            grpOnline.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            grpOnline.Location = new Point(528, 116);
            grpOnline.Name = "grpOnline";
            grpOnline.Size = new Size(272, 346);
            grpOnline.TabIndex = 1;
            grpOnline.TabStop = false;
            grpOnline.Text = "Người Online";
            // 
            // lstOnline
            // 
            lstOnline.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lstOnline.FormattingEnabled = true;
            lstOnline.Location = new Point(6, 29);
            lstOnline.Name = "lstOnline";
            lstOnline.Size = new Size(260, 298);
            lstOnline.TabIndex = 0;
            // 
            // lstLog
            // 
            lstLog.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lstLog.FormattingEnabled = true;
            lstLog.HorizontalScrollbar = true;
            lstLog.Location = new Point(10, 145);
            lstLog.Name = "lstLog";
            lstLog.Size = new Size(512, 298);
            lstLog.TabIndex = 1;
            // 
            // FormServer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 462);
            Controls.Add(lstLog);
            Controls.Add(grpOnline);
            Controls.Add(pnlTop);
            Name = "FormServer";
            Text = "FormServer";
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numPort).EndInit();
            grpOnline.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTop;
        private TextBox txtIP;
        private NumericUpDown numPort;
        private Label lblCount;
        private Label lblPort;
        private Label lblIP;
        private Button btnStop;
        private Button btnStart;
        private GroupBox grpOnline;
        private ListBox lstOnline;
        private ListBox lstLog;
    }
}
