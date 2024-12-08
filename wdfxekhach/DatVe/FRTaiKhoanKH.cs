using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wdfxekhach
{
    public partial class FRTaiKhoanKH : Form
    {
        private CONNECT connect = new CONNECT();
       
        public FRTaiKhoanKH()
        {
            InitializeComponent();
            
        }
        public int TimKiemHanhKhachTheoSDTHK(string soDienThoai)
        {
            try
            {
                var khachHang = connect.db.HANHKHACHes
                    .Where(kh => kh.SoDienThoai == soDienThoai)
                    .Select(kh => kh.MaHanhKhach)
                    .FirstOrDefault();
                return khachHang;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                MessageBox.Show($"Lỗi khi tìm kiếm hành khách: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0; // Không tìm thấy hoặc có lỗi
            }

        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string soDienThoai = txt_SERCHSDT.Text.Trim(); // Lấy số điện thoại từ TextBox
            
            if (string.IsNullOrEmpty(soDienThoai))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Gọi hàm tìm kiếm hành khách dựa vào số điện thoại
            int maHanhKhach = TimKiemHanhKhachTheoSDTHK(soDienThoai);
            if (maHanhKhach > 0) // Nếu tìm thấy hành khách
            {
                // Đóng SqlDataReader trước khi chuyển dữ liệu


                // Chuyển sang form khách hàng
                frKhachHang formKhachHang = new frKhachHang();
                FRTaiKhoanKH frtaikhoan = new FRTaiKhoanKH();
                // Truyền thông tin số điện thoại sang form khách hàng
                formKhachHang.loadKhachHang(maHanhKhach);
                formKhachHang.Show();
                frtaikhoan.Hide();
                CONNECT.SoDienThoai = soDienThoai;
                CONNECT.MaKhachHang = maHanhKhach;
            }
            
            else
            {
                MessageBox.Show("Không tìm thấy hành khách với số điện thoại này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FRTaiKhoanKH_Load(object sender, EventArgs e)
        {

        }
    }
}
