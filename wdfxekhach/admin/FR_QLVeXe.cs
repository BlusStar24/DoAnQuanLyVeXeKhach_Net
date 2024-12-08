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

                // Hiển thị thông tin vào các TextBox
                txt_ten.Text = tenKhachHang;
                txt_sdt.Text = soDienThoai;
                txt_trangthai.Text = trangThai;
            }
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            LoadVeXe();
        }
    }
}
