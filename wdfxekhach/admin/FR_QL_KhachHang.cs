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
    public partial class FR_QL_KhachHang : Form
    {
        public FR_QL_KhachHang()
        {
            InitializeComponent();
        }
       
        CONNECT db = new CONNECT();
        public static string sdt_hanhkhach;
        private void txt_gioitinh_TextChanged(object sender, EventArgs e)
        {
            string input = txt_gioitinh.Text.Trim();

           
        }
      
  

        private void FR_QL_KhachHang_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.ScrollBars = ScrollBars.Both;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataSource = db.db.HANHKHACHes.ToList();


            txt_ten.Text = "";
            txt_Email.Text = "";
            txt_sdt.Text = "";
            txt_gioitinh.Text = "";
            txt_DiaChi.Text = "";


            btn_them.Enabled = true;
            btn_xoa.Enabled = false;
            btn_luu.Enabled = false;
        }

        private void txt_sdt_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_timkiem_Click_1(object sender, EventArgs e)
        {
            var query = db.db.HANHKHACHes.AsQueryable(); // Khởi tạo truy vấn từ bảng HANHKHACH

            // Lọc theo tên khách hàng
            if (!string.IsNullOrEmpty(txt_ten.Text))
            {
                query = query.Where(hk => hk.TenHanhKhach.Contains(txt_ten.Text));
            }

            // Lọc theo số điện thoại
            if (!string.IsNullOrEmpty(txt_sdt.Text))
            {
                query = query.Where(hk => hk.SoDienThoai == txt_sdt.Text);
            }

            // Lọc theo email
            if (!string.IsNullOrEmpty(txt_Email.Text))
            {
                query = query.Where(hk => hk.Email.Contains(txt_Email.Text));
            }

            // Chuyển dữ liệu tìm kiếm thành danh sách và gán vào DataGridView
            var result = query.ToList(); // Thực thi truy vấn
            dataGridView1.DataSource = result;
        }

        private void btn_them_Click_1(object sender, EventArgs e)
        {
            // Kiểm tra các điều kiện nhập liệu
            if (string.IsNullOrEmpty(txt_ten.Text))
            {
                errorProvider1.SetError(txt_ten, "Không được bỏ trống");
                return;
            }
            errorProvider1.Clear();

            if (string.IsNullOrEmpty(txt_sdt.Text))
            {
                errorProvider1.SetError(txt_sdt, "Không được bỏ trống");
                return;
            }
            errorProvider1.Clear();

            if (string.IsNullOrEmpty(txt_Email.Text))
            {
                errorProvider1.SetError(txt_Email, "Không được bỏ trống");
                return;
            }
            errorProvider1.Clear();



            try
            {
                var hanhKhach = new HANHKHACH
                {
                    TenHanhKhach = txt_ten.Text,
                    SoDienThoai = txt_sdt.Text,
                    Email = txt_Email.Text,
                    GioiTinh = txt_gioitinh.Text,
                    DiaChi = txt_DiaChi.Text
                };

                // Thêm vào cơ sở dữ liệu
                db.db.HANHKHACHes.Add(hanhKhach);
                db.db.SaveChanges();

                MessageBox.Show("Thêm hành khách thành công");
                FR_QL_KhachHang_Load(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      

        private void btn_luu_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_ten.Text))
            {
                errorProvider1.SetError(txt_ten, "Không được bỏ trống");
                return;
            }
            errorProvider1.Clear();

            // Kiểm tra số điện thoại không được bỏ trống
            if (string.IsNullOrEmpty(txt_sdt.Text))
            {
                errorProvider1.SetError(txt_sdt, "Không được bỏ trống");
                return;
            }
            errorProvider1.Clear();

            // Kiểm tra email không được bỏ trống
            if (string.IsNullOrEmpty(txt_Email.Text))
            {
                errorProvider1.SetError(txt_Email, "Không được bỏ trống");
                return;
            }
            errorProvider1.Clear();

            // Kiểm tra giới tính không được bỏ trống
            if (string.IsNullOrEmpty(txt_gioitinh.Text))
            {
                errorProvider1.SetError(txt_gioitinh, "Không được bỏ trống");
                return;
            }
            errorProvider1.Clear();

            // Kiểm tra địa chỉ không được bỏ trống
            if (string.IsNullOrEmpty(txt_DiaChi.Text))
            {
                errorProvider1.SetError(txt_DiaChi, "Không được bỏ trống");
                return;
            }
            errorProvider1.Clear();

            try
            {
                var khachHang = db.db.HANHKHACHes.FirstOrDefault(hk => hk.SoDienThoai == txt_sdt.Text);

                if (khachHang != null)
                {
                    // Cập nhật thông tin khách hàng
                    khachHang.TenHanhKhach = txt_ten.Text;
                    khachHang.SoDienThoai = txt_sdt.Text;
                    khachHang.Email = txt_Email.Text;
                    khachHang.GioiTinh = txt_gioitinh.Text;
                    khachHang.DiaChi = txt_DiaChi.Text;

                    // Lưu thay đổi vào cơ sở dữ liệu
                    db.db.SaveChanges();

                    MessageBox.Show("Sửa thành công");
                    FR_QL_KhachHang_Load(sender, e); // Load lại danh sách khách hàng
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khách hàng cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_load_Click_1(object sender, EventArgs e)
        {
            FR_QL_KhachHang_Load(sender, e);
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {

            try
            {
                if (string.IsNullOrEmpty(txt_sdt.Text))
                {
                    MessageBox.Show("Vui lòng chọn hoặc nhập sđt hành khách cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string sdt = txt_sdt.Text;

                // Tìm hành khách trong cơ sở dữ liệu
                var hanhKhach = db.db.HANHKHACHes.FirstOrDefault(hk => hk.SoDienThoai == sdt);

                if (hanhKhach != null)
                {
                    // Xóa hành khách
                    db.db.HANHKHACHes.Remove(hanhKhach);
                    db.db.SaveChanges();

                    MessageBox.Show($"Xóa hành khách có số điện thoại {sdt_hanhkhach} thành công");
                    FR_QL_KhachHang_Load(sender, e); // Load lại danh sách
                }
                else
                {
                    MessageBox.Show("Không tìm thấy hành khách với số điện thoại đã nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];


                txt_ten.Text = selectedRow.Cells[1].Value.ToString();
                txt_sdt.Text = selectedRow.Cells[2].Value.ToString();
                txt_gioitinh.Text = selectedRow.Cells[3].Value.ToString();
                txt_Email.Text = selectedRow.Cells[4].Value.ToString();
                txt_DiaChi.Text = selectedRow.Cells[5].Value.ToString();


                btn_xoa.Enabled = true;
                btn_luu.Enabled = true;
                btn_them.Enabled = false;


            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];


                txt_ten.Text = selectedRow.Cells[1].Value.ToString();
                txt_sdt.Text = selectedRow.Cells[2].Value.ToString();
                txt_gioitinh.Text = selectedRow.Cells[3].Value.ToString();
                txt_Email.Text = selectedRow.Cells[4].Value.ToString();
                txt_DiaChi.Text = selectedRow.Cells[5].Value.ToString();


                btn_xoa.Enabled = true;
                btn_luu.Enabled = true;
                btn_them.Enabled = false;


            }
        }
    }
}
