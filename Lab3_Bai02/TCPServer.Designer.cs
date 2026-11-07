namespace Lab3_Bai02
{
    partial class TCPServer
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
            this.btnListen = new System.Windows.Forms.Button();
            this.listViewCommand = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // btnListen
            // 
            this.btnListen.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnListen.FlatAppearance.BorderSize = 0;
            this.btnListen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnListen.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnListen.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnListen.Location = new System.Drawing.Point(46, 56);
            this.btnListen.Name = "btnListen";
            this.btnListen.Size = new System.Drawing.Size(119, 36);
            this.btnListen.TabIndex = 0;
            this.btnListen.Text = "Listen";
            this.btnListen.UseVisualStyleBackColor = false;
            this.btnListen.Click += new System.EventHandler(this.btnListen_Click);
            // 
            // listViewCommand
            // 
            this.listViewCommand.BackColor = System.Drawing.SystemColors.ControlLight;
            this.listViewCommand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewCommand.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewCommand.FullRowSelect = true;
            this.listViewCommand.GridLines = true;
            this.listViewCommand.HideSelection = false;
            this.listViewCommand.Location = new System.Drawing.Point(209, 56);
            this.listViewCommand.Name = "listViewCommand";
            this.listViewCommand.Size = new System.Drawing.Size(493, 358);
            this.listViewCommand.TabIndex = 1;
            this.listViewCommand.UseCompatibleStateImageBehavior = false;
            this.listViewCommand.View = System.Windows.Forms.View.List;
            // 
            // TCPServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listViewCommand);
            this.Controls.Add(this.btnListen);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "TCPServer";
            this.Text = "TCPServer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnListen;
        private System.Windows.Forms.ListView listViewCommand;
    }
}