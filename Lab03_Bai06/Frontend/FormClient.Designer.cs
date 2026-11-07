namespace Lab03_Bai06
{
    partial class FormClient
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
            txtName = new TextBox();
            btnDisconnect = new Button();
            btnConnect = new Button();
            numPort = new NumericUpDown();
            lblName = new Label();
            lblPort = new Label();
            lblIP = new Label();
            txtServerIP = new TextBox();
            grpUsers = new GroupBox();
            lstUsers = new ListBox();
            flpChat = new FlowLayoutPanel();
            pnlBottom = new Panel();
            btnSendFile = new Button();
            btnSendImage = new Button();
            btnSend = new Button();
            txtMessage = new TextBox();
            lblTarget = new Label();
            pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPort).BeginInit();
            grpUsers.SuspendLayout();
            pnlBottom.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.Controls.Add(txtName);
            pnlTop.Controls.Add(btnDisconnect);
            pnlTop.Controls.Add(btnConnect);
            pnlTop.Controls.Add(numPort);
            pnlTop.Controls.Add(lblName);
            pnlTop.Controls.Add(lblPort);
            pnlTop.Controls.Add(lblIP);
            pnlTop.Controls.Add(txtServerIP);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(876, 116);
            pnlTop.TabIndex = 1;
            // 
            // txtName
            // 
            txtName.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            txtName.Location = new Point(669, 18);
            txtName.Name = "txtName";
            txtName.Size = new Size(195, 25);
            txtName.TabIndex = 6;
            // 
            // btnDisconnect
            // 
            btnDisconnect.BackColor = SystemColors.ControlDark;
            btnDisconnect.Enabled = false;
            btnDisconnect.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDisconnect.Location = new Point(652, 63);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(136, 34);
            btnDisconnect.TabIndex = 5;
            btnDisconnect.Text = "DISCONNECT";
            btnDisconnect.UseVisualStyleBackColor = false;
            // 
            // btnConnect
            // 
            btnConnect.BackColor = SystemColors.ControlDark;
            btnConnect.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnConnect.Location = new Point(487, 63);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(136, 34);
            btnConnect.TabIndex = 4;
            btnConnect.Text = "CONNECT";
            btnConnect.UseVisualStyleBackColor = false;
            // 
            // numPort
            // 
            numPort.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            numPort.Location = new Point(355, 20);
            numPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            numPort.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numPort.Name = "numPort";
            numPort.Size = new Size(120, 29);
            numPort.TabIndex = 1;
            numPort.Value = new decimal(new int[] { 8080, 0, 0, 0 });
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblName.Location = new Point(620, 20);
            lblName.Name = "lblName";
            lblName.Size = new Size(43, 21);
            lblName.TabIndex = 3;
            lblName.Text = "Tên: ";
            // 
            // lblPort
            // 
            lblPort.AutoSize = true;
            lblPort.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPort.Location = new Point(305, 22);
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
            lblIP.Size = new Size(84, 21);
            lblIP.TabIndex = 1;
            lblIP.Text = "Server IP: ";
            // 
            // txtServerIP
            // 
            txtServerIP.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            txtServerIP.Location = new Point(99, 19);
            txtServerIP.Name = "txtServerIP";
            txtServerIP.Size = new Size(164, 25);
            txtServerIP.TabIndex = 1;
            txtServerIP.Text = "127.0.0.1";
            // 
            // grpUsers
            // 
            grpUsers.Controls.Add(lstUsers);
            grpUsers.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            grpUsers.Location = new Point(604, 122);
            grpUsers.Name = "grpUsers";
            grpUsers.Size = new Size(260, 401);
            grpUsers.TabIndex = 3;
            grpUsers.TabStop = false;
            grpUsers.Text = "Người dùng online";
            // 
            // lstUsers
            // 
            lstUsers.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lstUsers.FormattingEnabled = true;
            lstUsers.Location = new Point(6, 29);
            lstUsers.Name = "lstUsers";
            lstUsers.Size = new Size(248, 361);
            lstUsers.TabIndex = 0;
            // 
            // flpChat
            // 
            flpChat.BorderStyle = BorderStyle.FixedSingle;
            flpChat.FlowDirection = FlowDirection.TopDown;
            flpChat.Location = new Point(10, 134);
            flpChat.Name = "flpChat";
            flpChat.Size = new Size(588, 389);
            flpChat.TabIndex = 4;
            flpChat.WrapContents = false;
            // 
            // pnlBottom
            // 
            pnlBottom.Controls.Add(btnSendFile);
            pnlBottom.Controls.Add(btnSendImage);
            pnlBottom.Controls.Add(btnSend);
            pnlBottom.Controls.Add(txtMessage);
            pnlBottom.Controls.Add(lblTarget);
            pnlBottom.Location = new Point(12, 529);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new Size(846, 112);
            pnlBottom.TabIndex = 5;
            // 
            // btnSendFile
            // 
            btnSendFile.BackColor = SystemColors.ControlDark;
            btnSendFile.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSendFile.Location = new Point(676, 18);
            btnSendFile.Name = "btnSendFile";
            btnSendFile.Size = new Size(136, 34);
            btnSendFile.TabIndex = 9;
            btnSendFile.Text = "FILE";
            btnSendFile.UseVisualStyleBackColor = false;
            // 
            // btnSendImage
            // 
            btnSendImage.BackColor = SystemColors.ControlDark;
            btnSendImage.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSendImage.Location = new Point(498, 64);
            btnSendImage.Name = "btnSendImage";
            btnSendImage.Size = new Size(136, 34);
            btnSendImage.TabIndex = 8;
            btnSendImage.Text = "GỬI";
            btnSendImage.UseVisualStyleBackColor = false;
            // 
            // btnSend
            // 
            btnSend.BackColor = SystemColors.ControlDark;
            btnSend.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSend.Location = new Point(676, 64);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(136, 34);
            btnSend.TabIndex = 7;
            btnSend.Text = "ẢNH";
            btnSend.UseVisualStyleBackColor = false;
            // 
            // txtMessage
            // 
            txtMessage.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            txtMessage.Location = new Point(22, 64);
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(459, 25);
            txtMessage.TabIndex = 7;
            // 
            // lblTarget
            // 
            lblTarget.AutoSize = true;
            lblTarget.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTarget.Location = new Point(22, 31);
            lblTarget.Name = "lblTarget";
            lblTarget.Size = new Size(191, 21);
            lblTarget.TabIndex = 7;
            lblTarget.Text = "Đang gửi tới mọi người...";
            // 
            // FormClient
            // 
            ClientSize = new Size(876, 653);
            Controls.Add(pnlBottom);
            Controls.Add(flpChat);
            Controls.Add(grpUsers);
            Controls.Add(pnlTop);
            Name = "FormClient";
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numPort).EndInit();
            grpUsers.ResumeLayout(false);
            pnlBottom.ResumeLayout(false);
            pnlBottom.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTop;
        private Button btnDisconnect;
        private Button btnConnect;
        private NumericUpDown numPort;
        private Label lblName;
        private Label lblPort;
        private Label lblIP;
        private TextBox txtServerIP;
        private GroupBox grpUsers;
        private ListBox lstUsers;
        private TextBox txtName;
        private FlowLayoutPanel flpChat;
        private Panel pnlBottom;
        private Button btnSendFile;
        private Button btnSendImage;
        private Button btnSend;
        private TextBox txtMessage;
        private Label lblTarget;
    }
}
