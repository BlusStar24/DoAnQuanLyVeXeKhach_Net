using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wdfxekhach.Admin
{
    public partial class FRXe : Form
    {
        CONNECT db = new CONNECT();
        public FRXe()
        {
            InitializeComponent();
        }

        public static int MaXe;
        private void FRXe_Load(object sender, EventArgs e)
        {             
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.ScrollBars = ScrollBars.Both;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.DataSource = db.LayXe();

            dataGridView1.Columns[0].HeaderText = "Mã Xe";
            dataGridView1.Columns[1].HeaderText = "Biển Số Xe";
            dataGridView1.Columns[2].HeaderText = "Loại Xe";
            dataGridView1.Columns[3].HeaderText = "Tài Xế";

            cb_loaixe.DataSource = db.LayLoaiXe();
            cb_loaixe.DisplayMember = "TenLoaiXe";
            cb_loaixe.ValueMember = "MaLoaiXe";
            cb_loaixe.SelectedIndex = -1;

            cb_taixe.DataSource = db.LoadTaiXe();
            cb_taixe.DisplayMember = "CCCD";
            cb_taixe.ValueMember = "MaTaiXe";
            cb_taixe.SelectedIndex = -1;

            btn_xoa.Enabled = false;
            btn_luu.Enabled = false;
            btn_them.Enabled = true;

            txt_bienso.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                MaXe = int.Parse(selectedRow.Cells[0].Value.ToString());
                txt_bienso.Text = selectedRow.Cells[1].Value.ToString();
                cb_loaixe.SelectedValue = selectedRow.Cells[2].Value.ToString();
                cb_taixe.SelectedValue = selectedRow.Cells[3].Value.ToString();

                btn_xoa.Enabled = true;
                btn_luu.Enabled = true;
                btn_them.Enabled = false;
            }
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            FRXe_Load(sender, e);
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_bienso.Text))
            {
                errorProvider1.SetError(txt_bienso, "Không được bỏ trống");
            }
            else
            {
                errorProvider1.Clear();
                if(cb_loaixe.SelectedIndex == -1)
                {
                    errorProvider1.SetError(cb_loaixe, "Vui lòng chọn");
                }
                else
                {
                    errorProvider1.Clear();
                    if(cb_taixe.SelectedIndex == -1)
                    {
                        errorProvider1.SetError(cb_taixe, "Vui lòng chọn");
                    }
                    else
                    {
                        if(db.ThemXe(MaXe, txt_bienso.Text, int.Parse(cb_loaixe.SelectedValue.ToString()),int.Parse(cb_taixe.SelectedValue.ToString())) == 1)
                        {
                            MessageBox.Show("Thêm thành công");
                            FRXe_Load(sender, e);
                        }
                        else
                        {
                            MessageBox.Show("Thêm Thất bại");
                        }
                    }
                }
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if(db.XoaXe(MaXe) == 1)
            {
                MessageBox.Show("Xóa thành công");
                FRXe_Load(sender, e);
            }
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_bienso.Text))
            {
                errorProvider1.SetError(txt_bienso, "Không được bỏ trống");
            }
            else
            {
                errorProvider1.Clear();
                if(db.SuaXe(MaXe, txt_bienso.Text, int.Parse(cb_loaixe.SelectedValue.ToString()), int.Parse(cb_taixe.SelectedValue.ToString())) == 1)
                {
                    MessageBox.Show("Sửa thành công");
                    FRXe_Load(sender, e);
                }
            }
        }

        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            string dieukien = " where";
            if (!string.IsNullOrEmpty(txt_bienso.Text))
            {
                dieukien += $" BienSoXe = '{txt_bienso.Text}'";
            }

            if(cb_loaixe.SelectedIndex != -1)
            {
                if(dieukien != " where")
                {
                    dieukien += $" and MaLoaiXe = {cb_loaixe.SelectedValue}";
                }
                else
                {
                    dieukien += $" MaLoaiXe = {cb_loaixe.SelectedValue}";
                }
            }

            if(cb_taixe.SelectedIndex != -1)
            {
                if(dieukien != " where")
                {
                    dieukien += $" and MaTaiXe = {cb_taixe.SelectedValue}";
                }
                else
                {
                    dieukien += $" MaTaiXe = {cb_taixe.SelectedValue}";
                }
            }

            dataGridView1.DataSource = db.LayXe("select * from XE" + ((dieukien != " where") ? dieukien : ""));
        }
    }
}
