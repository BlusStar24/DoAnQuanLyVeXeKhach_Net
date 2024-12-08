using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wdfxekhach.Admin
{
    public partial class FRTuyenXe : Form
    {

        CONNECT db = new CONNECT();
        public FRTuyenXe()
        {
            InitializeComponent();
            
        }

        public static int MaTX;
        private void FRTuyenXe_Load(object sender, EventArgs e)
        {
            // Khoc duoc nhap lieu vao o
            dataGridView1.ReadOnly = true;
            //khong thay doi kich thuoc cot
            dataGridView1.AllowUserToResizeColumns = false;
            //hien thi thanh truot
            dataGridView1.ScrollBars = ScrollBars.Both;
            dataGridView1.DataSource = db.LayTuyenXe();
            //kich thuoc cot bao phu toan bo datagirdview
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            

            cb_diemden.DataSource = db.LayTinhThanh();
            cb_diemdi.DataSource = db.LayTinhThanh();
            cb_diemden.DisplayMember = "TenTinhThanh";
            cb_diemdi.DisplayMember = "TenTinhThanh";
            cb_diemden.ValueMember = "MaTinhThanh";
            cb_diemdi.ValueMember = "MaTinhThanh";



            cb_diemden.SelectedIndex = -1;
            cb_diemdi.SelectedIndex = -1;


            //ho tro du lieu khi go vao
            //cb_diemdi.AutoCompleteSource = AutoCompleteSource.ListItems;
            //cb_diemdi.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txt_tentuyenxe.Text = "";

            btn_luu.Enabled = false;
            btn_xoa.Enabled = false;
            btn_them.Enabled = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                MaTX = int.Parse(selectedRow.Cells[0].Value.ToString());
                txt_tentuyenxe.Text = selectedRow.Cells[1].Value.ToString();
                cb_diemdi.SelectedValue = selectedRow.Cells[2].Value.ToString();
                cb_diemden.SelectedValue = selectedRow.Cells[3].Value.ToString();

                btn_luu.Enabled = true;
                btn_xoa.Enabled = true;
                btn_them.Enabled = false;
            }
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            FRTuyenXe_Load(sender, e);
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txt_tentuyenxe.Text))
            {
                errorProvider1.SetError(txt_tentuyenxe, "Không được bỏ trống");
            }
            else
            {
                errorProvider1.Clear();
                if(cb_diemdi.SelectedIndex == -1)
                {
                    errorProvider1.SetError(cb_diemdi, "Vui lòng lua chon");
                }
                else
                {
                    errorProvider1.Clear();
                    if(cb_diemden.SelectedIndex == -1)
                    {
                        errorProvider1.SetError(cb_diemden, "Vui lòng lựa chọn");
                    }
                    else
                    {
                        errorProvider1.Clear();
                        if (db.ThemTuyenXe(db.LayMaTX(), txt_tentuyenxe.Text, cb_diemdi.SelectedValue.ToString(), cb_diemden.SelectedValue.ToString()) == 1)
                        {
                            MessageBox.Show("Thêm thành công");
                            FRTuyenXe_Load(sender, e);
                        }
                    }
                }

            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if(db.XoaTuyenXe(MaTX) == 1)
            {
                MessageBox.Show("Xóa thành công");
                FRTuyenXe_Load(sender, e);
            }
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_tentuyenxe.Text)){
                errorProvider1.SetError(txt_tentuyenxe, "Không được bỏ trống");
            }
            else
            {
                errorProvider1.Clear();
                if (db.SuaTuyenXe(MaTX, txt_tentuyenxe.Text, cb_diemdi.SelectedValue.ToString(), cb_diemden.SelectedValue.ToString()) == 1)
                {
                    MessageBox.Show("Sửa thành công");
                    FRTuyenXe_Load(sender, e);
                }
            }
        }

        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            string dieukien = " where";
            string ten = "";
            string diemdi = "";
            string diemden = "";
            if (!string.IsNullOrEmpty(txt_tentuyenxe.Text))
            {
                ten += $" TenTuyenXe like N'%{txt_tentuyenxe.Text}%'";
            }

            if(cb_diemdi.SelectedIndex != -1)
            {
                diemdi = cb_diemdi.SelectedValue.ToString();
            }

            if(cb_diemden.SelectedIndex != -1)
            {
                diemden = cb_diemden.SelectedValue.ToString();
            }

            string caulenh = "select * from TUYENXE";

            if (ten != "")
            {
                caulenh += dieukien + ten;

                if(diemdi != "")
                {
                    caulenh += $" and DiemDi = {diemdi}";
                }

                if(diemden != "")
                {
                    caulenh += $" and DiemDen = {diemden}";
                }

                dataGridView1.DataSource = db.LayTuyenXe(caulenh);
                return;
            }
            else
            {
                if (diemdi != "")
                {
                    caulenh += dieukien + $" DiemDi = {diemdi}";

                    if (diemden != "")
                    {
                        caulenh += $" and DiemDen = {diemden}";
                    }

                    dataGridView1.DataSource = db.LayTuyenXe(caulenh);
                    return;
                }
                else
                {
                    if (diemden != "")
                    {
                        caulenh += dieukien + " DiemDen = {diemden}";

                        dataGridView1.DataSource = db.LayTuyenXe(caulenh);
                        return;
                    }
                    else
                    {
                        return;
                    }
                }                
            }

        }
    }
}
