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
    public partial class FRThanhToan : Form
    {
        CONNECT con = new CONNECT();
        public FRThanhToan()
        {
            InitializeComponent();
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FRThanhToan_Load(object sender, EventArgs e)
        {
            lbl_GiaTien.Text = String.Format("{0:#,##0} VND", (CONNECT.GiaVeDangChon * CONNECT.DsGheDangChon.Count()).ToString());
        }
        public void ThanhToan()
        {
            // Kiểm tra thông tin khách hàng, vé, chuyến xe, vv.
            if (con.InsertVeXe(CONNECT.MaKhachHang, CONNECT.chuyendangchon))
            {
                Console.WriteLine("Vé xe đã được thanh toán và chèn vào hệ thống.");
            }
            else
            {
                Console.WriteLine("Có lỗi xảy ra khi thanh toán vé.");
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ThanhToan();
            con.DatVe(CONNECT.MaKhachHang, CONNECT.chuyendangchon);
            CONNECT.ThanhToanXong = true;
            this.Close();
        }
    }
}
