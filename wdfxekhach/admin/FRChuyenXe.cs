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
    public partial class FRChuyenXe : Form
    {
        public FRChuyenXe()
        {
            InitializeComponent();
        }

        CONNECT db = new CONNECT();

        public static int MaChuyenXe;
        private void FRChuyenXe_Load(object sender, EventArgs e)
        {
            // dinh dang tuy chinh
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm";
            dateTimePicker1.Value = DateTime.Parse("01/01/1900 00:00");

            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy HH:mm";
            dateTimePicker2.Value = DateTime.Parse("01/01/1900 00:00");

            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.ScrollBars = ScrollBars.Both;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataSource = db.LayChuyenXe();

            cb_tuyenxe.DataSource = db.LayTuyenXe();
            cb_tuyenxe.DisplayMember = "TenTuyenXe";
            cb_tuyenxe.ValueMember = "MaTuyenXe";
            cb_tuyenxe.SelectedIndex = -1;

            cb_xe.DataSource = db.LayXe();
            cb_xe.DisplayMember = "BienSoXe";
            cb_xe.ValueMember = "Maxe";
            cb_xe.SelectedIndex = -1;

            txt_giatien.Text = "";

            btn_xoa.Enabled = false;
            btn_luu.Enabled = false;
            btn_them.Enabled = true;
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            FRChuyenXe_Load(sender, e);
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_giatien.Text))
            {
                errorProvider1.SetError(txt_giatien, "Không được bỏ trống");
            }
            else
            {
                errorProvider1.Clear();
                if (cb_tuyenxe.SelectedIndex == -1)
                {
                    errorProvider1.SetError(cb_tuyenxe, "Vui lòng chọn tuyến");
                }
                else
                {
                    errorProvider1.Clear();
                    if (cb_xe.SelectedIndex == -1)
                    {
                        errorProvider1.SetError(cb_xe, "Vui lòng chọn xe");
                    }
                    else
                    {
                        errorProvider1.Clear();
                        string xuatphat = dateTimePicker1.Value.ToString("dd/MM/yyyy HH:mm");
                        string dukien = dateTimePicker2.Value.ToString("dd/MM/yyyy HH:mm");

                        if (db.ThemChuyenXe(db.LayMaChuyenXe(), int.Parse(cb_tuyenxe.SelectedValue.ToString()), int.Parse(cb_xe.SelectedValue.ToString()), float.Parse(txt_giatien.Text), DateTime.Parse(xuatphat), DateTime.Parse(dukien)) == 1)
                        {
                            MessageBox.Show("Thêm thành công");
                            FRChuyenXe_Load(sender, e);
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
            if (db.XoaChuyenXe(MaChuyenXe) == 1)
            {
                MessageBox.Show("Xóa thành công");
                FRChuyenXe_Load(sender, e);
            }
            else
            {
                MessageBox.Show("Chuyến xe đang hoạt động");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow seletedRow = dataGridView1.Rows[e.RowIndex];

                MaChuyenXe = int.Parse(seletedRow.Cells[0].Value.ToString());

                txt_giatien.Text = seletedRow.Cells[3].Value.ToString();

                cb_tuyenxe.SelectedValue = int.Parse(seletedRow.Cells[1].Value.ToString());

                cb_xe.SelectedValue = int.Parse(seletedRow.Cells[2].Value.ToString());

                dateTimePicker1.Value = DateTime.Parse(seletedRow.Cells[5].Value.ToString());

                dateTimePicker2.Value = DateTime.Parse(seletedRow.Cells[6].Value.ToString());

                btn_xoa.Enabled = true;
                btn_luu.Enabled = true;
                btn_them.Enabled = false;
            }
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_giatien.Text))
            {
                errorProvider1.SetError(txt_giatien, "Không được bỏ trống");
            }
            else
            {
                errorProvider1.Clear();
                if (cb_tuyenxe.SelectedIndex == -1)
                {
                    errorProvider1.SetError(cb_tuyenxe, "Vui lòng chọn tuyến");
                }
                else
                {
                    errorProvider1.Clear();
                    if (cb_xe.SelectedIndex == -1)
                    {
                        errorProvider1.SetError(cb_xe, "Vui lòng chọn xe");
                    }
                    else
                    {
                        errorProvider1.Clear();
                        string xuatphat = dateTimePicker1.Value.ToString("dd/MM/yyyy HH:mm");
                        string dukien = dateTimePicker2.Value.ToString("dd/MM/yyyy HH:mm");

                        if (db.SuaChuyenXe(MaChuyenXe, int.Parse(cb_tuyenxe.SelectedValue.ToString()), int.Parse(cb_xe.SelectedValue.ToString()), float.Parse(txt_giatien.Text), DateTime.Parse(xuatphat), DateTime.Parse(dukien)) == 1)
                        {
                            MessageBox.Show("Lưu thành công");
                            FRChuyenXe_Load(sender, e);
                        }
                        else
                        {
                            MessageBox.Show("Không thể thay đổi chuyến xe");
                        }
                    }
                }
            }
        }

        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            string dieukien = " where";
            if (!string.IsNullOrEmpty(txt_giatien.Text))
            {
                dieukien += $" GiaTien = {txt_giatien.Text}";
            }

            if(cb_tuyenxe.SelectedIndex != -1)
            {
                if(dieukien != " where")
                {
                    dieukien += $" and MaTuyenXe = {cb_tuyenxe.SelectedValue}";
                }
                else
                {
                    dieukien += $" MaTuyenXe = {cb_tuyenxe.SelectedValue}";
                }
            }

            if(cb_xe.SelectedIndex != -1)
            {
                if (dieukien != " where")
                {
                    dieukien += $" and MaXe = {cb_xe.SelectedValue}";
                }
                else
                {
                    dieukien += $" MaXe = {cb_xe.SelectedValue}";
                }
            }

            string diemdi = dateTimePicker1.Value.ToString("dd/MM/yyyy HH:mm");

            string diemden = dateTimePicker2.Value.ToString("dd/MM/yyyy HH:mm");


            if(diemdi != "01/01/1900 00:00")
            {
                if(dieukien != " where")
                {
                    dieukien += $" and ThoiGianXuatPhat = '{diemdi}'";
                }
                else
                {
                    dieukien += $" ThoiGianXuatPhat = '{diemdi}'";
                }
            }

            if(diemden != "01/01/1900 00:00")
            {
                if (dieukien != " where")
                {
                    dieukien += $" and ThoiGianDenDuKien = '{diemden}'";
                }
                else
                {
                    dieukien += $" ThoiGianDenDuKien = '{diemden}'";
                }
            }

            dataGridView1.DataSource = db.LayChuyenXe("select * from CHUYENXE" + ((dieukien != " where") ? dieukien : ""));
        }


    }
}
