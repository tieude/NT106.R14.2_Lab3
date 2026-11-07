namespace Lab03_Bai06
{
    partial class FormMenu
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitle = new Label();
            btnOpenClient = new Button();
            btnOpenServer = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(124, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(195, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "BÀI 6 - Chat Room";
            // 
            // btnOpenClient
            // 
            btnOpenClient.BackColor = SystemColors.ButtonShadow;
            btnOpenClient.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnOpenClient.Location = new Point(263, 89);
            btnOpenClient.Name = "btnOpenClient";
            btnOpenClient.Size = new Size(174, 42);
            btnOpenClient.TabIndex = 2;
            btnOpenClient.Text = "Mở CLIENT";
            btnOpenClient.UseVisualStyleBackColor = false;
            // 
            // btnOpenServer
            // 
            btnOpenServer.BackColor = SystemColors.ButtonShadow;
            btnOpenServer.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnOpenServer.Location = new Point(12, 89);
            btnOpenServer.Name = "btnOpenServer";
            btnOpenServer.Size = new Size(174, 42);
            btnOpenServer.TabIndex = 3;
            btnOpenServer.Text = "Mở SERVER";
            btnOpenServer.UseVisualStyleBackColor = false;
            // 
            // FormMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(449, 223);
            Controls.Add(btnOpenServer);
            Controls.Add(btnOpenClient);
            Controls.Add(lblTitle);
            Name = "FormMenu";
            Text = "Menu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Button btnOpenClient;
        private Button btnOpenServer;
    }
}

