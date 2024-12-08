using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static wdfxekhach.CONNECT;

namespace wdfxekhach
{
    public partial class frKhachHang : Form
    {
        
        public frKhachHang()
        {
            InitializeComponent();
            LoadDanhSachChuyenXe();
        }
        //Data Source=XUANCUONG-PC;Initial Catalog=databaseQLBanVeXeKhach;Integrated Security=True;Encrypt=True;Trust Server Certificate=True
        //Data Source=.;Initial Catalog=quanlyxekhachdoan;Integrated Security=True;
        private CONNECT con = new CONNECT();
        SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=quanlyxekhachdoan;Integrated Security=True;");
        bool DangSuaHoSo = true;
        int VeDangChon = -1;
        private int maHanhKhach;
        public static int chuyendatve;
        public void xemve()
        {
            tabcontrol.SelectedTab = tabchitietve;
        }

        public void loadKhachHang(int maHanhKhach)
        {
            // Gọi hàm LoadThongTinKhachHang để lấy thông tin khách hàng
            HANHKHACH khachHang = con.LoadThongTinKhachHang(maHanhKhach);
            
            // Vô hiệu hóa các trường nhập liệu
            txt_hoten.Enabled = false;
            txt_diachi.Enabled = false;
            txt_sdt.Enabled = false;
            txt_email.Enabled = false;
            checkbox_nam.Enabled = false;
            checkBox_nu.Enabled = false;

            // Kiểm tra và hiển thị thông tin khách hàng
            if (khachHang != null)
            {
                txt_hoten.Text = khachHang.TenHanhKhach;
                txt_diachi.Text = khachHang.DiaChi;
                txt_sdt.Text = khachHang.SoDienThoai;
                txt_email.Text = khachHang.Email;

                // Xử lý giới tính
                if (khachHang.GioiTinh == "Nam")
                {
                    checkbox_nam.Checked = true;
                    checkBox_nu.Checked = false;
                }
                else if (khachHang.GioiTinh == "Nữ")
                {
                    checkbox_nam.Checked = false;
                    checkBox_nu.Checked = true;
                }
                else
                {
                    // Trường hợp không rõ giới tính, bỏ chọn cả hai
                    checkbox_nam.Checked = false;
                    checkBox_nu.Checked = false;
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Hàm để xóa các trường dữ liệu trên form
        private void clearTextFields()
        {
            txt_hoten.Text = "";
            checkbox_nam.Checked = false;
            checkBox_nu.Checked = false;
            txt_sdt.Text = "";
            txt_email.Text = "";
            txt_diachi.Text = "";
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void bt_suaname_Click(object sender, EventArgs e)
        {
            txt_hoten.Enabled = true;
        }

        private void bt_sua_sdt_Click(object sender, EventArgs e)
        {
            txt_sdt.Enabled = true;
        }

        private void bt_sua_diachi_Click(object sender, EventArgs e)
        {
            txt_diachi.Enabled = true;
        }

        private void bt_sua_email_Click(object sender, EventArgs e)
        {
            txt_email.Enabled = true;
        }

        private void bt_sua_gt_Click(object sender, EventArgs e)
        {
            checkbox_nam.Enabled = true;
            checkBox_nu.Enabled = true;
            
            
        }

        private void button_luu_Click(object sender, EventArgs e)
        {
            // Kiểm tra trạng thái chỉnh sửa
            if (!DangSuaHoSo)
            {
                MessageBox.Show("Vui lòng bấm vào nút Sửa để chỉnh sửa thông tin khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Lấy thông tin từ các trường nhập liệu
            string hoTen = txt_hoten.Text.Trim();
            string diaChi = txt_diachi.Text.Trim();
            string soDienThoai = txt_sdt.Text.Trim();
            string email = txt_email.Text.Trim();
            string gioiTinh = checkbox_nam.Checked ? "Nam" : (checkBox_nu.Checked ? "Nữ" : "");
            int mahk = CONNECT.MaKhachHang; // Biến maHanhKhach đã được xác định từ trước.

            // Kiểm tra thông tin đầu vào
            if (string.IsNullOrWhiteSpace(hoTen) || string.IsNullOrWhiteSpace(diaChi) || string.IsNullOrWhiteSpace(soDienThoai))
            {
                MessageBox.Show("Tên, địa chỉ và số điện thoại không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (soDienThoai.Length != 10 || !soDienThoai.All(char.IsDigit))
            {
                MessageBox.Show("Số điện thoại phải gồm 10 chữ số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrWhiteSpace(email) && !email.Contains("@"))
            {
                MessageBox.Show("Email không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Tìm khách hàng cần cập nhật
                var khachHang = con.db.HANHKHACHes.Find(mahk);
                if (khachHang != null)
                {
                    // Cập nhật thông tin khách hàng
                    khachHang.TenHanhKhach = hoTen;
                    khachHang.DiaChi = diaChi;
                    khachHang.SoDienThoai = soDienThoai;
                    khachHang.Email = email;
                    khachHang.GioiTinh = gioiTinh;

                    // Lưu thay đổi vào cơ sở dữ liệu
                    con.db.SaveChanges();

                    MessageBox.Show("Thông tin khách hàng đã được lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Vô hiệu hóa các trường nhập liệu sau khi lưu
                    txt_hoten.Enabled = false;
                    txt_diachi.Enabled = false;
                    txt_sdt.Enabled = false;
                    txt_email.Enabled = false;
                    checkbox_nam.Enabled = false;
                    checkBox_nu.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin khách hàng để cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu thông tin khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void btn_doidiem_Click(object sender, EventArgs e)
        {
            string diemDiHienTai = cbx_diemdi.SelectedValue.ToString();
            string diemDenHienTai = cbx_diemden.SelectedValue.ToString();
            cbx_diemdi.SelectedValue = diemDenHienTai;
            cbx_diemden.SelectedValue = diemDiHienTai;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                dateTimePicker2.Enabled = false;
            }    
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                dateTimePicker2.Enabled = true;
            }
        }
        private void LoadDanhSachChuyenXe()
        {
            // Tắt tính năng cho phép thêm hàng
            dataGridView1.AllowUserToAddRows = false;

            // Gán lại DataSource cho DataGridView
            var chuyenXeData = con.LoadChuyen(); // Lấy dữ liệu từ phương thức LoadChuyen()

            // Kiểm tra nếu có dữ liệu
            if (chuyenXeData != null && chuyenXeData.Rows.Count > 0)
            {
                dataGridView1.DataSource = chuyenXeData;

                // Đặt tên tiêu đề cho các cột
                dataGridView1.Columns["MaChuyenXe"].HeaderText = "Mã Chuyến Xe";
                dataGridView1.Columns["TenTuyenXe"].HeaderText = "Tên Tuyến Xe";
                dataGridView1.Columns["DiemDi"].HeaderText = "Điểm Đi";
                dataGridView1.Columns["DiemDen"].HeaderText = "Điểm Đến";
                dataGridView1.Columns["ThoiGianXuatPhat"].HeaderText = "Thời Gian Xuất Phát";
                dataGridView1.Columns["SoGheTrong"].HeaderText = "Số Ghế Trống";
                dataGridView1.Columns["GiaTien"].HeaderText = "Giá Tiền";
                dataGridView1.Columns["LoaiGhe"].HeaderText = "Loại Ghế";

                // Đảm bảo DataGridView chỉ đọc
                dataGridView1.ReadOnly = true;
            }
            else
            {
                MessageBox.Show("Không có chuyến xe nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            // Lấy giá trị từ ComboBox và DateTimePicker
            int diemDi = (int)cbx_diemdi.SelectedValue;
            int diemDen = (int)cbx_diemden.SelectedValue;
            DateTime thoiGianXuatPhat = dateTimePicker1.Value.Date;

            // Kiểm tra dữ liệu đầu vào
            if (diemDi < 0 || diemDen < 0)
            {
                MessageBox.Show("Vui lòng chọn điểm đi và điểm đến.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Gọi hàm TimChuyenXe sử dụng Entity Framework
                var danhSachChuyenXe = con.TimChuyenXe(diemDi, diemDen, thoiGianXuatPhat);

                if (danhSachChuyenXe != null && danhSachChuyenXe.Count > 0)
                {
                    // Hiển thị danh sách chuyến xe lên DataGridView
                    dataGridView1.DataSource = danhSachChuyenXe;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy chuyến xe phù hợp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        void LoadchitietVe(int i)
        {
            DataTable ve = con.LoadVeCuaToi(i);
          
            
          
            dataGridView2.DataSource = ve;
        }
        private void frKhachHang_Load(object sender, EventArgs e)
        {
            var diemDiList = con.LoadComboboxDiemDi();
            cbx_diemdi.DataSource = diemDiList;
            cbx_diemdi.DisplayMember = "TenTinhThanh";
            cbx_diemdi.ValueMember = "MaTinhThanh";

            var diemDenList = con.LoadComboboxDiemDen();
            cbx_diemden.DataSource = diemDenList;
            cbx_diemden.DisplayMember = "TenTinhThanh";
            cbx_diemden.ValueMember = "MaTinhThanh";

            LoadchitietVe(0);
            cbx_locve.Items.Add("Tất cả");
            cbx_locve.Items.Add("Còn hạn");
            cbx_locve.Items.Add("Hết hạn");
            cbx_locve.SelectedIndex = 0; // Chọn "Tất cả" làm mặc định
        }

        private void but_reset_Click(object sender, EventArgs e)
        {
            LoadDanhSachChuyenXe();

            
            cbx_diemdi.SelectedIndex = -1; 
            cbx_diemden.SelectedIndex = -1; 
            dateTimePicker1.Value = DateTime.Now;
        }

        
        private void ShowBookingForm(string busType)
        {
            if (busType == "VIP")
            {
                // Hiển thị Form Đặt Vé cho Xe VIP
                Form formVipBooking = new FRChonGhe8();
                formVipBooking.ShowDialog();
            }
            else if(busType == "Thường")
            {
                // Hiển thị Form Đặt Vé cho Xe Thường
                Form formRegularBooking = new FRChonGhe11();
                formRegularBooking.ShowDialog();
            }
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0)
            {
                // Lấy dữ liệu của dòng đã chọn
                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                string maChuyenXe = row.Cells["MaChuyenXe"].Value.ToString();
                
                string loaiGhe = row.Cells["LoaiGhe"].Value.ToString(); // VIP hoặc Xe thường
                string chonchuyen = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
                con.ChonChuyen(chonchuyen);
                VeDangChon = Convert.ToInt32(maChuyenXe); // Lưu ID chuyến xe đã chọn
               CONNECT.chuyendangchon = int.Parse(chonchuyen);
                chuyendatve = int.Parse(chonchuyen);
               
                // Kiểm tra loại ghế và hiển thị form tương ứng
                if (loaiGhe == "Ghe VIP")
                {
                    VeDangChon = 0; // Lưu thông tin chuyến xe VIP
                }
                else
                {
                    VeDangChon = 1; // Lưu thông tin chuyến xe Thường
                }
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void btn_trolai_Click(object sender, EventArgs e)
        {
            clearTextFields();
            dateTimePicker1.Value = DateTime.Now;
            this.Close();
            FRTaiKhoanKH frTaikhoan = new FRTaiKhoanKH();
            frTaikhoan.Show();
        }
        private void btn_datve_Click(object sender, EventArgs e)
        {

            if (VeDangChon != -1)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[VeDangChon];
                int loaiGhe = VeDangChon;
                if (loaiGhe == 0)
                {
                    ShowBookingForm("VIP");
                }
                else
                {
                    ShowBookingForm("Thường");
                }

            }
            else
            {
                MessageBox.Show("Vui lòng chọn một chuyến xe!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void cbx_locve_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            int i;
            if (cbx_locve.SelectedItem.ToString() == "Còn hạn")
                i = 1;    // Chỉ lọc vé còn hạn
            else if (cbx_locve.SelectedItem.ToString() == "Hết hạn")
                i = -1;   // Chỉ lọc vé hết hạn
            else
                i = 0;    // Hiển thị tất cả vé

            DataTable dt = con.LoadVeCuaToi(i);
            dataGridView2.DataSource = dt;
        }

        private void btn_reload_Click(object sender, EventArgs e)
        {
            LoadchitietVe(0);
        }

        private void tabcontrol_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bt_sua_cccd_Click(object sender, EventArgs e)
        {

        }

        private void checkBox_nu_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txt_email_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void txt_diachi_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void checkbox_nam_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txt_sdt_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void txt_cccd_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void txt_hoten_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void cbx_diemden_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbx_diemdi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabchitietve_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }

}
