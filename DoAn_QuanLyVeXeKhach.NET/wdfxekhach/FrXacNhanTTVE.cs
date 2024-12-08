using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wdfxekhach
{
    public partial class FrXacNhanTTVE : Form
    {
       
        private CONNECT con = new CONNECT();
        public FrXacNhanTTVE()
        {
            InitializeComponent();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void btn_QuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
        
        private void FrXacNhanTTVE_Load(object sender, EventArgs e)
        {
            int chuyendangchonLocal = CONNECT.chuyendangchon;
            string sdt = CONNECT.SoDienThoai;
            MessageBox.Show("sdt ham frxacnhanttve:", sdt);
            List<string> thongTinVe = con.LoadXacNhanThongTinMuaVe(sdt);
            MessageBox.Show("thongtinve ham frxacnhanttve:", thongTinVe.Count.ToString());
            if (thongTinVe.Count > 0)
            {

                txt_Ten.Text = thongTinVe[0];          // "Họ tên: ..."
                txt_SoDienThoai.Text = thongTinVe[1];    // "Số điện thoại: ..."
                txt_Email.Text = thongTinVe[2];          // "Email: ..."
                txt_Tuyen.Text = thongTinVe[3];       // "Tên tuyến: ..."
                txt_NgayDi.Text = thongTinVe[4];         // "Ngày đi: ..."
                txt_GioDi.Text = thongTinVe[5];          // "Giờ đi: ..."
                txtGioDen.Text = thongTinVe[6];         // "Giờ đến: ..."
                txtGiaVe.Text = thongTinVe[7];          // "Giá vé: ..."
                string dsghe = "";
                CONNECT.DsGheDangChon.ForEach(i => dsghe += ", " + i);
                txtdsGhe.Text = dsghe.Remove(0, 1);
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin vé.");
            }
        }

        private void btn_ThanhToan_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txt_DiemDon.Text))
            {
                txt_DiemDon.Text = "Tại bến";
            }
            if (String.IsNullOrEmpty(txt_DiemTra.Text))
            {
                txt_DiemTra.Text = "Tại bến";
            }
            CONNECT.DiemDon = txt_DiemDon.Text;
            CONNECT.DiemTra = txt_DiemTra.Text;
            this.Close();
            Form tt = new FRThanhToan();
            tt.ShowDialog();
        }
    }
}
