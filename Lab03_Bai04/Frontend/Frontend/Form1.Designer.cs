namespace LAB3_CAU5
{
    partial class Form1
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
            cboPhim = new ComboBox();
            cboPhong = new ComboBox();
            txtGheChon = new TextBox();
            btnDatVe = new Button();
            label1 = new Label();
            label2 = new Label();
            btnChonGhe = new Button();
            SuspendLayout();
            // 
            // cboPhim
            // 
            cboPhim.FormattingEnabled = true;
            cboPhim.Location = new Point(152, 74);
            cboPhim.Name = "cboPhim";
            cboPhim.Size = new Size(182, 33);
            cboPhim.TabIndex = 0;
            cboPhim.SelectedIndexChanged += cboPhim_SelectedIndexChanged;
            // 
            // cboPhong
            // 
            cboPhong.FormattingEnabled = true;
            cboPhong.Location = new Point(152, 136);
            cboPhong.Name = "cboPhong";
            cboPhong.Size = new Size(182, 33);
            cboPhong.TabIndex = 1;
            cboPhong.SelectedIndexChanged += cboPhong_SelectedIndexChanged;
            // 
            // txtGheChon
            // 
            txtGheChon.Location = new Point(76, 224);
            txtGheChon.Name = "txtGheChon";
            txtGheChon.Size = new Size(222, 31);
            txtGheChon.TabIndex = 3;
            // 
            // btnDatVe
            // 
            btnDatVe.Location = new Point(114, 303);
            btnDatVe.Name = "btnDatVe";
            btnDatVe.Size = new Size(112, 34);
            btnDatVe.TabIndex = 4;
            btnDatVe.Text = "đặt vé";
            btnDatVe.UseVisualStyleBackColor = true;
            btnDatVe.Click += btnDatVe_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(30, 74);
            label1.Name = "label1";
            label1.Size = new Size(84, 25);
            label1.TabIndex = 5;
            label1.Text = "Tên phim";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 136);
            label2.Name = "label2";
            label2.Size = new Size(96, 25);
            label2.TabIndex = 6;
            label2.Text = "Tên phòng";
            // 
            // btnChonGhe
            // 
            btnChonGhe.Location = new Point(401, 224);
            btnChonGhe.Name = "btnChonGhe";
            btnChonGhe.Size = new Size(112, 34);
            btnChonGhe.TabIndex = 7;
            btnChonGhe.Text = "Chọn ghế";
            btnChonGhe.UseVisualStyleBackColor = true;
            btnChonGhe.Click += btnChonGhe_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(980, 651);
            Controls.Add(btnChonGhe);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnDatVe);
            Controls.Add(txtGheChon);
            Controls.Add(cboPhong);
            Controls.Add(cboPhim);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cboPhim;
        private ComboBox cboPhong;
        private TextBox txtGheChon;
        private Button btnDatVe;
        private Label label1;
        private Label label2;
        private Button btnChonGhe;
    }
}
