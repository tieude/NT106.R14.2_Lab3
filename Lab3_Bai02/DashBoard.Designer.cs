namespace Lab3_Bai02
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
            this.btnOpenTCPServer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOpenTCPServer
            // 
            this.btnOpenTCPServer.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnOpenTCPServer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenTCPServer.FlatAppearance.BorderSize = 0;
            this.btnOpenTCPServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenTCPServer.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenTCPServer.ForeColor = System.Drawing.SystemColors.Window;
            this.btnOpenTCPServer.Location = new System.Drawing.Point(318, 194);
            this.btnOpenTCPServer.Name = "btnOpenTCPServer";
            this.btnOpenTCPServer.Size = new System.Drawing.Size(150, 43);
            this.btnOpenTCPServer.TabIndex = 0;
            this.btnOpenTCPServer.Text = "Mở TCP Server";
            this.btnOpenTCPServer.UseVisualStyleBackColor = false;
            this.btnOpenTCPServer.Click += new System.EventHandler(this.btnOpenTCPServer_Click);
            // 
            // DashBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnOpenTCPServer);
            this.Name = "DashBoard";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.DashBoard_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpenTCPServer;
    }
}

