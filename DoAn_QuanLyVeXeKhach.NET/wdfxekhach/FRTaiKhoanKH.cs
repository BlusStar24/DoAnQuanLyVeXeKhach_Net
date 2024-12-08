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


        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string soDienThoai = txt_SERCHSDT.Text.Trim(); // Lấy số điện thoại từ TextBox
            
            if (string.IsNullOrEmpty(soDienThoai))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Gọi hàm tìm kiếm hành khách dựa vào số điện thoại
            bool timThayHanhKhach = connect.TimKiemHanhKhachTheoSDT(soDienThoai);
            if( timThayHanhKhach == true)
            {
                this.Hide();
            }    
            if (!timThayHanhKhach)
            {
                MessageBox.Show("Không tìm thấy hành khách với số điện thoại này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            CONNECT.SoDienThoai = soDienThoai;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
