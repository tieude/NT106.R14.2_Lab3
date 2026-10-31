using System;
using System.Collections.Generic;
using System.Drawing; 
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using api1;
using DTO;



namespace LAB3_CAU5
{
    public partial class Form1 : Form
    {
        private List<GheDto> dsGheDaChon = new List<GheDto>();
        public Form1()
        {
            InitializeComponent();
            this.Load += FormMain_Load;

        }
        private async void FormMain_Load(object sender, EventArgs e)
        {
            cboPhong.Enabled = false;
            btnDatVe.Enabled = false;
            btnChonGhe.Enabled = false;
            txtGheChon.ReadOnly = true;

            try
            {
                var dsPhim = await api2.GetMoviesAsync();

                cboPhim.DataSource = dsPhim;
                cboPhim.DisplayMember = "TenPhim";
                cboPhim.ValueMember = "MaPhim";
                cboPhim.SelectedIndex = -1;
                cboPhim.SelectedIndexChanged += cboPhim_SelectedIndexChanged;
                cboPhong.SelectedIndexChanged += cboPhong_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load danh sách phim: " + ex.Message);
            }
        }

        private async void cboPhim_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPhim.SelectedIndex == -1 || cboPhim.SelectedItem == null) return;
            if (cboPhim.SelectedIndex != -1 && cboPhim.SelectedItem is PhimDto phimChon)
            {
                try
                {
                    int maPhim = phimChon.MaPhim;
                    var dsPhong = await api2.GetRoomAsync(maPhim);
                    cboPhong.DataSource = dsPhong;
                    cboPhong.DisplayMember = "TenPhong";
                    cboPhong.ValueMember = "MaPhong";
                    
                    cboPhong.SelectedIndex = -1;
                    cboPhong.Enabled = true;
                    ResetGhe();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("L?i l?y danh sách phong" + ex.Message);
                }
            }
            else
            {

                cboPhong.DataSource = null;
                cboPhong.Enabled = false;
                ResetGhe();
            }

        }

        private void cboPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboPhong.SelectedIndex != -1 && cboPhong.SelectedValue != null)
            {
                btnChonGhe.Enabled = true;
                ResetGhe();
            }
            else
            {
                btnChonGhe .Enabled = false;
                ResetGhe();
            }
        }

        private void btnChonGhe_Click(object sender, EventArgs e)
        {
            if (cboPhong.SelectedValue == null) return;
            if (!(cboPhim.SelectedItem is PhimDto phimChon))
            {
                MessageBox.Show("Vui lòng chọn phim trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            double giaGocPhim = phimChon.GiaGoc;
            int maPhong = Convert.ToInt32(cboPhong.SelectedValue);
            FormChonGhe frm = new FormChonGhe(maPhong);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                dsGheDaChon = frm.DanhSachGheDaChon;
                string tenGheChuoi = string.Join(",", dsGheDaChon.Select(g => g.TenGhe));
                double tongTien = 0;
                foreach (var g in dsGheDaChon)
                {
                    double giaVe = giaGocPhim;
                    if (g.LoaiGhe == 2) giaVe *= 1.5;
                    else if (g.LoaiGhe == 3) giaVe *= 0.25;
                    tongTien += giaVe;
                }
                txtGheChon.Text = $"Ghế: [{tenGheChuoi}]-Tổng tiền: {tongTien:N0}VNĐ";
                btnDatVe.Enabled = (dsGheDaChon.Count > 0);

            }
        }

        private async void btnDatVe_Click(object sender, EventArgs e)
        {
            if (dsGheDaChon.Count == 0) return;
            List<int> dsMaGhe = dsGheDaChon.Select(g => g.MaGhe).ToList();
            try
            {
                BookResponseDto result = await api2.BookSeatsAsync(dsMaGhe);

                if (result != null && result.Success)
                {
                    MessageBox.Show("Xác nhận đặt vé thành công! Trạng thái ghế đã được cập nhật.", "Thông báo");
                    ResetGhe();
                }
                else
                {
                    string msg = result?.Message ?? "Đặt vé thất bại! Có thể ghế đã bị người khác nhanh tay chọn trước.";
                    MessageBox.Show(msg, "Thông báo");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi kết nối đặt vé: " + ex.Message);
            }
        }
        private void ResetGhe()
        {
            dsGheDaChon.Clear();
            txtGheChon.Clear();
            btnDatVe.Enabled = false;
        }
    }
}
