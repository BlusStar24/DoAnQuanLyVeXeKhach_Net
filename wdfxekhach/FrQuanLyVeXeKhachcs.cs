using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wdfxekhach.admin;
using wdfxekhach.Admin;

namespace wdfxekhach
{
    public partial class FrQuanLyVeXeKhachcs : Form
    {
        public FrQuanLyVeXeKhachcs()
        {
            InitializeComponent();
           
        }
        public void QuanLy_on_Click(Button click_)
        {
            btn_thongke.BackColor = Color.FromArgb(0, 192, 192);
            btnQLVe.BackColor = Color.FromArgb(0, 192, 192);
            btnQLXe.BackColor = Color.FromArgb(0, 192, 192);
            btnQLTX.BackColor = Color.FromArgb(0, 192, 192);
            btnQLTuyen.BackColor = Color.FromArgb(0, 192, 192);
            btnQLNhanVien.BackColor = Color.FromArgb(0, 192, 192);
            btnQLKH.BackColor = Color.FromArgb(0, 192, 192);
            btnQLChuyen.BackColor = Color.FromArgb(0, 192, 192);
            btnQLloaive.BackColor = Color.FromArgb(0, 192, 192);

            click_.BackColor = Color.Gray;
        }

        private void btn_thongke_Click(object sender, EventArgs e)
        {

            //QuanLy_on_Click((Button)sender);
            //btn_thongke.BackColor = Color.DodgerBlue;
            //panelContainer.Controls.Clear();
            ////.ThongKe tk = new ThongKe.ThongKe();
            //tk.TopLevel = false;
            //tk.FormBorderStyle = FormBorderStyle.None;
            //tk.Dock = DockStyle.Fill;
            //panelContainer.Controls.Add(tk);
            //tk.Show();
        }

        private void btnQLNhanVien_Click(object sender, EventArgs e)
        {
            
            QuanLy_on_Click((Button)sender);
            panel3.Controls.Clear();
            FRQL_NhanVien tl = new FRQL_NhanVien();
            tl.TopLevel = false;
            tl.FormBorderStyle = FormBorderStyle.None; 
            tl.Dock = DockStyle.Fill; 
            panel3.Controls.Add(tl);
            tl.Show();
        }

        private void FrQuanLyVeXeKhachcs_Load(object sender, EventArgs e)
        {
           
            //panel2.Dock = DockStyle.Top;
            //panel2.Height = 100;
            //panel1.Dock = DockStyle.Left;
            //panel1.Width = 200;
            //panel3.Dock = DockStyle.Fill;
            //this.Controls.Add(panel3); 
            //this.Controls.Add(panel1); 
            //this.Controls.Add(panel2); 
        }
        private void btnQLKH_Click(object sender, EventArgs e)
        {
            QuanLy_on_Click((Button)sender);
            panel3.Controls.Clear();
            FR_QL_KhachHang tl = new FR_QL_KhachHang();
            tl.TopLevel = false;
            tl.FormBorderStyle = FormBorderStyle.None;
            tl.Dock = DockStyle.Fill;
            panel3.Controls.Add(tl);
            tl.Show();
        }

        private void btnQLloaive_Click(object sender, EventArgs e)
        {
            QuanLy_on_Click((Button)sender);
            panel3.Controls.Clear();
            FRQLLoaiXe tl = new FRQLLoaiXe();
            tl.TopLevel = false;
            tl.FormBorderStyle = FormBorderStyle.None;
            tl.Dock = DockStyle.Fill;
            panel3.Controls.Add(tl);
            tl.Show();
        }

        private void btnQLTX_Click(object sender, EventArgs e)
        {
            QuanLy_on_Click((Button)sender);
            panel3.Controls.Clear();
            FRQLTaiXe tl = new FRQLTaiXe();
            tl.TopLevel = false;
            tl.FormBorderStyle = FormBorderStyle.None;
            tl.Dock = DockStyle.Fill;
            panel3.Controls.Add(tl);
            tl.Show();
        }

        private void btnQLTuyen_Click(object sender, EventArgs e)
        {
            QuanLy_on_Click((Button)sender);
            panel3.Controls.Clear();
            FRTuyenXe tl = new FRTuyenXe();
            tl.TopLevel = false;
            tl.FormBorderStyle = FormBorderStyle.None;
            tl.Dock = DockStyle.Fill;
            panel3.Controls.Add(tl);
            tl.Show();
        }

        private void btnQLXe_Click(object sender, EventArgs e)
        {
            QuanLy_on_Click((Button)sender);
            panel3.Controls.Clear();
            FRXe tl = new FRXe();
            tl.TopLevel = false;
            tl.FormBorderStyle = FormBorderStyle.None;
            tl.Dock = DockStyle.Fill;
            panel3.Controls.Add(tl);
            tl.Show();
        }

        private void btnQLChuyen_Click(object sender, EventArgs e)
        {
            QuanLy_on_Click((Button)sender);
            panel3.Controls.Clear();
            FRChuyenXe tl = new FRChuyenXe();
            tl.TopLevel = false;
            tl.FormBorderStyle = FormBorderStyle.None;
            tl.Dock = DockStyle.Fill;
            panel3.Controls.Add(tl);
            tl.Show();
        }

        private void btnQLVe_Click(object sender, EventArgs e)
        {
            QuanLy_on_Click((Button)sender);
            panel3.Controls.Clear();
            FR_QLVeXe tl = new FR_QLVeXe();
            tl.TopLevel = false;
            tl.FormBorderStyle = FormBorderStyle.None;
            tl.Dock = DockStyle.Fill;
            panel3.Controls.Add(tl);
            tl.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            QuanLy_on_Click((Button)sender);
            panel3.Controls.Clear();
            FRTaiKhoanKH tl = new FRTaiKhoanKH();
            tl.TopLevel = false;
            tl.FormBorderStyle = FormBorderStyle.None;
            tl.Dock = DockStyle.Fill;
            panel3.Controls.Add(tl);
            tl.Show();
        }

        private void btn_DangXuat_Click(object sender, EventArgs e)
        {
            this.Hide();
            new FRDangNhap().ShowDialog();
        }
    }
}
