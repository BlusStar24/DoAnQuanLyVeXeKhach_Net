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
    public partial class FRDoiMatKhau : Form
    {
        CONNECT con = new CONNECT();
        public FRDoiMatKhau()
        {
            InitializeComponent();
        }

        private void FRDoiMatKhau_Load(object sender, EventArgs e)
        {
            txt_matkhau.Focus();
        }



        private void btn_xacnhan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_matkhau.Text))
            {
                errorProvider1.SetError(txt_matkhau, "Không được bỏ trống");
            }
            else
            {
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(txt_matkhaumoi.Text))
                {
                    errorProvider1.SetError(txt_matkhaumoi, "Không được bỏ trống");
                }
                else
                {
                    errorProvider1.Clear();
                    if (string.IsNullOrEmpty(txt_xacnhanmk.Text))
                    {
                        errorProvider1.SetError(txt_xacnhanmk, "Không được bỏ trống");
                    }
                    else
                    {
                        errorProvider1.Clear();
                        if (txt_matkhau.Text != Staff.matKhau)
                        {
                            errorProvider1.SetError(txt_matkhau, "Mật khẩu sai");
                        }
                        else
                        {
                            errorProvider1.Clear();
                            if (txt_matkhaumoi.Text != txt_xacnhanmk.Text)
                            {
                                MessageBox.Show("Mật khẩu xác nhận không trùng khớp");
                                txt_xacnhanmk.Text = "";
                            }
                            else
                            {
                                con.Change_Password(Staff.maNV, txt_matkhaumoi.Text);
                                Application.Exit();
                                new FRDangNhap().ShowDialog();
                            }
                        }
                    }
                }
            }
        }

        private void txt_matkhau_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_matkhau.Text))
            {
                errorProvider1.SetError(txt_matkhau, "Không được bỏ trống");
            }
            else
            {
                errorProvider1.Clear();            
            }
        }

        private void txt_matkhaumoi_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_matkhaumoi.Text))
            {
                errorProvider1.SetError(txt_matkhaumoi, "Không được bỏ trống");
            }
            else
            {
                errorProvider1.Clear();                
            }
        }

        private void txt_xacnhanmk_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_xacnhanmk.Text))
            {
                errorProvider1.SetError(txt_xacnhanmk, "Không được bỏ trống");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void llbl_Back_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
    }
}
