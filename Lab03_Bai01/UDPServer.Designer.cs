namespace Lab03
{
    partial class UDPServer
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
            this.lsvMessages = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lsvMessages
            // 
            this.lsvMessages.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lsvMessages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lsvMessages.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsvMessages.FullRowSelect = true;
            this.lsvMessages.GridLines = true;
            this.lsvMessages.HideSelection = false;
            this.lsvMessages.Location = new System.Drawing.Point(95, 66);
            this.lsvMessages.Name = "lsvMessages";
            this.lsvMessages.Size = new System.Drawing.Size(581, 322);
            this.lsvMessages.TabIndex = 0;
            this.lsvMessages.UseCompatibleStateImageBehavior = false;
            this.lsvMessages.View = System.Windows.Forms.View.List;
            // 
            // UDPServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lsvMessages);
            this.Name = "UDPServer";
            this.Text = "UDPServer";
            this.Load += new System.EventHandler(this.UDPServer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lsvMessages;
    }
}