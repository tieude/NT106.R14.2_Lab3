namespace lab3_bai3
{
    partial class FormDashboard
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
            btnOpenServer = new Button();
            btnOpenClient = new Button();
            SuspendLayout();
            // 
            // btnOpenServer
            // 
            btnOpenServer.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnOpenServer.ForeColor = Color.SteelBlue;
            btnOpenServer.Location = new Point(114, 56);
            btnOpenServer.Name = "btnOpenServer";
            btnOpenServer.Size = new Size(231, 51);
            btnOpenServer.TabIndex = 0;
            btnOpenServer.Text = "Open TCP Server";
            btnOpenServer.UseVisualStyleBackColor = true;
            btnOpenServer.Click += btnOpenServer_Click;
            // 
            // btnOpenClient
            // 
            btnOpenClient.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnOpenClient.ForeColor = Color.SteelBlue;
            btnOpenClient.Location = new Point(114, 126);
            btnOpenClient.Name = "btnOpenClient";
            btnOpenClient.Size = new Size(231, 53);
            btnOpenClient.TabIndex = 1;
            btnOpenClient.Text = "Open new TCP client";
            btnOpenClient.UseVisualStyleBackColor = true;
            btnOpenClient.Click += btnOpenClient_Click;
            // 
            // FormDashboard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(451, 231);
            Controls.Add(btnOpenClient);
            Controls.Add(btnOpenServer);
            Name = "FormDashboard";
            Text = "FormDashboard";
            ResumeLayout(false);
        }

        #endregion

        private Button btnOpenServer;
        private Button btnOpenClient;
    }
}