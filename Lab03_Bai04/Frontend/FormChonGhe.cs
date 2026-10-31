using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using api1;
using DTO;

namespace LAB3_CAU5
{
    public partial class FormChonGhe : Form
    {
        private int _maPhong;
        private List<Button> list15Ghe;
        public List<GheDto> DanhSachGheDaChon { get; set; } = new List<GheDto>();
        public FormChonGhe(int maPhong)
        {
            InitializeComponent();
            _maPhong = maPhong;
            this.Load += FormChonGhe_Load;
            list15Ghe = new List<Button> {
                A1, A2, A3, A4, A5,
                B1, B2, B3, B4, B5,
                C1, C2, C3, C4, C5
            };

        }
        private async void FormChonGhe_Load(object sender, EventArgs e)
        {
            await LoadSoDoGhe();
        }
        private async Task LoadSoDoGhe()
        {
            DanhSachGheDaChon.Clear();
            try
            {
                var dsGhe = await api2.GetSeatAsync(_maPhong);
                foreach (Button btn in list15Ghe)
                {
                    btn.UseVisualStyleBackColor = false;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.Click -= BtnGhe_Click;
                    GheDto ghe = dsGhe.FirstOrDefault(g => g.TenGhe.Trim().Equals(btn.Text.Trim(), StringComparison.OrdinalIgnoreCase));
                    if (ghe != null)
                    {

                        btn.Tag = ghe;
                        if (ghe.TTGhe == 1)
                        {
                            btn.BackColor = Color.Gray;      
                            btn.ForeColor = Color.White;
                            btn.Enabled = false;
                        }
                        else
                        {
                            btn.BackColor = Color.White;
                            btn.ForeColor = Color.Black;
                            btn.Enabled = true;
                            btn.Click += BtnGhe_Click;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải sơ đồ ghế: " + ex.Message);
            }
        }
        private void BtnGhe_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null || !(btn.Tag is GheDto ghe)) return;
            if (btn.BackColor == Color.White)
            {
                btn.BackColor = Color.LightGreen;
                if (!DanhSachGheDaChon.Contains(ghe))
                {
                    DanhSachGheDaChon.Add(ghe);
                }
            }
            else if (btn.BackColor == Color.LightGreen)
            {
                btn.BackColor = Color.White;
                DanhSachGheDaChon.Remove(ghe);
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (DanhSachGheDaChon.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất 1 ghế trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        
    }
}
