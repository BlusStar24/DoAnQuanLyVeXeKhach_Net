using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace wdfxekhach
{
    public partial class FRQuenMatKhau : Form
    {
        CONNECT con = new CONNECT();
        public FRQuenMatKhau()
        {
            InitializeComponent();
        }

        private void llbl_Back_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form a = new FRDangNhap();
            this.Close();
            a.Show();
        }

        

        private void btn_xacnhan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_email.Text))
            {
                errorProvider1.SetError(txt_email, "Không được bỏ trống");
            }
            else
            {
                if (con.IsEmail(txt_email.Text) == false)
                {
                    errorProvider1.SetError(txt_email, "Vui lòng nhập đúng định dạng");
                }
                else
                {
                    errorProvider1.Clear();
                    string email = (txt_email.Text).Trim();

                    if (con.KT_NhanVien(email) == 1)
                    {

                        var message = new MailMessage("leminhminh1303@gmail.com", email)
                        {
                            Subject = "Yêu cầu lấy lại mật khẩu",
                            Body = $"Mật khẩu của bạn là: {con.GetPassword(email)}",
                            IsBodyHtml = true
                        };

                        // Cấu hình SMTP
                        using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                        {
                            smtp.Credentials = new NetworkCredential("leminhminh1303@gmail.com", "nwbu pvej egmo adqj");
                            smtp.EnableSsl = true;

                            smtp.Send(message);
                        }

                        MessageBox.Show("Mật khẩu đã được gửi tới Email của bạn");

                    }
                    else
                    {
                        MessageBox.Show("Email không tồn tại");
                    }
                }                
            }
            
        }

        private void FRQuenMatKhau_Load(object sender, EventArgs e)
        {
            
        }

        
    }
}
