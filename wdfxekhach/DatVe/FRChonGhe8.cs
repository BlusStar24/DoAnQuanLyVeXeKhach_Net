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
    public partial class FRChonGhe8 : Form
    {
        private CONNECT con = new CONNECT();
        int ThanhTien = 0;
        int FlagChonGhe = 0;
        public FRChonGhe8()
        {
            InitializeComponent();
        }
        

        void ChonGhe(Control e)
        {
            if (e.BackColor == Color.White)
            {
                e.BackColor = Color.Blue;
                ThanhTien += CONNECT.GiaVeDangChon;
                CONNECT.DsGheDangChon.Add(e.Text.ToString());
                FlagChonGhe++;
            }
            else if (e.BackColor == Color.Blue)
            {
                e.BackColor = Color.White;
                ThanhTien -= CONNECT.GiaVeDangChon;
                CONNECT.DsGheDangChon.Remove(e.Text.ToString());
                FlagChonGhe--;
            }
            lblThanhTien.Text = String.Format("{0:#,##0} VND", ThanhTien);
            CONNECT.thanhtien = lblThanhTien.Text;
            lblGheDangChon.Text = "";
            string dsGhe = "";
            foreach (var i in CONNECT.DsGheDangChon)
            {
                dsGhe += ", " + i.ToString();
            }
            if (dsGhe.Length > 1) lblGheDangChon.Text = dsGhe.Remove(0, 1);
        }
        private void btn_XacNhan_Click(object sender, EventArgs e)
        {
            if (FlagChonGhe == 0)
            {
                MessageBox.Show("Chưa chọn ghế");
            }
            else
            {
                this.Close();
                Form tt = new FrXacNhanTTVE();
                tt.ShowDialog();
            }
        }

        private void btn_101_Click(object sender, EventArgs e)
        {
            ChonGhe((Control)sender);
        }

        private void btn_104_Click(object sender, EventArgs e)
        {
            ChonGhe((Control)sender);
        }

        private void btn_102_Click(object sender, EventArgs e)
        {
            ChonGhe((Control)sender);
        }

        private void btn_103_Click(object sender, EventArgs e)
        {
            ChonGhe((Control)sender);
        }

        private void btn_107_Click(object sender, EventArgs e)
        {
            ChonGhe((Control)sender);
        }

        private void btn_105_Click(object sender, EventArgs e)
        {
            ChonGhe((Control)sender);
        }

        private void btn_106_Click(object sender, EventArgs e)
        {
            ChonGhe((Control)sender);
        }

        private void btn_108_Click(object sender, EventArgs e)
        {
            btn_108.Enabled = true;
        }

        private void FRChonGhe8_Load(object sender, EventArgs e)
        {
            lblTitleMain.Text = "SƠ ĐỒ GHẾ CHUYẾN " + CONNECT.ChuyenDangChon;
            List<string> ghe = con.LayDanhSachGheDaDat(CONNECT.chuyendangchon);
            foreach (var item in ghe)
            {
                switch (item)
                {
                    case "101":
                        btn_101.BackColor = Color.Gray;
                        btn_101.Enabled = false;
                        break;
                    case "102":
                        btn_102.BackColor = Color.Gray;
                        btn_102.Enabled = false;
                        break;
                    case "103":
                        btn_103.BackColor = Color.Gray;
                        btn_103.Enabled = false;
                        break;
                    case "104":
                        btn_104.BackColor = Color.Gray;
                        btn_104.Enabled = false;
                        break;
                    case "105":
                        btn_105.BackColor = Color.Gray;
                        btn_105.Enabled = false;
                        break;
                    case "106":
                        btn_106.BackColor = Color.Gray;
                        btn_106.Enabled = false;
                        break;
                    case "107":
                        btn_107.BackColor = Color.Gray;
                        btn_107 .Enabled = false;
                        break;
                    case "108":
                        MessageBox.Show("Ghế của tài xế không được chọn!");
                        break;
                }
            }
        }

        private void llbl_Back_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void lblThanhTien_Click(object sender, EventArgs e)
        {

        }
    }
}
