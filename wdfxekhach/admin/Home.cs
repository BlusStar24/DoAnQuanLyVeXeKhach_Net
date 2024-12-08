using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wdfxekhach;

namespace wdfxekhach.Admin
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }



        public Form activeform = null;
        public void Form_Child(Form childform)
        {
            if (activeform != null)
            {
                activeform.Close();
            }
            else
            {
                activeform = childform;
                childform.TopLevel = false;
                childform.FormBorderStyle = FormBorderStyle.None;
                childform.Dock = DockStyle.Fill;
                panel1.Controls.Add(childform);
                panel3.Tag = childform;
                childform.BringToFront();
                childform.Show();
            }
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
        private void Home_Load(object sender, EventArgs e)
        {
            // Panel 2 ở trên cùng với chiều cao cố định
            panel2.Dock = DockStyle.Top;
            panel2.Height = 200;

            // Panel 4 ở bên trái với chiều rộng cố định
            panel4.Dock = DockStyle.Left;
            panel4.Width = 250;

            // Panel 1 ở bên phải và chiếm phần còn lại dưới panel2
            panel1.Dock = DockStyle.Fill; // Panel1 chiếm phần còn lại của chiều rộng

            // Panel 3 sẽ chiếm phần còn lại phía dưới panel2, và không bị che mất
            //panel3.Dock = DockStyle.Fill;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
        }

        private void btn_dangxuat_Click(object sender, EventArgs e)
        {
            this.Hide();
            new FRDangNhap().ShowDialog();
        }

        private void btnQLTuyen_Click(object sender, EventArgs e)
        {
            Form_Child(new FRTuyenXe());
            activeform = null;
            Control ctr = (Control)sender;
            QuanLy_on_Click((Button)sender);            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            new FRDoiMatKhau().ShowDialog();
        }

        private void btnQLTX_Click(object sender, EventArgs e)
        {
            Form_Child(new FRQLTaiXe());
            activeform = null;
            Control ctr = (Control)sender;
            QuanLy_on_Click((Button)sender);
        }

        private void btnQLXe_Click(object sender, EventArgs e)
        {
            Form_Child(new FRXe());
            activeform = null;
            Control ctr = (Control)sender;
            QuanLy_on_Click((Button)sender);
        }

        private void btnQLloaive_Click(object sender, EventArgs e)
        {
            Form_Child(new FRQLLoaiXe());
            activeform = null;
            Control ctr = (Control)sender;
            QuanLy_on_Click((Button)sender);
        }

        private void btnQLChuyen_Click(object sender, EventArgs e)
        {
            Form_Child(new FRChuyenXe());
            activeform = null;
            Control ctr = (Control)sender;
            QuanLy_on_Click((Button)sender);
        }

        private void btnQLVe_Click(object sender, EventArgs e)
        {
            new frKhachHang().Show();
            QuanLy_on_Click((Button)sender);
        }

        private void btn_thongke_Click(object sender, EventArgs e)
        {
            QuanLy_on_Click((Button)sender);
        }

        private void btnQLNhanVien_Click(object sender, EventArgs e)
        {
            Form_Child(new FRQL_NhanVien());
            activeform = null;
            Control ctr = (Control)sender;
            QuanLy_on_Click((Button)sender);
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //this.Dock = DockStyle.Fill;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnQLKH_Click(object sender, EventArgs e)
        {

        }
    }
}
