using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Windows.Forms;
using wdfxekhach.Admin;


namespace wdfxekhach
{
    public partial class FRDangNhap : Form
    {
        CONNECT con = new CONNECT();
        public FRDangNhap()
        {
            InitializeComponent();
        }
        public static int manhanvien;


        public int GetMaNhanVienFromUsername(string username)
        {
            try
            {
                // Lấy UserID từ bảng TAIKHOAN
                var user = con.db.TAIKHOANs.FirstOrDefault(u => u.UserName == username);

                if (user != null)
                {
                    // Lấy MaNhanVien từ bảng NHANVIEN dựa vào UserID
                    var nhanVien = con.db.NHANVIENs.FirstOrDefault(nv => nv.MaNhanVien == user.UserID);

                    if (nhanVien != null)
                    {
                        return nhanVien.MaNhanVien;
                    }
                }

                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy MaNhanVien: " + ex.Message);
                return -1; // Trả về -1 nếu có lỗi
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtUsername.Text))
            {
                this.errorProvider1.SetError(txtUsername, "Vui lòng nhập username");
                err_name.Text = "Vui lòng nhập username";
                return;
            }
            if (String.IsNullOrEmpty(txtPassword.Text))
            {
                this.errorProvider1.SetError(txtPassword, "Vui lòng nhập username");
                err_password.Text = "Vui lòng nhập password";
                return;
            }

            if (con.KT_TaiKhoan(txtUsername.Text, txtPassword.Text) == 0)
            {
                MessageBox.Show("Tài khoản không tồn tại");
            }
            else
            {
                int maNhanVien = GetMaNhanVienFromUsername(txtUsername.Text);
                CONNECT.MaNHanVien = maNhanVien;
                Form tam = new FrQuanLyVeXeKhachcs();
                this.Hide();
                tam.Show();
            }

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim().Length > 0)
            {
                this.errorProvider1.Clear();
                err_name.Text = string.Empty;
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text.Trim().Length > 0)
            {
                this.errorProvider1.Clear();
                err_password.Text = string.Empty;
            }
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            err_name.Text = string.Empty;
            err_password.Text = string.Empty;
        }        

        private void lblQuenMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form quen = new FRQuenMatKhau();
            quen.ShowDialog();
        }
    }
}
