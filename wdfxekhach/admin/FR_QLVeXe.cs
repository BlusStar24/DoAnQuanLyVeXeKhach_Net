using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wdfxekhach.admin
{
    public partial class FR_QLVeXe : Form
    {
        public FR_QLVeXe()
        {
            InitializeComponent();
        }
        CONNECT cn = new CONNECT();
        public void LoadVeXe()
        {
            try
            {
                // Lấy tất cả vé xe từ bảng VEXE (có thể chỉnh sửa câu truy vấn nếu cần thêm điều kiện lọc)
                var veXeList = from ve in cn.db.VEXEs
                               join hk in cn.db.HANHKHACHes on ve.MaHanhKhach equals hk.MaHanhKhach
                               join cx in cn.db.CHUYENXEs on ve.MaChuyenXe equals cx.MaChuyenXe
                               select new
                               {
                                   ve.MaVeXe,
                                   hk.TenHanhKhach,
                                   hk.SoDienThoai,
                                   cx.MaChuyenXe,
                                   GiaTien = (double?)cx.GiaTien,
                                   ve.SoGhe,
                                   ve.TrangThai
                               };

                // Đưa dữ liệu vào DataGridView
                dataGridView1.DataSource = veXeList.ToList();

                // Tùy chỉnh các cột DataGridView nếu cần thiết
                dataGridView1.Columns["MaVeXe"].HeaderText = "Mã Vé";
                dataGridView1.Columns["TenHanhKhach"].HeaderText = "Tên Khách Hàng";
                dataGridView1.Columns["SoDienThoai"].HeaderText = "Số Điện Thoại";
                dataGridView1.Columns["MaChuyenXe"].HeaderText = "Mã Chuyến Xe";
                dataGridView1.Columns["GiaTien"].HeaderText = "Giá Vé";
                dataGridView1.Columns["SoGhe"].HeaderText = "Số Ghế";
                dataGridView1.Columns["TrangThai"].HeaderText = "Trạng Thái";

              
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load vé xe: " + ex.Message);
            }
        }

        private void FR_QLVeXe_Load(object sender, EventArgs e)
        {
            LoadVeXe();
            btn_luu.Enabled = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu người dùng nhấn vào cột "TrangThai"
            if (e.RowIndex >= 0)  // Đảm bảo không phải nhấn vào header
            {
                // Lấy thông tin của các cột trong dòng đã chọn
                var row = dataGridView1.Rows[e.RowIndex];

                // Lấy giá trị từ các cột và hiển thị lên các TextBox
                string tenKhachHang = row.Cells["TenHanhKhach"].Value.ToString();
                string soDienThoai = row.Cells["SoDienThoai"].Value.ToString();
                string trangThai = row.Cells["TrangThai"].Value.ToString();
                string mave = row.Cells[0].Value.ToString();
                // Hiển thị thông tin vào các TextBox
                txt_ten.Text = tenKhachHang;
                txt_sdt.Text = soDienThoai;
                txt_mave.Text = mave;
               if(trangThai == "True")
                {
                    check_true.Checked = true;
                }    
            }
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            LoadVeXe();
           
        }
        public bool TimKiemKhachHang(string soDienThoai, string tenhk, int mavexe)
        {
            try
            {
                // Tìm kiếm khách hàng dựa vào số điện thoại hoặc tên
                var ketQua = from ve in cn.db.VEXEs
                             join hk in cn.db.HANHKHACHes on ve.MaHanhKhach equals hk.MaHanhKhach
                             join cx in cn.db.CHUYENXEs on ve.MaChuyenXe equals cx.MaChuyenXe
                             where hk.SoDienThoai == soDienThoai && hk.TenHanhKhach == tenhk && ve.MaVeXe == mavexe
                             select new
                             {
                                 ve.MaVeXe,
                                 hk.TenHanhKhach,
                                 hk.SoDienThoai,
                                 cx.MaChuyenXe,
                                 GiaTien = (double?)cx.GiaTien,
                                 ve.SoGhe,
                                 ve.TrangThai
                             };

                // Đưa kết quả tìm kiếm vào DataGridView
                var ketQuaList = ketQua.ToList();
                dataGridView1.DataSource = ketQuaList;

                // Tùy chỉnh các cột DataGridView nếu cần thiết
                dataGridView1.Columns["MaVeXe"].HeaderText = "Mã Vé";
                dataGridView1.Columns["TenHanhKhach"].HeaderText = "Tên Khách Hàng";
                dataGridView1.Columns["SoDienThoai"].HeaderText = "Số Điện Thoại";
                dataGridView1.Columns["MaChuyenXe"].HeaderText = "Mã Chuyến Xe";
                dataGridView1.Columns["GiaTien"].HeaderText = "Giá Vé";
                dataGridView1.Columns["SoGhe"].HeaderText = "Số Ghế";
                dataGridView1.Columns["TrangThai"].HeaderText = "Trạng Thái";

                // Kiểm tra nếu không tìm thấy kết quả
                if (!ketQuaList.Any())
                {
                    MessageBox.Show("Không tìm thấy khách hàng với thông tin đã nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                return true; // Tìm thấy kết quả
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm khách hàng: " + ex.Message);
                return false;
            }
        }


        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            // Gọi hàm tìm kiếm và bật nút lưu nếu tìm thấy kết quả
            if (TimKiemKhachHang(txt_sdt.Text.Trim(), txt_ten.Text.Trim(), int.Parse(txt_mave.Text.Trim())))
            {
                btn_luu.Enabled = true;
            }
            else
            {
                btn_luu.Enabled = false;
            }
        }
        public void UpdateTrangThai()
        {
            try
            {
                // Lấy mã vé từ TextBox
                if (string.IsNullOrEmpty(txt_mave.Text))
                {
                    MessageBox.Show("Vui lòng chọn mã vé để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txt_mave.Text, out int maVeXe))
                {
                    MessageBox.Show("Mã vé không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tìm vé trong cơ sở dữ liệu
                var veXe = cn.db.VEXEs.FirstOrDefault(v => v.MaVeXe == maVeXe);

                if (veXe == null)
                {
                    MessageBox.Show($"Không tìm thấy vé với mã {maVeXe} trong cơ sở dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Xác định trạng thái mới dựa vào checkbox
                bool trangThaiMoi = check_false.Checked ? false : true;

                // Cập nhật trạng thái vé
                veXe.TrangThai = trangThaiMoi;

                // Lưu thay đổi vào cơ sở dữ liệu
                cn.db.SaveChanges();

                MessageBox.Show("Cập nhật trạng thái thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reload lại danh sách vé xe
                LoadVeXe();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật trạng thái: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            UpdateTrangThai();
        }
    }
}
