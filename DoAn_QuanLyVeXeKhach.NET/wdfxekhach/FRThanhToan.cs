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

        private void btnOK_Click(object sender, EventArgs e)
        {
            con.DatVe();
            CONNECT.ThanhToanXong = true;
            this.Close();
        }
    }
}
