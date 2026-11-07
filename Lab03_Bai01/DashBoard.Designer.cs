namespace Lab03
{
    partial class DashBoard
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
            this.btnOpenServer = new System.Windows.Forms.Button();
            this.btnOpenClient = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOpenServer
            // 
            this.btnOpenServer.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnOpenServer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenServer.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenServer.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnOpenServer.Location = new System.Drawing.Point(128, 116);
            this.btnOpenServer.Name = "btnOpenServer";
            this.btnOpenServer.Size = new System.Drawing.Size(162, 43);
            this.btnOpenServer.TabIndex = 0;
            this.btnOpenServer.Text = "UDP Server";
            this.btnOpenServer.UseVisualStyleBackColor = false;
            this.btnOpenServer.Click += new System.EventHandler(this.btnOpenServer_Click);
            // 
            // btnOpenClient
            // 
            this.btnOpenClient.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnOpenClient.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenClient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenClient.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenClient.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnOpenClient.Location = new System.Drawing.Point(425, 116);
            this.btnOpenClient.Name = "btnOpenClient";
            this.btnOpenClient.Size = new System.Drawing.Size(162, 43);
            this.btnOpenClient.TabIndex = 1;
            this.btnOpenClient.Text = "UDP Client";
            this.btnOpenClient.UseVisualStyleBackColor = false;
            this.btnOpenClient.Click += new System.EventHandler(this.btnOpenClient_Click);
            // 
            // DashBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnOpenClient);
            this.Controls.Add(this.btnOpenServer);
            this.Name = "DashBoard";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpenServer;
        private System.Windows.Forms.Button btnOpenClient;
    }
}

