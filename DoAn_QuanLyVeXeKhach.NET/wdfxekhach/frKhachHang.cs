using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        SqlConnection connection = new SqlConnection("Data Source=XUANCUONG-PC;Initial Catalog=QLBanVeXeKhach_net;Integrated Security=True;");
        bool DangSuaHoSo = true;
        int VeDangChon = -1;
        private int maHanhKhach;
        public static int chuyendatve;
        public void xemve()
        {
            tabcontrol.SelectedTab = tabchitietve;
        }

        public void loadKhachHang(string soDienThoai)
        {
            DataTable dtKhachHang = con.LoadThongTinKhachHang(soDienThoai);

            txt_hoten.Enabled = false;
            txt_diachi.Enabled = false;
            txt_sdt.Enabled = false;
            txt_email.Enabled = false;
            checkbox_nam.Enabled = false;
            checkBox_nu.Enabled = false;
            if (dtKhachHang.Rows.Count > 0)
            {
                // Hiển thị thông tin khách hàng lên các điều khiển
                DataRow row = dtKhachHang.Rows[0];
                maHanhKhach = Convert.ToInt32(row["MaHanhKhach"]);
                CONNECT.MaKhachHang = maHanhKhach.ToString();
                txt_hoten.Text = row["TenHanhKhach"].ToString();
                txt_diachi.Text = row["DiaChi"].ToString();
                txt_sdt.Text = row["SoDienThoai"].ToString();
                txt_email.Text = row["Email"].ToString(); 
                string gioiTinh = row["GioiTinh"].ToString();
                if (gioiTinh == "Nam")
                {
                    checkbox_nam.Checked = true;
                    checkBox_nu.Checked = false;
                }
                else if (gioiTinh == "Nữ")
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

            //bt_suaname.Enabled == true || bt_sua_diachi.Enabled == true || bt_sua_email.Enabled == true || bt_sua_sdt.Enabled == true || bt_sua_gt.Enabled == true
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
            int mahanhkhachh = maHanhKhach;
            try
            {
                // Mở kết nối
                connection.Open();

                // Tạo câu lệnh SQL cập nhật
                string sqlUpdate = "UPDATE HANHKHACH SET TenHanhKhach = @TenHanhKhach, DiaChi = @DiaChi, SoDienThoai = @SoDienThoai, Email = @Email, GioiTinh = @GioiTinh WHERE MaHanhKhach = @MaHanhKhach";

                // Khởi tạo đối tượng SqlCommand
                SqlCommand cmd = new SqlCommand(sqlUpdate, connection);
                
                // Thêm tham số
                cmd.Parameters.AddWithValue("@TenHanhKhach", hoTen);
                cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                cmd.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                cmd.Parameters.AddWithValue("@MaHanhKhach", mahanhkhachh);

                // Thực thi câu lệnh cập nhật
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
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
            finally
            {
                // Đóng kết nối
                if (connection.State == ConnectionState.Open)
                    connection.Close();
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
            dataGridView1.AllowUserToAddRows = false;
            while (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);
            }
            //dGV_DatVe.AllowUserToAddRows = true;
            dataGridView1.DataSource = con.LoadChuyen();
            //List<object> danhSachChuyenXe = dbconnect.GetDanhSachdatve();
            //dataGridView1.DataSource = danhSachChuyenXe;
            dataGridView1.Columns["MaChuyenXe"].HeaderText = "Mã Chuyến Xe";
            dataGridView1.Columns["TenTuyenXe"].HeaderText = "Tên Tuyến Xe";
            dataGridView1.Columns["DiemDi"].HeaderText = "Điểm Đi";
            dataGridView1.Columns["DiemDen"].HeaderText = "Điểm Đến";
            dataGridView1.Columns["ThoiGianXuatPhat"].HeaderText = "Thời Gian Xuất Phát";
            //dataGridView1.Columns["ThoiGianDuKien"].HeaderText = "Thời Gian Dự Kiến";
            dataGridView1.Columns["SoGheTrong"].HeaderText = "Số Ghế Trống";
            dataGridView1.Columns["GiaTien"].HeaderText = "Giá Tiền";
            dataGridView1.Columns["LoaiGhe"].HeaderText = "Loại Ghế";
            dataGridView1.ReadOnly = true;
        }

        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            // Lấy giá trị từ ComboBox và DateTimePicker
            int diemDi;
            int diemDen;
            DateTime thoiGianXuatPhat = dateTimePicker1.Value.Date;

            // Chuyển đổi giá trị điểm đi và điểm đến về kiểu int nếu cần
            if (!int.TryParse(cbx_diemdi.SelectedValue?.ToString(), out diemDi) ||
                !int.TryParse(cbx_diemden.SelectedValue?.ToString(), out diemDen))
            {
                MessageBox.Show("Vui lòng chọn điểm đi và điểm đến.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Mở kết nối
                connection.Open();

                // Tạo câu truy vấn SQL để tìm kiếm
                string sql = @"SELECT 
                        cx.MaChuyenXe, 
                        tx.TenTuyenXe, 
                        tt1.TenTinhThanh AS DiemDi, 
                        tt2.TenTinhThanh AS DiemDen, 
                        cx.ThoiGianXuatPhat, 
                        cx.SoGheTrong, 
                        cx.GiaTien, 
                        lx.LoaiGhe
                    FROM 
                        CHUYENXE cx
                    JOIN 
                        TUYENXE tx ON cx.MaTuyenXe = tx.MaTuyenXe
                    JOIN 
                        TINHTHANH tt1 ON tx.DiemDi = tt1.MaTinhThanh
                    JOIN 
                        TINHTHANH tt2 ON tx.DiemDen = tt2.MaTinhThanh
                    JOIN 
                        XE xe ON cx.MaXe = xe.MaXe
                    JOIN 
                        LOAIXE lx ON xe.MaLoaiXe = lx.MaLoaiXe
                    WHERE 
                        tx.DiemDi = @DiemDi 
                        AND tx.DiemDen = @DiemDen 
                        OR CONVERT(DATE, cx.ThoiGianXuatPhat) = @ThoiGianXuatPhat";

                // Tạo và thiết lập SqlCommand
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@DiemDi", diemDi);
                cmd.Parameters.AddWithValue("@DiemDen", diemDen);
                cmd.Parameters.AddWithValue("@ThoiGianXuatPhat", thoiGianXuatPhat);

                // Kiểm tra giá trị các tham số
                //MessageBox.Show("diemdi: " + diemDi);
                //MessageBox.Show("diemden: " + diemDen);
                //MessageBox.Show("thời gian xuất phát: " + thoiGianXuatPhat.ToString("yyyy-MM-dd"));

                // Tạo SqlDataAdapter để điền dữ liệu vào DataTable
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Hiển thị kết quả lên DataGridView
                dataGridView1.DataSource = dt;

                // Kiểm tra nếu không có kết quả nào trả về
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy chuyến xe phù hợp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Đóng kết nối
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        void LoadchitietVe(int i)
        {
            DataTable ve = con.LoadVeCuaToi(i);
            ve.Columns.Add("GioDi");
            ve.Columns.Add("GioDen");
            foreach (DataRow item in ve.Rows)
            {
                item["NgayDi"] = Convert.ToDateTime(item["NgayDi"]).ToShortDateString();
                string giodi = Convert.ToDateTime(item["ThoiGianXuatPhat"]).ToShortTimeString();
                string gioden = Convert.ToDateTime(item["ThoiGianDenDuKien"]).ToShortTimeString();
                item["GioDi"] = giodi;
                item["GioDen"] = gioden;
            }
            ve.Columns.Remove("ThoiGianXuatPhat");
            ve.Columns.Remove("ThoiGianDenDuKien");
            dataGridView2.DataSource = ve;
        }
        private void frKhachHang_Load(object sender, EventArgs e)
        {
            this.cbx_diemdi.DataSource = con.LoadComboboxDiemDi();
            cbx_diemdi.DisplayMember = "TenTinhThanh"; 
            cbx_diemdi.ValueMember = "MaTinhThanh";    
            con.goiy_ComboBoxTINHTHANH(cbx_diemdi);
            this.cbx_diemden.DataSource = con.LoadComboboxDiemDen();
            cbx_diemden.DisplayMember = "TenTinhThanh"; 
            cbx_diemden.ValueMember = "MaTinhThanh";    
            con.goiy_ComboBoxTINHTHANH(cbx_diemden);
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
                VeDangChon = Convert.ToInt32(chonchuyen); // Lưu ID chuyến xe đã chọn
                CONNECT.ChuyenDangChon = Convert.ToInt32(chonchuyen).ToString();
                CONNECT.chuyendangchon = Convert.ToInt32(chonchuyen);
                chuyendatve = int.Parse(chonchuyen);
               
                // Kiểm tra loại ghế và hiển thị form tương ứng
                if (loaiGhe == "VIP")
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
                // Lấy dòng chuyến xe đã chọn
                DataGridViewRow selectedRow = dataGridView1.Rows[VeDangChon];
                int loaiGhe = VeDangChon;
                if (loaiGhe == 0)
                {
                    // Hiển thị Form Đặt Vé cho Xe VIP
                    ShowBookingForm("VIP");
                }
                else
                {
                    // Hiển thị Form Đặt Vé cho Xe Thường
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

        
    }

}
